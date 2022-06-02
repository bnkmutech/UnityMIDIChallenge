using UnityEngine;

namespace SOTemplate.TrackSO
{
    [CreateAssetMenu(fileName = "New Track", menuName = "SO/Track", order = 0)]
    public class GameTrackSO : ScriptableObject
    {
        public MidiAsset Midi;
        public AudioClip Song;
        
    }
}