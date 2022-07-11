using System;
using System.Collections;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class SongMaster : MonoBehaviour
{
    //Editor
    [SerializeField] private GameObject notePrefab;
    [SerializeField] public float speed = 5;
    [SerializeField] public int score = 0;
    [SerializeField] private string filePath = "Assets/Sound/Midi/DrumTrack1.mid";
    [SerializeField] public Color[] editorNoteColor = new Color[6]
    {new Color(177,71,255,255),new Color(69,232,255,255),new Color(146,255,83,255),new Color(255,229,35,255),new Color(255,184,66,255),new Color(255,90,108,255)};

    //In file Component
    public AudioSource song;
    public Text scoreText;
    private MidiFile midiFile;

    //In file variable
    private float songTime;
    private float songDelay;
    private Vector3 noteSpawnPoint = new Vector3(-2.49f, 5.5f, 0);
    private List<double> noteTimestamp = new List<double>();
    private Note[] arrayNote;

    //ยัดค่าสีที่เลือกไว้ใน editor ลงใน dictionary
    public IDictionary<string, Color> noteColorData
    {
        get
        {
            return new Dictionary<string, Color>(){
                {"C#2",editorNoteColor[0]},
                {"C#3",editorNoteColor[1]},
                {"D2",editorNoteColor[2]},
                {"C2",editorNoteColor[3]},
                {"C3",editorNoteColor[4]},
                {"G2",editorNoteColor[5]},
            };
        }
    }

    //คำนวนระยะทางที่ต้องใช้จากจุด Spawn จนถึงปุ่ม
    private float distanceToHit
    {
        get
        {
            return noteSpawnPoint.y - (-1.53f);
        }
    }
    //คำนวนระยะเวลาที่ด้งเดินทางจากจุด Spawn จนถึงปุ่ม
    public float timeToHit
    {
        get
        {
            return distanceToHit / speed;
        }
    }

    //==================================================================================================================


    private void Awake()
    {
        song = GetComponent<AudioSource>();
        scoreText = GameObject.Find("ScoreBoard").GetComponent<Text>();
        GetMidiData();
    }

    void Start()
    {
        StartCoroutine(CalculateDelayAndPlayMusic());
        StartCoroutine(SpawnNote());
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString();
        songTime = song.time;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restarting Game");
        }
    }

    //ดึงค่าโน้ตและเวลาจาก File
    private void GetMidiData()
    {
        midiFile = MidiFile.Read(filePath); //อ่านไฟล์ midi ตามตำแหน่ง Folder
        var notes = midiFile.GetNotes();
        arrayNote = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(arrayNote, 0); //ใส่ค่าโน้ตเข้า array "arrayNote"

        foreach (var note in arrayNote)
        {
            //แปลงเวลาโน้ตเป็น metrictime
            var metricTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, midiFile.GetTempoMap());

            //แปลง metrictime เป็นวินาทีและใส่ลง List "timeStamp"
            noteTimestamp.Add((double)metricTime.Minutes * 60f + metricTime.Seconds + (double)metricTime.Milliseconds / 1000f);
        }
    }

    IEnumerator SpawnNote()
    {
        double previousTime = 0;

        for (var i = 0; i < noteTimestamp.Count; i++)
        {
            GameObject button = GameObject.FindGameObjectWithTag(arrayNote[i].ToString());
            if (button != null)
            {
                noteSpawnPoint.x = button.transform.position.x;

                //ถ้าหากNoteมาก่อนเพลงเริ่มจะใช้เวลาของsongtimeไม่ได้ จึงต้องใช้วิธีนี้
                if (noteTimestamp[i] - timeToHit < 0)
                {
                    yield return new WaitForSeconds((float)(noteTimestamp[i] - previousTime));
                    previousTime = noteTimestamp[i];
                }
                else
                {
                    //โน้ตจะออกมาตามเวลาในไฟล์.mid ลบกับเวลาที่ใช้เดินทางถึงปุ่ม
                    yield return new WaitUntil(() => songTime >= (noteTimestamp[i] - timeToHit));
                }
                var note = Instantiate(notePrefab, noteSpawnPoint, Quaternion.identity);
                note.name = arrayNote[i].ToString();
                NoteColorChanger(note);
            }

            //ถ้าหากเป็นโน้ตที่ไม่มีอยู่ใน Button
            else
            {
                Debug.Log("Button for note " + arrayNote[i] + " not available.");
            }
        }
    }

    //Delayเพลง ถ้าโน้ตควรมาก่อนเพลงจะเริ่ม
    IEnumerator CalculateDelayAndPlayMusic()
    {
        if (noteTimestamp[0] - timeToHit < 0)
        {
            songDelay = timeToHit - (float)noteTimestamp[0];
        }
        yield return new WaitForSeconds(songDelay);
        song.Play();
    }

    //เปลี่ยนสี note ตามค่าที่เก็บไว้ใน Dict "noteColorData"
    private void NoteColorChanger(GameObject ob)
    {
        var obColor = ob.GetComponent<SpriteRenderer>().material;
        obColor.SetColor("_Color", noteColorData[ob.name]);
    }
}
