public class GamePauseSignal
{
    public bool isPause => _isPause;
    private bool _isPause;

    public GamePauseSignal(bool isPause)
    {
        _isPause = isPause;
    }
}

public class GameEndSignal
{
    
}