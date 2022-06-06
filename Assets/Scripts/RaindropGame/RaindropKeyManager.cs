using System;
using System.Collections.Generic;
using SOTemplate.NoteSO;
using UnityEngine;

namespace RaindropGame
{
    public class RaindropKeyManager : MonoBehaviour
    {
        [SerializeField] private NoteLineSetSO NoteSet;
        [SerializeField] private RaindropKeyRow[] KeyRows;

        private Dictionary<int, RaindropKeyRow> MidiKeyPair = new Dictionary<int, RaindropKeyRow>();

        private void Start()
        {
            SetUpKeyRows();
        }

        void SetUpKeyRows()
        {
            int index = 0;
            foreach (var note in NoteSet.notesLine)
            {
                MidiKeyPair.Add(note.midiValue, KeyRows[index]);
                KeyRows[index].InitKeyRow(note);
                index++;
            }
        }

        
        public void AddNoteToRow(int note, float time)
        {
            if (MidiKeyPair.ContainsKey(note)) MidiKeyPair[note].AddNote(time);
        }
    }
}