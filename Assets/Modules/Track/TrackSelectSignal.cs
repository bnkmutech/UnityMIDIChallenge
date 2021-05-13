public class TrackSelectSignal
{
    public string modeName => _modeName;
    public TrackData trackData => _trackData;
    private string _modeName;
    private TrackData _trackData;

    public TrackSelectSignal(string modeName, TrackData trackData)
    {
        _modeName = modeName;
        _trackData = trackData;
    }
}