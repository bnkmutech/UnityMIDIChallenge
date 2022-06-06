using UnityEngine;

namespace SOTemplate.NoteSO
{
    [CreateAssetMenu(fileName = "new Note Lines Set", menuName = "SO/NoteLineSet", order = 0)]
    public class NoteLineSetSO : ScriptableObject
    {
        public NoteLineSO[] notesLine;
    }
}