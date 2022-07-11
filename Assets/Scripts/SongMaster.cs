using System;
using System.Collections;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//TODO: Add color to note

public class SongMaster : MonoBehaviour
{
    //For Different file use
    public float speed = 5;
    public int score = 0;

    //For Editor
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Color[] noteColorPicker = new Color[6];

    //For in file Component
    public MidiFile midiFile;
    public AudioSource music;
    public Text scoreText;

    //For in file variable
    private float musicTime;
    private List<double> timeStamp = new List<double>();
    private float musicDelay;
    private Note[] arrayNote;
    private Vector3 spawnPoint = new Vector3(-2.49f, 5.5f, 0);
    private float songDelay;
    private float gameTimer;

    //
    public IDictionary<string, Color> noteColorData
    {
        get
        {
            return new Dictionary<string, Color>(){
                {"C#2",noteColorPicker[0]},
                {"C#3",noteColorPicker[1]},
                {"D2",noteColorPicker[2]},
                {"C2",noteColorPicker[3]},
                {"C3",noteColorPicker[4]},
                {"G2",noteColorPicker[5]},
            };
        }
    }

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
        scoreText = GameObject.Find("ScoreBoard").GetComponent<Text>();
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
        scoreText.text = "Score: " + score.ToString();
        musicTime = music.time;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Restarting Game");
        }
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
                }
                else
                {
                    //โน้ตจะออกมาตามเวลาในไฟล์.mid ลบกับเวลาที่ใช้เดินทางถึงปุ่ม
                    yield return new WaitUntil(() => musicTime >= (timeStamp[i] - timeToHit));
                }
                var note = Instantiate(notePrefab, spawnPoint, Quaternion.identity);
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

    IEnumerator PlayMusic()
    {
        yield return new WaitForSeconds(songDelay);
        music.Play();
    }

    private void NoteColorChanger(GameObject ob)
    {
        var obColor = ob.GetComponent<SpriteRenderer>().material;
        obColor.SetColor("_Color", noteColorData[ob.name]);
    }
}
