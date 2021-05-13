public class TrackSelectedSignal
{
    public GameMode mode => _mode;
    public TrackData trackData => _trackData;
    private GameMode _mode;
    private TrackData _trackData;

    public TrackSelectedSignal(GameMode mode, TrackData trackData)
    {
        _mode = mode;
        _trackData = trackData;
    }
}