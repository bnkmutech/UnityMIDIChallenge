using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class MidiReader : MonoBehaviour
{
    private List<int> midiValueOnButtons = new List<int>{37, 49, 38, 36, 48, 43};
    private List<List<int>> sortedNotes = new List<List<int>>();
    private float initialTime;
    private float timeToReleaseNextNote;
    private int currentNote;

    /* convert midi file to note number and time span by DryWetMidi */
    private void ConvertMidiToText(string midiFilePath, string textFilePath)
    {
        var midiFile = MidiFile.Read(midiFilePath);
        TempoMap tempoMap = midiFile.GetTempoMap();

        string midiText = string.Join(
            "\n",
            midiFile.GetNotes()
            .Select(
                n => $"{n.NoteNumber} {n.TimeAs<MetricTimeSpan>(tempoMap)}"
            )
        );
        ExtractTextToNote(midiText);
    }

    /* turn text to note time and button line number */
    private void ExtractTextToNote(string midiText)
    {
        string[] midiNotes = midiText.Split('\n');

        for(int n = 0; n < midiNotes.Count(); n++)
        {
            string[] noteDesc = midiNotes[n].Split(' ');
            string[] timeSpan = noteDesc[1].Split(':');
            List<int> newNote = new List<int>();
            
            int currentMillisecs = 0;
            
            for(int i = 0; i < timeSpan.Count(); i++)
            {
                if(i < timeSpan.Count() - 1)
                {
                    currentMillisecs += (int.Parse(timeSpan[i]) * (int)(1000 * Mathf.Pow(60, (float)(timeSpan.Count() - 2 - i))));
                }
                else if(i == timeSpan.Count() - 1)
                {
                    currentMillisecs += (int.Parse(timeSpan[i]));
                }
            }
            newNote.Add(currentMillisecs);

            bool flag = false;
            for(int i = 0; i < midiValueOnButtons.Count; i++)
            {
                if(midiValueOnButtons[i] == int.Parse(noteDesc[0]))
                {
                    newNote.Add(i);
                    flag = true;
                    break;
                }
            }
            if(!flag) /* it was not the note in any buttons */
            {
                continue; 
            }

            sortedNotes = InsertionSort(sortedNotes, newNote);
        }
    }

    /* sort note timing (O(n) per a note, but since almost every notes are sorted so insertion sort will have O(1)* per note and summary is O(n)*) */
    public List<List<int>> InsertionSort(List<List<int>> originalList, List<int> insertingList) /* sort by first index of list ([0])*/
    {
        if(originalList.Count == 0)
        {
            originalList.Add(insertingList);
            return originalList;
        }
        for(int i = originalList.Count; i > 0; i--)
        {
            if(insertingList[0] > originalList[i - 1][0])
            {
                if(i == originalList.Count)
                {
                    originalList.Add(insertingList);
                    break;
                }
                else
                {
                    originalList.Insert(i, insertingList);
                    break;
                }
            }
        }
        return originalList;
    }

    private void Start()
    {
        sortedNotes = new List<List<int>>();
        ConvertMidiToText("./Assets/AssetData/Midi/DrumTrack1.mid", "./Assets/AssetData/Midi/test.txt");
        initialTime = Time.time * 1000f;
        timeToReleaseNextNote = Time.time;
        currentNote = 0;
    }

    private void Update()
    {
        if(timeToReleaseNextNote <= Time.time && currentNote < sortedNotes.Count)
        {
            int timeToWaitMs = (int)(sortedNotes[currentNote][0] + initialTime) - (int)(timeToReleaseNextNote * 1000);
            gameObject.GetComponent<NoteController>().GenerateNote(sortedNotes[currentNote][1]);
            timeToReleaseNextNote = Time.time + (timeToWaitMs / 1000f);
            currentNote++;
        }
    }
}
