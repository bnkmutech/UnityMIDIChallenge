using UnityEngine;
using Zenject;

public class RhythmPaletteSpawnWorker
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private RhythmPalettePool _pool;
    private GameMode _gameMode;

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
    }
    public void SpawnNote(RhythmPalette palette, LaneUI laneUI, Transform rhythmField)
    {
        var rhythm = _pool.Create(palette.id, laneUI.spawnPoint, rhythmField);
        rhythm.SetColor(laneUI.laneColor);
        rhythm.Move(_gameSettings.startDelay + _gameMode.laneDelay, laneUI.despawnPoint.anchoredPosition);
    }
    public void Pause()
    {
        _pool.Pause();
    }
    public void DespawnNote(string id)
    {
        _pool.Destroy(id);
    }

    public void DestroyAll()
    {
        _pool.DestroyAll();
    }
}