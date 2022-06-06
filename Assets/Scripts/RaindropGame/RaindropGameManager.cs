using System;
using System.Collections;
using SOTemplate.TrackSO;
using UnityEngine;
using MidiReader;

namespace RaindropGame
{
    public class RaindropGameManager : MonoBehaviour
    {
        [Header("Game Setting")] [SerializeField]
        private bool isPlaying = false;

        [SerializeField] private int startDelayMs; // start delay in milisec

        [Header("Note Setting")] [SerializeField] [Range(0.1f, 1.0f)]
        public float noteSpeed; //time delay from note spawn to note hitting judgement bar

        [SerializeField] public int spawnY;
        [SerializeField] public int perfectY;
        [SerializeField] public int missY;
        [SerializeField] public int hitMargin;

        [Header("Script Setting")] [SerializeField]
        private GameTrackSO GameTrack;

        [SerializeField] private RaindropKeyManager KeyManager;

        [SerializeField] private ScoreManager ScoreManager;

        [SerializeField] private AudioSource AudioSource;

        [SerializeField] private GameObject startText;
        #region Instance

        public static RaindropGameManager Instance;

        public static float FixedStartTime;
        public static bool IsPlaying => Instance.isPlaying;
        public static float NoteSpeed => Instance.noteSpeed;

        public static int NoteSpawnY => Instance.spawnY;

        public static int NotePerfectY => Instance.perfectY;
        
        public static int NoteMissY => Instance.missY;

        public static int NoteHitMargin => Instance.hitMargin;

        private void Awake()
        {
            Instance = this;
        }

        #endregion


        private void Start()
        {
            KeyManager?.SetUpKeyRows();
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isPlaying)
            {
                StartGame();
            }

            if (isPlaying && !AudioSource.isPlaying)
            {
                isPlaying = !isPlaying;
            }
            
            startText?.SetActive(!isPlaying);
        }

        private void StartGame()
        {
            isPlaying = !isPlaying;

            FixedStartTime = Time.fixedTime * 1000.0f;

            LoadTrack();
            
            AudioSource?.PlayDelayed(1.0f/noteSpeed);

            ScoreManager?.Reset();
        }

        private float TrackTPM = 0;

        void LoadTrack()
        {
            TrackTPM = 0;
            AudioSource.clip = GameTrack.Song;

            var midiFile = new MidiFile(GameTrack.Midi.filePath);

            // also known as pulses per quarter note
            var ticksPerQuarterNote = midiFile.TicksPerQuarterNote;
            Debug.Log("tpq: " + ticksPerQuarterNote);

            foreach (var track in midiFile.Tracks)
            {
                foreach (var midiEvent in track.MidiEvents)
                {
                    if (midiEvent.MidiEventType == MidiEventType.MetaEvent)
                    {
                        var bpm = midiEvent.Arg2;
                        TrackTPM = ticksPerQuarterNote * bpm / 60000.0f;
                        print("tpm: " + TrackTPM);
                    }

                    if (midiEvent.MidiEventType == MidiEventType.NoteOn)
                    {
                        var note = midiEvent.Note;
                        var time = midiEvent.Time;
                        float timeMs = (time / TrackTPM);
                        print("note:"+note+" time:"+timeMs);
                        KeyManager.AddNoteToRow(note, timeMs + FixedStartTime);
                    }
                }
            }
            print(FixedStartTime);
        }
    }
}