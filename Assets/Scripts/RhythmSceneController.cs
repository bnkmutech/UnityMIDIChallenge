using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RhythmSceneController : MonoBehaviour
{
    [SerializeField]
    private string drumTrackFileName;
    private byte[] _midiTrackFileBuffer;

    private Playback _playback;
    private OutputDevice _outputDevice;

    void Start()
    {
        // Loading drum track from midi file
        // _midiTrackFileBuffer = File.ReadAllBytes($"{Application.dataPath}/Resources/{drumTrackFileName}.mid");
        
        // Below this please implement scene initialize
        // from MIDI track above to any kind of rhythm game play scene
        // *  please ensure the best approach to draw the scene for memory efficiency 
        // ** any kind of scene is welcome ( both 2D or 3D )
        //
        // Hint for MIDI track 
        // this MIDI track represent 4 bars of drum track in Cha Cha Cha rhythm style
        // Looping for pad note
        // C#2 (37) is rim snare sound
        // C#3 (49) is Cymbal
        // D2  (38) snare
        // C2  (36) is Bass drum
        // C3  (48) is High tom
        // G2  (43) is Floor tom
        // G2  (43) is Floor tom
        
        var midiFile = MidiFile.Read($"{Application.dataPath}/Resources/{drumTrackFileName}.mid");
        _outputDevice = OutputDevice.GetById(0);
        _outputDevice.Volume = new Volume(0);
        _playback = midiFile.GetPlayback(_outputDevice, new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        
        
        _playback.NotesPlaybackFinished += Test;
        _playback.InterruptNotesOnStop = true;
        StartCoroutine(StartMusic());
    }
    private void Test(object sender, NotesEventArgs notesArgs)
    {
        var notesList = notesArgs.Notes;
        foreach (Note item in notesList)
        {
            Debug.Log(item);
        }
    }
    private IEnumerator StartMusic()
    {
        _playback.Start();
        while (_playback.IsRunning)
        {
            
            yield return null;
           
        }
        _playback.Dispose();

    }
    private void OnApplicationQuit()
    {
        _playback.Stop();
        _playback.Dispose();
    }
}
