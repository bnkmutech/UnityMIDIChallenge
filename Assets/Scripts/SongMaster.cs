using System;
using System.Collections;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;
using UnityEngine;
using System.Collections.Generic;



public class SongMaster : MonoBehaviour
{
    //For Different file use
    public float speed = 5;
    public IDictionary<string, ButtonControl> checkButton = new Dictionary<string, ButtonControl>();

    //For Editor
    [SerializeField] private GameObject notePrefab;

    //For in file Component
    public MidiFile midiFile;
    public AudioSource music;


    //For in file variable
    private float musicTime;
    private List<double> timeStamp = new List<double>();
    private float musicDelay;
    private Note[] arrayNote;
    private Vector3 spawnPoint = new Vector3(-2.49f, 5.5f, 0);
    private float songDelay;


    private float distanceToHit
    {
        get
        {
            return spawnPoint.y - (-1.67f);
        }
    }
    private float timeToHit
    {
        get
        {
            return distanceToHit / speed;
        }
    }

    //==================================================================================================================


    private void Awake()
    {
        midiFile = MidiFile.Read("Assets/Sound/Midi/DrumTrack1.mid");
        music = GetComponent<AudioSource>();
        GetMidiData();
        CalculateMusicDelay();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateButtonChecker();
        StartCoroutine(PlayMusic());
        StartCoroutine(SpawnNote());
    }

    // Update is called once per frame
    void Update()
    {
        musicTime = music.time;
    }

    private void GetMidiData()
    {
        var notes = midiFile.GetNotes();
        arrayNote = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(arrayNote, 0);
        foreach (var note in arrayNote)
        {
            var metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());
            timeStamp.Add((double)metricTime.Minutes * 60f + metricTime.Seconds + (double)metricTime.Milliseconds / 1000f);
        }
    }

    private void CalculateMusicDelay()
    {
        if (timeStamp[0] - timeToHit < 0)
        {
            songDelay = timeToHit - (float)timeStamp[0];
        }
    }

    IEnumerator SpawnNote()
    {
        for (var i = 0; i < timeStamp.Count; i++)
        {
            yield return new WaitUntil(() => musicTime >= (timeStamp[i] - timeToHit));
            GameObject button = GameObject.Find(arrayNote[i].ToString());
            if (button != null)
            {
                spawnPoint.x = button.transform.position.x;
                var note = Instantiate(notePrefab, spawnPoint, Quaternion.identity);
                note.tag = arrayNote[i].ToString();
            }
            else
            {
                Debug.Log("Note: " + arrayNote[i] + " Not available");
            }
        }
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(songDelay);
        music.Play();
    }

    private void CreateButtonChecker()
    {
        Transform parent = GameObject.Find("ButtonPanel").transform;
        foreach (Transform child in parent)
        {
            checkButton.Add(child.name, child.GetComponent<ButtonControl>());
        }
    }
}
