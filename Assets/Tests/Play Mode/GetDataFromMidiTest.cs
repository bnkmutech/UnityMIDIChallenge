using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class GetDataFromMidiTest
{
    [UnityTest]
    public IEnumerator Read_Notes_From_MidiFile() //This process is a part from SongManager.GetDataFromMidi()
    {
        MidiFile midiFile;
        midiFile = MidiFile.Read("Assets/AssetData/Midi/DrumTrack1.mid");

        var notes = midiFile.GetNotes();
        var notesArray = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(notesArray, 0);
        
        yield return null;

        Debug.Log("List of All Notes in the Midi File.");

        foreach (var note in notesArray) Debug.Log(note);
        
        Assert.IsNotNull(notes);
    }

    //Sorry, I can't write other unit test.
}
