using System;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.MusicTheory;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrackProcessor))]
public class TrackProcessorEditor : Editor
{
    private DefaultAsset _midi;
    private AudioClip _clip;
    private TrackProcessor _trackProcessor;
    private NoteName _newNoteName;
    private int _newNoteNumber;
    private List<bool> _noteListToggle = new List<bool>();
    private void OnEnable()
    {
        _trackProcessor = (TrackProcessor)target;

        for (int i = 0; i < _trackProcessor.noteSpecifier.targetNotes.Count; i++)
            _noteListToggle.Add(false);
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        
        EditorGUILayout.BeginVertical("Button");
        GUILayout.Label("MIDI", EditorStyles.largeLabel);
        _midi = (DefaultAsset)EditorGUILayout.ObjectField(_midi, typeof(DefaultAsset), false);
        EditorGUILayout.Space(5);
        
        GUILayout.Label("Actual track", EditorStyles.largeLabel);
        _clip = (AudioClip)EditorGUILayout.ObjectField(_clip, typeof(AudioClip), false);
        EditorGUILayout.Space(5);

        if (GUILayout.Button("Create track data"))
        {
            var path = AssetDatabase.GetAssetPath(_midi);
            path = path.Replace(_midi.name + ".mid", "");
            var newTrackData = CreateInstance<TrackData>();
            newTrackData.Create(AssetDatabase.GetAssetPath(_midi), _clip, _trackProcessor.noteSpecifier);
            AssetDatabase.CreateAsset(newTrackData, $"{path}{_midi.name}_track_data.asset");
            
            EditorUtility.SetDirty(newTrackData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.Space(5);
        
        EditorGUILayout.BeginVertical("Button");
        GUILayout.Label("Note specifier", EditorStyles.largeLabel);
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginVertical("Button");
        GUILayout.Label($"{_newNoteName}{_newNoteNumber}", EditorStyles.largeLabel);
        EditorGUILayout.EndVertical();
        _newNoteName = (NoteName)EditorGUILayout.EnumPopup(_newNoteName);
        _newNoteNumber = EditorGUILayout.IntSlider(_newNoteNumber, 0, 12);
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Add note"))
        {
            _trackProcessor.noteSpecifier.targetNotes.Add(new NoteWithOctave(_newNoteName, _newNoteNumber));
            _noteListToggle.Add(false);
        }
        
        EditorGUILayout.Space(5);
        
        EditorGUILayout.BeginVertical("Button");
        GUILayout.Label($"Note List", EditorStyles.largeLabel);
        EditorGUILayout.EndVertical();

        for (int i = 0; i < _trackProcessor.noteSpecifier.targetNotes.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label($"{_trackProcessor.noteSpecifier.targetNotes[i].name}{_trackProcessor.noteSpecifier.targetNotes[i].octave}");
            if (GUILayout.Button($"Edit {_trackProcessor.noteSpecifier.targetNotes[i].name}{_trackProcessor.noteSpecifier.targetNotes[i].octave}"))
                _noteListToggle[i] = !_noteListToggle[i];
            
            EditorGUILayout.EndHorizontal();

            if (!_noteListToggle[i])
                continue;

            _trackProcessor.noteSpecifier.targetNotes[i].name = (NoteName)EditorGUILayout.EnumPopup(_trackProcessor.noteSpecifier.targetNotes[i].name);
            _trackProcessor.noteSpecifier.targetNotes[i].octave = EditorGUILayout.IntSlider( _trackProcessor.noteSpecifier.targetNotes[i].octave, 0, 12);

            if (GUILayout.Button($"Delete {_trackProcessor.noteSpecifier.targetNotes[i].name}{_trackProcessor.noteSpecifier.targetNotes[i].octave}"))
            {
                _trackProcessor.noteSpecifier.targetNotes.RemoveAt(i);
                _noteListToggle.RemoveAt(i);
            }
        }

        EditorGUILayout.EndVertical();
        
        EditorUtility.SetDirty(_trackProcessor);
    }
}