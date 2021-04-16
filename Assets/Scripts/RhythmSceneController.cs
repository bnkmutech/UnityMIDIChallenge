using MidiParser;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RhythmSceneController : MonoBehaviour
{
    [SerializeField]
    private string drumTrackFileName = default;

    [SerializeField]
    private Rigidbody gameCameraRb = default;

    [SerializeField]
    private int countDownSecond = default;

    [SerializeField]
    private NoteChecker noteChecker = default;

    [SerializeField]
    private float noteOffsetX = default;

    [SerializeField]
    private float noteSpeed = default; // x1, x2, x3

    [SerializeField]
    private NoteData[] notePrototypes = default;

    // note
    private Dictionary<int, NoteData> noteDict = default;
    private GameObject notesContainer = default;
    private float noteWidth = default;
    private float noteHeight = default;
    private float ticksPerQuarterNote = default;
    private int bpm = default;

    private byte[] _midiTrackFileBuffer;
    
    private void Start()
    {
        // Loading drum track from midi file
        _midiTrackFileBuffer = File.ReadAllBytes($"{Application.dataPath}/Resources/{drumTrackFileName}.mid");

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

        DetectNoteSize();
        SetCameraToCenter();
        GenerateNoteDictionary();
        GenerateNotes(_midiTrackFileBuffer);

        Invoke("StartGame", countDownSecond);
    }

    private void StartGame()
    {
        float velocityPerSec = (bpm / 60f * noteSpeed);
        noteChecker.Active(gameCameraRb, velocityPerSec);
    }

    private void DetectNoteSize()
    {
        noteWidth = notePrototypes[0].Prototype.transform.localScale.x;
        noteHeight = notePrototypes[0].Prototype.transform.localScale.y;
    }

    private void SetCameraToCenter()
    {
        var oldPos = gameCameraRb.transform.position;
        var firstNoteXPos = noteWidth + noteOffsetX;
        var lastNoteXPos = notePrototypes.Length * firstNoteXPos;

        var xPos = (firstNoteXPos + lastNoteXPos) / 2;
        gameCameraRb.transform.position = new Vector3(xPos, oldPos.y, oldPos.z);
    }

    private void GenerateNoteDictionary()
    {
        noteDict = new Dictionary<int, NoteData>();

        foreach (NoteData note in notePrototypes)
        {
            noteDict.Add(note.Key, note);
        }
    }

    private void GenerateNotes(byte[] midiTrackFileBuffer)
    {
        if (notesContainer == null)
        {
            notesContainer = new GameObject("Notes Container");
        }

        var midiFile = new MidiFile(midiTrackFileBuffer);
        var track = midiFile.Tracks[0];
        ticksPerQuarterNote = midiFile.TicksPerQuarterNote;
        bpm = midiFile.BPM;

        foreach (var midiEvent in track.MidiEvents)
        {
            if (midiEvent.MidiEventType == MidiEventType.NoteOn)
            {
                var noteKey = midiEvent.Note;

                if (noteDict.TryGetValue(noteKey, out NoteData note))
                {
                    Note newNote = Instantiate(note.Prototype, notesContainer.transform);
                    newNote.KeyCode = note.KeyCode;

                    var xPos = note.Lane * (noteWidth + noteOffsetX);
                    var yPos = midiEvent.Time / ticksPerQuarterNote * noteSpeed;
                    newNote.transform.position = new Vector3(xPos, yPos, 0);
                }
            }
        }

        var oldPos = notesContainer.transform.position;
        notesContainer.transform.position = new Vector3(oldPos.x, oldPos.y + (noteHeight / 2), oldPos.z);
    }
}

[Serializable]
public class NoteData
{
    public int Key;
    public KeyCode KeyCode;
    public int Lane;
    public Note Prototype;
}
