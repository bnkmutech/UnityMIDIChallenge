using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

public class SongManager : MonoBehaviour
{
    [SerializeField] private string midiFileName;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private LaneScript[] lanes;

    private AudioSource _audioSource;
    private MidiFile _midiFile;
    private Note[] _notes;

    public System.Action OnPressRestart;

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
        _audioSource.Play();
    }
    void PressSpacebarToReplay()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.Play();
            SendDataToLaneScript();

            OnPressRestart?.Invoke();
        }
    }

    public double GetAudioSourceTime()
    {
        return (double)_audioSource.time;
    }
}
