using System;
using UnityEngine;

namespace ScriptableObjectTemplates
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
    }
}