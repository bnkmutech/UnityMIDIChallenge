using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RhythmPalettePool
{
    [Inject] private GameSettings _gameSettings;
    [Inject] private LaneRhythm.Factory _factory;
    
    private List<LaneRhythm> _activePool = new List<LaneRhythm>();
    private List<LaneRhythm> _disablePool = new List<LaneRhythm>();

    public LaneRhythm Create(string id, RectTransform spawnPoint, Transform parent)
    {
        LaneRhythm laneRhythmInstance = null;
        
        var disabledIndex = _activePool.FindIndex(disabled => !disabled.isEnabled);

        if (disabledIndex > -1)
        {
            laneRhythmInstance = _activePool[disabledIndex];
            laneRhythmInstance.Initialize(id, spawnPoint.transform.position);
            laneRhythmInstance.SetEnable(true);
        }
        else
        {
            laneRhythmInstance = _factory.Create(_gameSettings.laneRhythmPrefab, spawnPoint.transform.position, parent);
            laneRhythmInstance.Initialize(id);
            laneRhythmInstance.SetEnable(true);
            _activePool.Add(laneRhythmInstance);
        }
        
        return laneRhythmInstance;
    }

    public void Pause()
    {
        foreach (var laneRhythm in _activePool)
        {
            if (laneRhythm.isEnabled)
                laneRhythm.Pause();
        }
    }

    public void DestroyAll()
    {
        for (int i = 0; i < _activePool.Count; i++)
        {
            _activePool[i].SetEnable(false);
        }
    }
    public void Destroy(string id)
    {
        for (int i = 0; i < _activePool.Count; i++)
        {
            if (_activePool[i].id == id)
                _activePool[i].SetEnable(false);
        }
    }
}