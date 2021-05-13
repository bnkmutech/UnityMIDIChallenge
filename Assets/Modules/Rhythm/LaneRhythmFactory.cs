using UnityEngine;
using Zenject;

public class LaneRhythmFactory : IFactory<LaneRhythm, Vector2, Transform, LaneRhythm>
{
    private DiContainer _container;

    public LaneRhythmFactory(DiContainer container)
    {
        _container = container;
    }
    public LaneRhythm Create(LaneRhythm prefab, Vector2 position, Transform parent)
    {
        var instance = _container.InstantiatePrefabForComponent<LaneRhythm>(prefab, position, Quaternion.identity, parent);
        instance.transform.localScale = Vector3.one;
        return instance;
    }
}