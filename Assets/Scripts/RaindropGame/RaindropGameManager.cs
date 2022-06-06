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

        [Header("Note Setting")] [SerializeField] [Range(1.0f, 100.0f)]
        private float noteSpeed; //time delay from note spawn to note hitting judgement bar

        [SerializeField] private int spawnY;
        [SerializeField] private int perfectY;
        [SerializeField] private int hitMargin;

        [Header("Script Setting")] [SerializeField]
        private GameTrackSO GameTrack;

        [SerializeField] private RaindropKeyManager KeyManager;

        [SerializeField] private AudioSource AudioSource;

        #region Instance

        public static RaindropGameManager Instance;

        public static int FixedStartTime;
        public static bool IsPlaying => Instance.isPlaying;
        public static float NoteSpeed => Instance.noteSpeed;

        public static int NoteSpawnY => Instance.spawnY;

        public static int NotePerfectY => Instance.perfectY;

        public static int NoteHitMargin => Instance.hitMargin;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        private void Start()
        {
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
        }

        private void StartGame()
        {
            isPlaying = !isPlaying;

            FixedStartTime = (int)(Time.fixedTime) * 1000;

            LoadTrack();

            int travelDistant = NoteSpawnY - NotePerfectY;
            float noteVelocity = travelDistant / NoteSpeed;
            float fixedDelayTime = travelDistant / noteVelocity * 0.02f;

            //AudioSource.PlayScheduled(FixedStartTime + fixedDelayTime);
            AudioSource.PlayDelayed(FixedDelayTime());
            //AudioSource.PlayDelayed(fixedDelayTime + startDelayMs/1000.0f);
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
                        float timeMs = (time / TrackTPM) + FixedDelayTime()*1000.0f;
                        print("note:"+note+" time:"+timeMs);
                        KeyManager.AddNoteToRow(note, timeMs + FixedStartTime);
                        //StartCoroutine(AddNewNote(note, timeMs + FixedStartTime));
                    }
                }
            }
            print(FixedStartTime);
        }

        private IEnumerator AddNewNote(int note, float time)
        {
            
            yield return new WaitUntil(() => TrackTPM != 0);
            KeyManager.AddNoteToRow(note, time + FixedStartTime);
        }

        private float FixedDelayTime()
        {
            int travelDistant = NoteSpawnY - NotePerfectY;
            float noteVelocity = travelDistant / NoteSpeed;
            float fixedDelayTime = travelDistant / noteVelocity * 0.02f;

            return fixedDelayTime;
        }
    }
}