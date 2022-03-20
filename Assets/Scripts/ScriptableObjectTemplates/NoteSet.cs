using ScriptableObjectTemplates.Structs;
using UnityEngine;

namespace ScriptableObjectTemplates
{
    [CreateAssetMenu(fileName = "NoteScriptableObject", menuName = "ScriptableObjects/NoteSet")]
    public class NoteSet : ScriptableObject
    {
        public Note[] notes;
    }
}