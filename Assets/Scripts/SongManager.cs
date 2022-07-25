using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    [SerializeField] SongSelection songSelection;
    [SerializeField] public AudioSource song;
    public int inputDelay; //In Milliseconds.
    public float marginOfError; //In Seconds.

    [SerializeField] Lane[] lanes;

    public float noteDisplayTime;
    public float noteSpawnPosY;
    public float noteTapPosY;
    public float noteDespawnPosY
    {
        get
        {
            return noteTapPosY - (noteSpawnPosY - noteTapPosY);
        }
    }

    public enum setSpeed
    {
        Speed_1 = 1,
        Speed_2 = 2,
        Speed_3 = 3,
        Speed_4 = 4,
        Speed_5_EXTREME = 5,
    }
    
    public setSpeed noteSpeed;
    public static int speed;

    [SerializeField] public string midiFilePath;
    public static MidiFile midiFile;

    void Awake()
    {
        speed = (int)noteSpeed;
    }

    void Start()
    {
        Instance = this;
        noteDisplayTime = (speed * (-1f)) + 6f;
        noteSpawnPosY = 500f;
        if(speed == 1)
        {
            noteTapPosY = -275f;
            inputDelay = -720; 
            marginOfError = 0.082f;
        }
        else if(speed == 2) 
        {
            noteTapPosY = -310f;
            inputDelay = -720; 
            marginOfError = 0.078f;
        }
        else if(speed == 3)
        {
            noteTapPosY = -375f;
            inputDelay = -725; 
            marginOfError = 0.06f;
        } 
        else if(speed == 4)
        {
            noteTapPosY = -550f;
            inputDelay = -710; 
            marginOfError = 0.048f;
        } 
        else if(speed == 5)
        {
            noteTapPosY = -1800f;
            inputDelay = -660; 
            marginOfError = 0.07f;
        } 
        marginOfError = Mathf.Round(marginOfError * 1000.0f) * 0.001f;

        song = songSelection.selectedSong;
        midiFile = MidiFile.Read(midiFilePath);
        GetDataFromMidi();
    }

    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var notesArray = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(notesArray, 0);

        foreach(var lane in lanes) lane.SetTimeStamps(notesArray);

        StartCoroutine(DelaySongStart());
    }

    public static double GetSongTime()
    {
        return (double)Instance.song.timeSamples / Instance.song.clip.frequency;
    }

    public IEnumerator DelaySongStart() //To make the song's sound start at the same time in all note speed.
    {
        float delay = 0f;
        if(speed == 1) delay = 0.1f;
        else if(speed == 2) delay = 1f;
        else if(speed == 3) delay = 2f;
        else if(speed == 4) delay = 3f;
        else if(speed == 5) delay = 4f;
        yield return new WaitForSeconds(delay);
        song.Play();
    }
}
