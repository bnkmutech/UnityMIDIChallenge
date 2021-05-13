using nanoid;

public class RhythmPalette
{
    public bool isLongNote => _isLongNote;
    public string id => _id;
    public string note => _note;
    public float startTime => _startTime;
    public float endTime => _endTime;

    private bool _isLongNote;
    private string _id;
    private string _note;
    private float _startTime;
    private float _endTime;

    public RhythmPalette(float startTime, float endTime, string note)
    {
        _id = NanoId.Generate(2) + NanoId.Generate(2);
        _startTime = startTime;
        _endTime = endTime;
        _note = note;
        _isLongNote = (endTime - startTime) > 1f;
    }
}