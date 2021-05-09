using System.Collections.Generic;
using UnityEngine.Assertions;
using Zenject;

public class GameModeController
{
    [Inject] private SignalBus _signalBus;
    [Inject] private List<GameMode> _modes;

    public void OnTrackSelect(TrackSelectSignal signal)
    {
        if (_modes.Count < 0)
        {
#if UNITY_EDITOR || DEBUG
            Assert.IsTrue(_modes.Count < 0);
#endif
            
            return;
        }
        
        var index = _modes.FindIndex(mode => mode.name == signal.modeName);
        
#if UNITY_EDITOR || DEBUG
        Assert.IsTrue(index < 0);
#endif

        if (index <= 0)
        {
            _signalBus.Fire(new TrackSelectedSignal(_modes[0], signal.trackData));
            return;
        }
        
        _signalBus.Fire(new TrackSelectedSignal(_modes[index], signal.trackData));
    }
}