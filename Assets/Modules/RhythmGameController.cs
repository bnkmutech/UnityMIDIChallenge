using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RhythmGameController : ITickable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private GameSettings _gameSettings;
    [Inject] private RhythmLaneInputWorker _inputWorker;
    
    private bool _isEnabled;
    private float _currentTime;
    private GameMode _mode;
    private List<IRhythmLaneController> _controllers = new List<IRhythmLaneController>();
    
    public void OnNoteDetected(RhythmPalette palette)
    {
        for (int i = 0; i < _controllers.Count; i++)
            _controllers[i].AddPalette(palette);
    }
    public void OnTrackSelected(TrackSelectedSignal signal)
    {
        _mode = signal.mode;

        if (signal.mode.laneSettings.Count < _controllers.Count)
        {
            var discardCount = _controllers.Count - signal.mode.laneSettings.Count;
            for (int i = 0; i < discardCount; i++)
            {
                _controllers.RemoveAt(_controllers.Count - 1);
            }

            for (int i = 0; i < _mode.laneSettings.Count; i++)
                _controllers[i].Renew(_mode.laneSettings[i]);
            
        }
        else
        {
            for (int i = 0; i < _mode.laneSettings.Count; i++)
            {
                if (_controllers.Count - 1 < i)
                {
                    var classicRhythmController = new RhythmLaneController(_signalBus, _mode.laneSettings[i]);
                    _controllers.Add(classicRhythmController);
                }
                else
                    _controllers[i].Renew(_mode.laneSettings[i]);
            }
        }
        
        _currentTime = 0;
        _inputWorker.SetLogicInput(_controllers);
        
        _isEnabled = true;
    }
    public void OnGameEnd(GameEndSignal signal)
    {
        _isEnabled = false;

        for (int i = 0; i < _controllers.Count; i++)
            _controllers[i].currentTime = 0;
    }
    public void OnGamePause(GamePauseSignal signal)
    {
        _isEnabled = !_isEnabled;
    }
    public void Tick()
    {
        if (!_isEnabled)
            return;
        
        _currentTime += Time.deltaTime;

        for (int i = 0; i < _controllers.Count; i++)
            _controllers[i].currentTime = _currentTime - (_mode.laneDelay + _gameSettings.startDelay);
    }
}