using UnityEngine;

namespace ScriptableObjectTemplates
{
    [CreateAssetMenu(fileName = "NoteScriptableObject", menuName = "ScriptableObjects/PlayableTrack")]
    public class PlayableTrack : ScriptableObject
    {
        public NoteSet noteSet;
        public AudioClip audioTrack;
        public float trackDelay;
        public string midiPath;
    }
}