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
    [SerializeField] private LaneScript[] lanes;
    public float Pitch => _pitch;

    private AudioSource _audioSource;
    private MidiFile _midiFile;
    private Note[] _notes;

    public System.Action OnPressRestart;
    public System.Action OnGameFinish;
    [SerializeField] private CheckCollision _checkCollision;
    private bool _isGamePlaying = true;

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
        if(_isGamePlaying && !_audioSource.isPlaying && (lanes[0].Timer > audioClip.length))
        {
            _isGamePlaying = false;
            OnGameFinish?.Invoke();
        }

        //if music is done then press spacebar to replay
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

    public double GetAudioSourceTime()
    {
        return (double)_audioSource.time;
    }
}
