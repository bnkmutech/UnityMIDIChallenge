using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RhythmLanePresenter : IInitializable
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private LaneUI.Factory _laneUIFactory;
    [Inject] private RhythmLaneInputWorker _inputWorker;
    [Inject] private RhythmPaletteSpawnWorker _rhythmPaletteSpawnWorker;
    [Inject] private GameBoardUI.Factory _gameBoardFactory;
    
    private GameBoardUI _gameBoard;
    private List<LaneUI> _laneUis = new List<LaneUI>();
    
    public void Initialize()
    {
        _gameBoard = _gameBoardFactory.Create();
    }
    public void OnTrackSelected(TrackSelectedSignal signal)
    {
        _rhythmPaletteSpawnWorker.SetGameMode(signal.mode);

        if (signal.mode.laneSettings.Count < _laneUis.Count)
        {
            var discardCount = _laneUis.Count - signal.mode.laneSettings.Count;
            for (int i = 0; i < discardCount; i++)
            {
                Object.Destroy(_laneUis[_laneUis.Count - 1].gameObject);
                _laneUis.RemoveAt(_laneUis.Count - 1);
            }
        }
        else
        {
            for (int i = 0; i < signal.mode.laneSettings.Count; i++)
            {
                if (_laneUis.Count - 1 < i)
                {
                    var laneUI = _laneUIFactory.Create(_gameSettings.laneUIPrefab, _gameBoard.laneSlot);
                    _laneUis.Add(laneUI);
                    _laneUis[i].Initialize(_gameSettings.laneColors[i], signal.mode.laneSettings[i].targetNotes);
                }
            }
        }

        _inputWorker.SetUIInput(_laneUis);
    }
    public void OnGameEnd(GameEndSignal signal)
    {
        _rhythmPaletteSpawnWorker.DestroyAll();
    }
    public void OnGamePause(GamePauseSignal signal)
    {
        _rhythmPaletteSpawnWorker.Pause();
    }
    public void OnNoteDetected(RhythmPalette palette)
    {
        var laneIndex = _laneUis.FindIndex(lane => lane.HasNote(palette.note));
        
        if (laneIndex <= -1)
            return;
        
        _rhythmPaletteSpawnWorker.SpawnNote(palette, _laneUis[laneIndex], _gameBoard.rhythmField);
    }
    public void OnNoteInteract(NoteInteractSignal signal)
    {
        _rhythmPaletteSpawnWorker.DespawnNote(signal.id);
        
        var index = _laneUis.FindIndex(lane => lane.HasNote(signal.noteNote));

        if (index > -1)
            _laneUis[index].OnNoteHit();
    }
}