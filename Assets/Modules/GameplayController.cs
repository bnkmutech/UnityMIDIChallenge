using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private GameObject _rimSnare;
    [SerializeField] private GameObject _snare;
    [SerializeField] private GameObject _floorTom;
    [SerializeField] private GameObject _highTom;
    [SerializeField] private GameObject _bass;
    [SerializeField] private GameObject _cymbal;
    [SerializeField] private TrackData _currentTrack;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;
    
    private Playback _playback;
    private MidiFile _midi;
    
    private void Start()
    {
        _source.clip = _clip;
        _midi = MidiFile.Read(_currentTrack.path).Clone();
        _playback = _midi.GetPlayback(new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        _playback.MoveToStart();
        _playback.NotesPlaybackStarted += StartNoteTest;
        _playback.NotesPlaybackFinished += EndedNoteTest;
        _playback.InterruptNotesOnStop = true;
        StartCoroutine(StartMusic());
    }
    private void StartNoteTest(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        foreach (Note item in notesList)
        {
            UnityForceMainThread.wkr.AddJob(() =>
            {
                var hasNote = _currentTrack.HasNote(item);
                if (hasNote == "C#2")
                    _rimSnare.SetActive(true);
            
                if (hasNote == "C#3")
                    _cymbal.SetActive(true);
            
                if (hasNote == "D2")
                    _snare.SetActive(true);
            
                if (hasNote == "C2")
                    _bass.SetActive(true);
            
                if (hasNote == "C3")
                    _highTom.SetActive(true);
            
                if (hasNote == "G2")
                    _floorTom.SetActive(true);
            
                Debug.Log(hasNote + "start");
            });
        }
    }
    private void EndedNoteTest(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        foreach (Note item in notesList)
        {
            UnityForceMainThread.wkr.AddJob(() =>
            {
                var hasNote = _currentTrack.HasNote(item);
            
                if (hasNote == "C#2")
                    _rimSnare.SetActive(false);
            
                if (hasNote == "C#3")
                    _cymbal.SetActive(false);
            
                if (hasNote == "D2")
                    _snare.SetActive(false);
            
                if (hasNote == "C2")
                    _bass.SetActive(false);
            
                if (hasNote == "C3")
                    _highTom.SetActive(false);
            
                if (hasNote == "G2")
                    _floorTom.SetActive(false);
            
                Debug.Log(hasNote + "ended");
            });
        }
    }
    private IEnumerator StartMusic()
    {
        _source.Play();
        _playback.Start();
        while (_playback.IsRunning)
        {
            
            yield return null;
           
        }
        _playback.Dispose();

    }
    private void OnApplicationQuit()
    {
        _playback.Stop();
        _playback.Dispose();
    }
}