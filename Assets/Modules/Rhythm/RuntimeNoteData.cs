using System;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Note = Melanchall.DryWetMidi.Interaction.Note;

[Serializable]
public class RuntimeNoteData
{
    public NoteName name;
    public int octave;
    public float startTime;
    public float endTime;

    public RuntimeNoteData() { }
    public RuntimeNoteData(Note note, TempoMap tempoMap)
    {
        name = note.NoteName;
        octave = note.Octave;
        startTime += note.TimeAs<MetricTimeSpan>(tempoMap).Milliseconds * 0.01f;
        startTime += note.TimeAs<MetricTimeSpan>(tempoMap).Seconds;
        startTime += note.TimeAs<MetricTimeSpan>(tempoMap).Minutes * 60;
        
        endTime += note.EndTimeAs<MetricTimeSpan>(tempoMap).Milliseconds * 0.01f;
        endTime += note.EndTimeAs<MetricTimeSpan>(tempoMap).Seconds;
        endTime += note.EndTimeAs<MetricTimeSpan>(tempoMap).Minutes * 60;
    }

    public RuntimeNoteData Clone()
    {
        return new RuntimeNoteData(this);
        
    }
    public RuntimeNoteData(RuntimeNoteData data)
    {
        name = data.name;
        octave = data.octave;
        startTime = data.startTime;
        endTime = data.endTime;
    }
}