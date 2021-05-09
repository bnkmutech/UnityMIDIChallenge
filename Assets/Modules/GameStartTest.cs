using UnityEngine;
using Zenject;

public class GameStartTest : MonoBehaviour
{
    [Inject] private SignalBus _signalBus;
    [SerializeField] private TrackData _trackData;
    
    public void Fire()
    {
        _signalBus.Fire(new TrackSelectSignal(_trackData.difficulty, _trackData));
    }
}