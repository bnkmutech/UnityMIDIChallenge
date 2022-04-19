using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Lane : Settings
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    private KeyCode input;

    [SerializeField]
    private int KeyNumber;


    public GameObject notePrefab;
    List<Note> notes = new List<Note>();
    public List<double> timeStamps = new List<double>();

    int spawnIndex =0;
    int inputIndex = 0;


    private void OnEnable()
    {
        input = keyCodeDict[KeyNumber];
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteRestriction)
            {
              var metricTimeSpan =     TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, GamePlay.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
                
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        if(spawnIndex < timeStamps.Count)
        {
            NoteInstantiate_Func();
            
          

         //   Debug.Log($"Name {note.name} Pos {note.transform.position}");


        }

        if(inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = GamePlay.gamePlay.TargetMargin;
            double audioTime = GamePlay.GetAudioSourceTime() - (GamePlay.gamePlay.inputDelay / 1000.0);

            if(Input.GetKeyDown(input))
            {
                KeyPressed_Func(audioTime, timeStamp, marginOfError);
            }

            if(timeStamp + marginOfError <= audioTime)
            {
                Miss();
            }
        }
    }

    void NoteInstantiate_Func()
    {
        if (GamePlay.GetAudioSourceTime() >= timeStamps[spawnIndex] - GamePlay.gamePlay.actualSpeed)
        {
            var note = Instantiate(notePrefab, transform);

            notes.Add(note.GetComponent<Note>());


            note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
            spawnIndex++;
        }
    }

    void KeyPressed_Func(double audioTime, double timeStamp, double marginOfError)
    {
        SoundManager.Press();

        if (Math.Abs(audioTime - timeStamp) < marginOfError)
        {
            Hit();
        }
        else
        {
            Debug.Log($"Hit Inaccurate On {inputIndex} note with {Math.Abs(audioTime - timeStamp)} delay");
        }

    }






    private void Hit()
    {
        ScoreManager.Hit();
        SoundManager.Hit();
        Destroy(notes[inputIndex].gameObject);
        inputIndex++;
    }



    private void Miss()
    {
       ScoreManager.Miss();
    //    SoundManager.Miss();
        Debug.Log($"Missed {inputIndex} note");
        inputIndex++;
    }

  
}
