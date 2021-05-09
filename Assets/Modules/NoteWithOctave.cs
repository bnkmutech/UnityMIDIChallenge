using System;
using Melanchall.DryWetMidi.MusicTheory;
using Note = Melanchall.DryWetMidi.Interaction.Note;

[Serializable]
public class NoteWithOctave
{
    public bool IsMatch(Note note)
    {
        return note.NoteName == name && note.Octave == octave;
    }

    public bool IsMatch(string noteName)
    {
        return $"{name}{octave}" == noteName;
    }
    
    public NoteName name;
    public int octave;
    
    public NoteWithOctave(NoteName name, int octave)
    {
        this.name = name;
        this.octave = octave;
    }
    public NoteWithOctave Clone()
    {
        return new NoteWithOctave(name, octave);
    }

    public bool Equal(Note note)
    {
        return note.NoteName == name && note.Octave == octave;
    }
}