using System;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using Zenject;

public class MidiWorker : ILateDisposable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private UnityEnforceMainThreadWorker _mainThread;

    private float _compositeDelay;
    
    private Playback _playback;
    private MidiFile _midiFile;
    private TempoMap _tempoMap;
    private TrackData _currentTrack;

    public void StartTrack(float compositeDelay, TrackData trackData)
    {
        _compositeDelay = compositeDelay;
        _currentTrack = trackData;
        _midiFile = MidiFile.Read(Application.dataPath + trackData.path).Clone();
        _tempoMap = _midiFile.GetTempoMap();
        _playback = _midiFile.GetPlayback(new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        _playback.MoveToStart();
        _playback.NotesPlaybackStarted += OnNoteStarted;
        _playback.InterruptNotesOnStop = true;
        _playback.Finished += OnSongFinish;
        _playback.Start();
    }
    public void PauseTrack()
    {
        if (!_playback.IsRunning)
        {
            _playback.Start();
            return;
        }

        _playback.Stop();
    }
    private void OnSongFinish(object sender, EventArgs e)
    {
        _mainThread.AddJob(async () =>
        {
            await Task.Delay(Mathf.CeilToInt((_compositeDelay + 1.5f) * 1000));
            _signalBus?.Fire(new GameEndSignal());
        });
    }
    private void OnNoteStarted(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        foreach (Note item in notesList)
        {
            var hasNote = _currentTrack.HasNote(item);
            
            if (string.IsNullOrEmpty(hasNote))
                continue;
            
            _mainThread.AddJob(() =>
            {
                float noteStartTime = item.TimeAs<MetricTimeSpan>(_tempoMap).TotalMicroseconds * 0.000001f;
                float noteEndTime = item.EndTimeAs<MetricTimeSpan>(_tempoMap).TotalMicroseconds * 0.000001f;
                _signalBus.Fire(new RhythmPalette(noteStartTime, noteEndTime, hasNote));
                // Debug.Log($"Note:{hasNote} start at:{noteStartTime} end at: {noteEndTime}");
            });
        }
    }
    public void LateDispose()
    {
        _playback?.Stop();
        _playback?.Dispose();
    }
}