public class NoteInteractSignal
{
    public string id => _id;
    public string noteNote => _noteName;
    private string _id;
    private string _noteName;

    public NoteInteractSignal(string id, string noteName)
    {
        _id = id;
        _noteName = noteName;
    }
}