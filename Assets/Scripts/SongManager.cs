using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class SongManager : MonoBehaviour
{
    [SerializeField] private string midiFileName;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float _pitch = 1;
    [SerializeField] private float _noteVelocity = 4;
    [SerializeField] private LaneScript[] lanes;
    public float Pitch => _pitch;
    public float NoteVelocity => _noteVelocity;

    private AudioSource _audioSource;
    private MidiFile _midiFile;
    private Note[] _notes;
    private bool _isGamePlaying = true;

    //event
    [SerializeField] private CheckCollision _checkCollision;
    public System.Action OnPressRestart;
    public System.Action OnGameFinish;

    private void OnEnable()
    {
        _checkCollision.OnTouchLine += PlayAudio;
    }
    private void OnDisable()
    {
        _checkCollision.OnTouchLine += PlayAudio;
    }

    // Start is called before the first frame update
    void Start()
    {
        //read midi file
        _midiFile = MidiFile.Read("Assets/AssetData/Midi/"+ midiFileName);

        GetNotesFromMidi();
        SendDataToLaneScript();
        SetAudio();
    }

    // Update is called once per frame
    void Update()
    {
        //check if game round is finish
        if(_isGamePlaying && !_audioSource.isPlaying && (lanes[0].Timer > audioClip.length))
        {
            _isGamePlaying = false;
            OnGameFinish?.Invoke();
        }

        //if round is done then press spacebar to replay
        if (!_isGamePlaying)
        {
            PressSpacebarToReplay();
        }
    }

    void GetNotesFromMidi()
    {
        var notes = _midiFile.GetNotes();
        _notes = new Note[notes.Count];
        notes.CopyTo(_notes, 0);
    }
    void SendDataToLaneScript()
    {
        foreach(var lane in lanes)
        {
            lane.ReceivedMidiData(_notes, _midiFile.GetTempoMap());
        }
    }
    void SetAudio()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = audioClip;
        _audioSource.pitch = _pitch;
    }
    void PressSpacebarToReplay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isGamePlaying = true;
            OnPressRestart?.Invoke();
            SendDataToLaneScript();
        }
    }
    void PlayAudio()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }
}
