using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.SceneManagement;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteRestriction;
    public int noteRestrictionOctave;
    private List<Note> notes = new List<Note>();
    [SerializeField] public List<double> timeStamps = new List<double>(); 
    [SerializeField] GameObject notePrefab;
    [SerializeField] PlayerController player;
    [SerializeField] ScoreManager scoreManager;
    public int noteScore;

    private int spawnCount = 0;
    private int notesReachIndicatorCount = 0;
    private int missCount = 0;

    void Update()
    {
        //Spawning Notes.
        if(spawnCount < timeStamps.Count) //If there're still notess left in midi file.
        {
            if(SongManager.GetSongTime() >= (timeStamps[spawnCount] - SongManager.Instance.noteDisplayTime))
            {
                var note = Instantiate(notePrefab, transform);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnCount]; //Make the note know its position to fall down.
                spawnCount++;
            }
        }

        else if(spawnCount >= timeStamps.Count && SongManager.Instance.song.isPlaying == false)
        {
            Debug.Log("Press 'Space bar' to restart.");
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                ClearLog();
                SceneManager.LoadScene("RhythmGamePlay");
            }
        }

        //Check Player Input.
        if(notesReachIndicatorCount < timeStamps.Count)
        {
            double marginOfError = SongManager.Instance.marginOfError;
            double SongTimeWithInputDelay = SongManager.GetSongTime() - (SongManager.Instance.inputDelay / 1000.0); //Song time minus input delay in seconds.
            
            KeyCode input = KeyCode.None; //To set input of each lanes.
            if(noteRestriction.ToString() == "CSharp" && noteRestrictionOctave == 2) input = player.RimSnare_Button;
            else if(noteRestriction.ToString() == "CSharp" && noteRestrictionOctave == 3) input = player.Cymbal_Button;
            else if(noteRestriction.ToString() == "D" && noteRestrictionOctave == 2) input = player.Snare_Button;
            else if(noteRestriction.ToString() == "C" && noteRestrictionOctave == 2) input = player.BassDrum_Button;
            else if(noteRestriction.ToString() == "C" && noteRestrictionOctave == 3) input = player.HighTom_Button;
            else if(noteRestriction.ToString() == "A" && noteRestrictionOctave == 2) input = player.FloorTom_Button; //MidTom is merged with FloorTom.
            else if(noteRestriction.ToString() == "G" && noteRestrictionOctave == 2) input = player.FloorTom_Button;

            if(Input.GetKeyDown(input))
            {
                if(Math.Abs(SongTimeWithInputDelay - timeStamps[notesReachIndicatorCount]) < marginOfError) //If player hit the note right in time.
                {
                    scoreManager.totalScore = scoreManager.totalScore + noteScore;
                    Destroy(notes[notesReachIndicatorCount].gameObject);
                    notesReachIndicatorCount++;
                }
            }
            else if(timeStamps[notesReachIndicatorCount] + marginOfError <= SongTimeWithInputDelay) //If player miss the note.
            {
                missCount++;
                print("Missed " + input.ToString() + " for " + missCount + " note(s).");
                notesReachIndicatorCount++;
            }
        }
    }

    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] notesArray) //To get timestamps of only desired notes of each lanes.
    {
        foreach(var note in notesArray)
        {
            if(note.NoteName == noteRestriction) 
            {
                if(note.Octave == noteRestrictionOctave)
                {
                    var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                    timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + SongManager.Instance.noteDisplayTime + (double)metricTimeSpan.Milliseconds / 1000f); //Convert all to seconds and add to the timeStamps List.
                }
            }
        }
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
