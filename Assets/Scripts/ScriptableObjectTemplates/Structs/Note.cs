using System;
using UnityEngine;

namespace ScriptableObjectTemplates.Structs
{
    [Serializable]
    public struct Note
    {
        public string note;
        public string sound;
        public int midiValue;
        public Color color;
        public int scorePoint;
        public string inputKey;

        public Note(string note, string sound, int midiValue, Color color, int scorePoint, string inputKey)
        {
            this.note = note;
            this.sound = sound;
            this.midiValue = midiValue;
            this.color = color;
            this.scorePoint = scorePoint;
            this.inputKey = inputKey;
        }
    }
}