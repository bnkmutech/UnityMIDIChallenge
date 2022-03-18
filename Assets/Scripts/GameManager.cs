using System.Collections.Generic;
using Helper;
using ScriptableObjectTemplates;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayableTrack currentTrack;

    [SerializeField]
    private Transform spawningPoint;

    [SerializeField]
    private Transform noteLine;

    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    private GameObject keyPanelPrefab;

    [SerializeField]
    private Transform notesParent;

    [SerializeField]
    private Transform keyPanelsParent;

    [SerializeField]
    private float startDelay = 1f;

    [Range(0.5f, 3f)]
    [SerializeField]
    private float noteWidth = 1f;

    [Range(1f, 10f)]
    [SerializeField]
    private float noteSpeed = 1f;

    [Range(0f, 10f)]
    [SerializeField]
    private float firstNoteOffset = 3f;

    private float _pressedModifier = -0.3f;
    private Dictionary<int, int> midiValueToIndex = new Dictionary<int, int>();
    private Dictionary<int, Color> midiValueToColor = new Dictionary<int, Color>();

    private void StartGame()
    {
        PopulateKeyPanels();
        PopulateNotes();
    }

    private Vector3 GetPositionByIndex(int index)
    {
        float origin = -((currentTrack.noteSet.notes.Length - 1f) / 2f) * noteWidth;
        return new Vector3(origin + index * noteWidth, 0f, 0f);
    }

    private void PopulateKeyPanels()
    {
        if (keyPanelsParent.childCount > 0)
        {
            midiValueToColor.Clear();
            midiValueToIndex.Clear();
        }

        foreach (Transform child in keyPanelsParent)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (var note in currentTrack.noteSet.notes)
        {
            midiValueToIndex.Add(note.midiValue, index);
            midiValueToColor.Add(note.midiValue, note.color);

            GameObject panel = Instantiate(keyPanelPrefab, spawningPoint.position + GetPositionByIndex(index), spawningPoint.rotation);
            panel.transform.SetParent(keyPanelsParent);
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
        foreach (Transform child in notesParent)
        {
            Destroy(child.gameObject);
        }

        MidiFile midiFile = new MidiFile(currentTrack.midiPath);
        float durationPerBeat = 0f;
        foreach (var midiEvent in midiFile.Tracks[0].MidiEvents)
        {
            if (midiEvent.MidiEventType == MidiEventType.MetaEvent)
            {
                durationPerBeat = 60f / midiEvent.Arg2;
            }

            if (midiEvent.MidiEventType == MidiEventType.NoteOn)
            {
                if (midiValueToIndex.ContainsKey(midiEvent.Note))
                {
                    GameObject note = Instantiate(notePrefab, noteLine.position + new Vector3(0f, ((float) midiEvent.Time / midiFile.TicksPerQuarterNote + firstNoteOffset) * noteSpeed, 0f) + GetPositionByIndex(midiValueToIndex[midiEvent.Note]), noteLine.rotation);
                    note.transform.SetParent(notesParent);
                    if (note.TryGetComponent(out VisualController visualController))
                    {
                        visualController.Color = midiValueToColor[midiEvent.Note];
                        visualController.Width = noteWidth;
                    }

                    if (note.TryGetComponent(out NoteController noteController))
                    {
                        noteController.fallingSpeed = noteSpeed / durationPerBeat * Time.fixedDeltaTime;
                        noteController.startingTime = Time.fixedTime + startDelay;
                    }
                }
            }
        }

        GetComponent<AudioSource>().clip = currentTrack.audioTrack;
        GetComponent<AudioSource>().PlayDelayed(startDelay + currentTrack.trackDelay + firstNoteOffset * durationPerBeat);
    }

    private void OnRestart()
    {
        StartGame();
    }
}