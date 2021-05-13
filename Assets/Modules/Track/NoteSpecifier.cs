using System;
using System.Collections.Generic;

[Serializable]
public class NoteSpecifier
{
    public List<NoteWithOctave> targetNotes = new List<NoteWithOctave>();

    public NoteSpecifier(NoteSpecifier noteSpecifier)
    {
        for (int i = 0; i < noteSpecifier.targetNotes.Count; i++)
            targetNotes.Add(noteSpecifier.targetNotes[i].Clone());
    }
}