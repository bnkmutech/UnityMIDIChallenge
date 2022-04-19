using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;





public class GamePlay : Settings
{

  

    public static GamePlay gamePlay;
    
    [SerializeField]
    private Lane[] lanes;

    public double TargetMargin = 0.5;
    public float actualSpeed { get;  set; }
    public int inputDelay { get;  set; }

    public float SpawnDistance = 500f;

 
    [SerializeField]
    private GameObject indicatorObject;

    [Space]
    [SerializeField]
    private string midiLocation;

    private float indicatorPos;






    public float NoteMissed // late or not tapped
    {
        get
        {
            return indicatorPos - (SpawnDistance - indicatorPos);
        }

    }

    public static MidiFile midiFile;



    void OnEnable()
    {

        NoteSpeedSwitch();
        


        indicatorPos = indicatorObject.transform.position.y;


        gamePlay = this;
        if (gamePlay != this)
            Destroy(gameObject);



        midiFile = MidiFile.Read(Application.streamingAssetsPath + "/" + midiLocation);
        ScoreManager.score = 0;
        ScoreManager.comboScore = 0;
        GetDataFromMidi();

    }

    private void Update()
    {
        if (!SoundManager.Music.isPlaying)
        {
            if (GameManager.isPlaying)
            {
                GameManager.isPlaying = false;
            }

        }
    }


    void NoteSpeedSwitch()
    {
     
        
        switch (NoteSpeed)
        {
            case 0:
                actualSpeed = 2f;
                break;
            case 1:
                actualSpeed = 1f;
                break;
            case 2:
                actualSpeed = 0.5f;
                break;
        }

        
    }







    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        var array = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(array, 0);


        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(array);
        }


        StartSong();

      //  Invoke(nameof(StartSong), 0);
       }


    private void StartSong()
    {

        SoundManager.Music.Play();
        GameManager.isPlaying = true;
        if(Metronome == true)
        SoundManager.Metronome.Play();
 
    }

    public static double GetAudioSourceTime()
    {
        return (double)SoundManager.Music.timeSamples / SoundManager.Music.clip.frequency;
    }


}
