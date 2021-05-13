using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class TrackController
{
    [Inject] private SignalBus _signalBus;
    [Inject] private MidiWorker _midiWorker;
    [Inject] private GameSettings _gameSettings;
    [Inject] private TrackSettings _settings;

    public async void OnTrackSelected(TrackSelectedSignal signal)
    {
        _settings.audioSource.clip = signal.trackData.clip;
        _midiWorker.StartTrack(_gameSettings.startDelay + signal.mode.laneDelay, signal.trackData);
        await Task.Delay(Mathf.CeilToInt(1000 * (_gameSettings.startDelay + signal.mode.laneDelay)));
        _settings.audioSource.Play();
        _signalBus.Fire(new TrackStartedSignal());
        
    }
    public void OnGamePause(GamePauseSignal signal)
    {
        _midiWorker.PauseTrack();

        if (_settings.audioSource.isPlaying)
            _settings.audioSource.Pause();
        else
            _settings.audioSource.UnPause();
    }
}

public class TrackStartedSignal
{
    
}