using UnityEngine;

namespace SOTemplate.NoteSO
{
    [CreateAssetMenu(fileName = "new Note Line", menuName = "SO/NoteLine", order = 0)]

    public class NoteLineSO : ScriptableObject
    {
        public string note;
        public string sound;
        public int midiValue;
        public Color color;
        public int scorePoint;
        public KeyCode keyCode;
    }
}