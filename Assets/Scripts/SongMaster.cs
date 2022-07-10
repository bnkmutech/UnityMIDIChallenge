using System;
using System.Collections;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using System.Collections.Generic;



public class SongMaster : MonoBehaviour
{
    //For Different file use
    public float speed = 5;

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
    private float gameTimer;


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
        double previousTime = 0;

        for (var i = 0; i < timeStamp.Count; i++)
        {
            GameObject button = GameObject.FindGameObjectWithTag(arrayNote[i].ToString());
            if (button != null)
            {
                spawnPoint.x = button.transform.position.x;

                //ถ้าหาก Note ควรจะมาก่อนเพลงเริ่ม
                if (timeStamp[i] - timeToHit < 0)
                {
                    yield return new WaitForSeconds((float)(timeStamp[i] - previousTime));
                    previousTime = timeStamp[i];
                    var note = Instantiate(notePrefab, spawnPoint, Quaternion.identity);
                    note.name = arrayNote[i].ToString();
                    Debug.Log(timeStamp[i]);
                    continue;
                }

                //โน้ตจะออกมาตามเวลาในไฟล์.mid ลบกับเวลาที่ใช้เดินทางถึงปุ่ม
                yield return new WaitUntil(() => musicTime >= (timeStamp[i] - timeToHit));
                {
                    var note = Instantiate(notePrefab, spawnPoint, Quaternion.identity);
                    note.name = arrayNote[i].ToString();
                }
            }

            //ถ้าหากไม่มีโน้ตในปุ่มที่ให้ผู้เล่นกด
            else
            {
                Debug.Log("Button for note " + arrayNote[i] + " not available.");
            }
        }
    }

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(songDelay);
        music.Play();
    }
}
