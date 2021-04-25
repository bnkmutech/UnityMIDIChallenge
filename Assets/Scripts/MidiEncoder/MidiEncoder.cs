using MidiParser;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidiEncoder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void MIDIPARSE() 
    {
        MidiFile midiFile = new MidiFile("song.mid");

        // 0 = single-track, 1 = multi-track, 2 = multi-pattern
        int midiFileformat = midiFile.Format;

        // also known as pulses per quarter note
        int ticksPerQuarterNote = midiFile.TicksPerQuarterNote;

        for (int i = 0; i < midiFile.Tracks.Length; i++)
        {
            MidiTrack track = midiFile.Tracks[i];
            foreach (MidiEvent midiEvent in track.MidiEvents)
            {
                if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    int channel = midiEvent.Channel;
                    int note = midiEvent.Note;
                    int velocity = midiEvent.Velocity;
                }
            }

            foreach (TextEvent textEvent in track.TextEvents)
            {
                if (textEvent.TextEventType == TextEventType.Lyric)
                {
                    int time = textEvent.Time;
                    string text = textEvent.Value;
                }
            }
        }
    }
}
