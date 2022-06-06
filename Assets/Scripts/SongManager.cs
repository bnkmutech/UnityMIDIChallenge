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
    [SerializeField] private CheckCollision _checkCollision;

    private void OnEnable()
    {
        _checkCollision.OnStart += PlayAudio;
    }
    private void OnDisable()
    {
        _checkCollision.OnStart += PlayAudio;
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
        //if music is done then press spacebar to replay
        if (!_audioSource.isPlaying)
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
