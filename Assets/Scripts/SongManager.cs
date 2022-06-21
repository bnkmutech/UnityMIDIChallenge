using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.Events;
using UnityEditor;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
    public AudioSource audioSource;
    public Lane[] lanes;
    public static MidiFile midiFile;
    public string fileLocation;
    [Range(0.01f, 1.99f)] public float noteSpeedMultiplier;
    

    [HideInInspector] public double marginOfError = 0.08;
    [HideInInspector] public float noteTime;
    [HideInInspector] public float songDelayInSeconds;
    [HideInInspector] public float noteSpawnY = 7;
    [HideInInspector] public float noteTapY = -1.5f;
    [HideInInspector] public float noteDespawnY;
    [HideInInspector] public float unrestartableDelay;

    UnityEvent gameEndedEvent;
    Melanchall.DryWetMidi.Interaction.Note[] noteArray;

    private void Awake()
    {
        noteDespawnY = noteTapY - (noteSpawnY - noteTapY);
    }
    void Start()
    {


        if (Instance == null)
        {

            Instance = this;

        }
        else if (Instance != this)
        {

            Destroy(gameObject);

        }

        noteTime = Mathf.Abs(((noteTime * noteSpeedMultiplier) - 1) - 1);

        if(gameEndedEvent == null)
        {
            gameEndedEvent = new UnityEvent();
        }
        gameEndedEvent.AddListener(GameManager.Instance.GameHasEnded);
        
        songDelayInSeconds = noteTime * 2;

        ReadFromFile();

    }

    private void ReadFromFile()
    {
        midiFile = MidiFile.Read(fileLocation);
        GetDataFromMidi();
    }
    public void GetDataFromMidi()
    {
        var notes = midiFile.GetNotes();
        noteArray = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(noteArray, 0);

        foreach (var lane in lanes)
        {
            lane.SetTimeStamps(noteArray);
            lane.ReceiveStartPosition(noteSpawnY, noteTapY);
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }
    public void StartSong()
    {
        audioSource.Play();
    }
    public static double GetAudioSourceTime()
    {
        return (double)Instance.audioSource.timeSamples / Instance.audioSource.clip.frequency;
    }

    public void Reset()
    {
        audioSource.Stop();

        foreach (var lane in lanes)
        {
            lane.Reset();
            lane.SetTimeStamps(noteArray);
            lane.ReceiveStartPosition(noteSpawnY, noteTapY);
        }

        Invoke(nameof(StartSong), songDelayInSeconds);
    }

    void Update()
    {

        if (unrestartableDelay > audioSource.clip.length + songDelayInSeconds)
        {
            gameEndedEvent.Invoke();
        }
        else
        {
            unrestartableDelay += Time.deltaTime;
        }

    }
    

}
