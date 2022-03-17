using System;
using System.Collections.Generic;
using System.Linq;
using Helper;
using ScriptableObjectTemplates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NoteSet currentNoteSet;

    [SerializeField]
    private Transform spawningPoint;

    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    private GameObject keyPanelPrefab;

    [Range(0.5f, 3)]
    [SerializeField]
    private float noteWidth = 1f;

    [Range(0.1f, 10)]
    [SerializeField]
    private float noteSpeed = 1f;

    [Range(1, 10)]
    [SerializeField]
    private float firstNoteOffset = 10f;

    private float _pressedModifier = -0.3f;
    private Dictionary<int, int> midiValueToIndex = new Dictionary<int, int>();
    private Dictionary<int, Color> midiValueToColor = new Dictionary<int, Color>();

    private void Start()
    {
        PopulateKeyPanels();
        PopulateNotes();
    }

    private Vector3 GetPositionByIndex(int index)
    {
        float origin = -((currentNoteSet.notes.Length - 1f) / 2f) * noteWidth;
        return new Vector3(origin + index * noteWidth, 0f, 0f);
    }

    private void PopulateKeyPanels()
    {
        int index = 0;
        foreach (var note in currentNoteSet.notes)
        {
            midiValueToIndex.Add(note.midiValue, index);
            midiValueToColor.Add(note.midiValue, note.color);

            GameObject panel = Instantiate(keyPanelPrefab, spawningPoint.position + GetPositionByIndex(index), spawningPoint.rotation);
            if (panel.TryGetComponent(out VisualController visualController))
            {
                visualController.Label = note.inputKey;
                visualController.Color = note.color;
                visualController.PressedColor = new Color(note.color.r + _pressedModifier, note.color.g + _pressedModifier, note.color.b + _pressedModifier, note.color.a + _pressedModifier);
                visualController.Width = noteWidth;
            }

            InputAction action = new InputAction(note.note, InputActionType.Button, $"<Keyboard>/#({note.inputKey.ToLower()})");
            action.started += ctx =>
            {
                if (panel.TryGetComponent(out KeyPanelController keyPanelController))
                {
                    keyPanelController.ButtonDown();
                }
            };
            action.canceled += ctx =>
            {
                if (panel.TryGetComponent(out KeyPanelController keyPanelController))
                {
                    keyPanelController.ButtonUp();
                }
            };
            action.Enable();

            index++;
        }
    }

    private void PopulateNotes()
    {
        MidiFile midiFile = new MidiFile("./Assets/AssetData/Midi/DrumTrack1.mid");
        foreach (var midiEvent in midiFile.Tracks[0].MidiEvents)
        {
            if (midiEvent.MidiEventType == MidiEventType.NoteOn)
            {
                if (midiValueToIndex.ContainsKey(midiEvent.Note))
                {
                    GameObject note = Instantiate(notePrefab, spawningPoint.position + new Vector3(0f, firstNoteOffset + (midiEvent.Time / (2000f / noteSpeed)), 0f) + GetPositionByIndex(midiValueToIndex[midiEvent.Note]), spawningPoint.rotation);
                    if (note.TryGetComponent(out VisualController visualController))
                    {
                        visualController.Color = midiValueToColor[midiEvent.Note];
                        visualController.Width = noteWidth;
                    }
                }
            }
        }

        Debug.Log(Math.Abs(spawningPoint.position.y + firstNoteOffset) / noteSpeed);
    }

    private void OnRestart()
    {
        Debug.Log("spacebar");
    }
}