using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Melanchall.DryWetMidi.MusicTheory.NoteName noteName;
    public int noteOctave;
    public KeyCode input;
    public GameObject notePrefabToSpawn;
    List<Note> notes = new List<Note>();
    [HideInInspector] public List<double> timeStamps = new List<double>();

    int spawnIndex = 0;
    int inputIndex = 0;

    float elapsedTime;
    [HideInInspector] public Vector3 startPos;

    float noteSpawnY;
    float noteTapY;

    [SerializeField] public GameObject laneBlock;
    public Color laneColorAndNote;

    public int noteScore;
    void Start()
    {
       laneBlock.GetComponent<SpriteRenderer>().color = laneColorAndNote;
       startPos = new Vector3(transform.position.x, (Math.Abs(noteSpawnY) + Math.Abs(noteTapY)) * 2, transform.position.z);

    }
    public void SetTimeStamps(Melanchall.DryWetMidi.Interaction.Note[] array)
    {
        foreach (var note in array)
        {
            if (note.NoteName == noteName && note.Octave == noteOctave)
            {
                var metricTimeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, SongManager.midiFile.GetTempoMap());
                timeStamps.Add((double)metricTimeSpan.Minutes * 60f + metricTimeSpan.Seconds + (double)metricTimeSpan.Milliseconds / 1000f);
            }
        }
    }
    void Update()
    {
        OnStartDelay();

        if (spawnIndex < timeStamps.Count)
        {
            if (SongManager.GetAudioSourceTime() >= timeStamps[spawnIndex] - SongManager.Instance.noteTime)
            {
                var note = Instantiate(notePrefabToSpawn, new Vector3(transform.position.x, noteSpawnY, transform.position.z), Quaternion.identity);
                note.transform.parent = transform;
                note.GetComponent<Note>().SetColor(laneColorAndNote);
                notes.Add(note.GetComponent<Note>());
                note.GetComponent<Note>().assignedTime = (float)timeStamps[spawnIndex];
                spawnIndex++;
            }
        }

        if (inputIndex < timeStamps.Count)
        {
            double timeStamp = timeStamps[inputIndex];
            double marginOfError = SongManager.Instance.marginOfError;
            double audioTime = SongManager.GetAudioSourceTime();
            if (Input.GetKeyDown(input) && SongManager.Instance.unrestartableDelay > Math.Abs(SongManager.Instance.songDelayInSeconds - SongManager.Instance.marginOfError))
            {
                if (Math.Abs(audioTime - timeStamp) < marginOfError)
                {
                    Hit();
                    Destroy(notes[inputIndex].gameObject);
                    inputIndex++;
                }
            }
            if (timeStamp + marginOfError <= audioTime)
            {
                inputIndex++;
            }


        }

    }
    public void ReceiveStartPosition(float noteSpawnY, float noteTapY)
    {
        this.noteSpawnY = noteSpawnY;
        this.noteTapY = noteTapY;
    }
    void OnStartDelay()
    {
        elapsedTime += Time.deltaTime;
        float percentTilComplete = elapsedTime / SongManager.Instance.songDelayInSeconds;
        transform.position = Vector3.Lerp(startPos, new Vector3(transform.position.x, 0, transform.position.z), percentTilComplete);
    }
    public void Reset()
    {
        notes.Clear();
        timeStamps.Clear();
        spawnIndex = 0;
        inputIndex = 0;
        elapsedTime = 0;
    }
    private void Hit()
    {
        ScoreManager.Instance.Hit(noteScore);
    }
}
