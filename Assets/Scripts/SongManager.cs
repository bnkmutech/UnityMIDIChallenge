using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;

public class SongManager : MonoBehaviour
{
    public string midiFileName;
    public AudioClip audioClip;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //read midi file
        var midiFile = MidiFile.Read("Assets/AssetData/Midi/"+ midiFileName);

        GetNoteArrayFromMidi(midiFile);
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

    void GetNoteArrayFromMidi(MidiFile midiFile)
    {
        //get every note in midi file
        var notes = midiFile.GetNotes();
        var noteArray = new Melanchall.DryWetMidi.Interaction.Note[notes.Count];
        notes.CopyTo(noteArray, 0);
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
        }
    }
}
