using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine;

[Serializable]
public class TrackData : ScriptableObject
{
    public string path => _path;
    public string difficulty => _difficulty;
    public AudioClip clip => _clip;
    public string HasNote(Note note)
    {
        var index =_noteSpecifier.targetNotes.FindIndex(n => n.IsMatch(note));
        
        return index > -1 ? note.ToString() : "";
    }
    
    [SerializeField] private string _path;
    [SerializeField] private string _difficulty;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private NoteSpecifier _noteSpecifier;

    public void Create(string midiPath, AudioClip clip, NoteSpecifier noteSpecifier)
    {
        _path = midiPath;
        _clip = clip;
        _noteSpecifier = noteSpecifier;
    }
}