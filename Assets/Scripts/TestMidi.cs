using System;
using UnityEngine;
using MidiReader;
using SOTemplate.TrackSO;

namespace DefaultNamespace
{
    public class TestMidi : MonoBehaviour
    {
        public GameTrackSO GameTrack;

        private void Start()
        {
            
            print(GameTrack.Midi.filePath);
        var midiFile = new MidiFile(GameTrack.Midi.filePath);
        
        // 0 = single-track, 1 = multi-track, 2 = multi-pattern
        var midiFileformat = midiFile.Format;
        
        // also known as pulses per quarter note
        var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;
        Debug.Log("tpq: " + ticksPerQuarterNote);
        
        foreach(var track in midiFile.Tracks)
        {
            foreach(var midiEvent in track.MidiEvents)
            {
                if(midiEvent.MidiEventType == MidiEventType.NoteOn)
                {
                    var channel = midiEvent.Channel;
                    var note = midiEvent.Note;
                    var time = midiEvent.Time;
                    var velo = midiEvent.Velocity;
                    Debug.Log("Note ON: " + note + " Time: "+time + "velo: "+velo);
                }

                if (midiEvent.MidiEventType == MidiEventType.NoteOff)
                {
                    var channel = midiEvent.Channel;
                    var note = midiEvent.Note;
                    var time = midiEvent.Time;
                    Debug.Log("Note OFF: " + note + " Time: "+time + "Channel: "+channel);
                }
            }
        
            foreach(var textEvent in track.TextEvents)
            {
                if(textEvent.TextEventType == TextEventType.Lyric)
                {
                    var time = textEvent.Time;
                    var text = textEvent.Value;
                }
            }    
        }
        }
    }
}