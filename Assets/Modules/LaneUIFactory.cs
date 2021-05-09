using UnityEngine;
using Zenject;

public class LaneUIFactory : IFactory<Object, Transform, LaneUI>
{
    private DiContainer _container;

    public LaneUIFactory(DiContainer container)
    {
        _container = container;
    }
    public LaneUI Create(Object prefab, Transform parent)
    {
        return _container.InstantiatePrefabForComponent<LaneUI>(prefab, parent);
    }
}