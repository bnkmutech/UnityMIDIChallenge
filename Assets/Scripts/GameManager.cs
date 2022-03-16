using System;
using ScriptableObjectTemplates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NoteSet currentNoteSet;

    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    private GameObject keyPanelPrefab;

    [SerializeField]
    private float noteWidth = 1;

    private float _pressedModifier = -0.3f;

    private void Start()
    {
        PopulateKeyPanels();
    }

    private void PopulateKeyPanels()
    {
        float offset = (currentNoteSet.notes.Length % 2 == 0) ? noteWidth / 2 : 0;
        int index = currentNoteSet.notes.Length / -2;
        foreach (var note in currentNoteSet.notes)
        {
            GameObject panel = Instantiate(keyPanelPrefab, new Vector3(offset + index * noteWidth, 0, 0), Quaternion.identity);
            if (panel.TryGetComponent(out SpriteVisualManager visualManager))
            {
                visualManager.Label = note.inputKey;
                visualManager.Color = note.color;
                visualManager.PressedColor = new Color(note.color.r, note.color.g, note.color.b, note.color.a + _pressedModifier);
                visualManager.Width = noteWidth;
            }

            InputAction action = new InputAction(note.note, InputActionType.Button, "<Keyboard>/" + note.inputKey.ToLower());
            action.started += ctx =>
            {
                if (panel.TryGetComponent(out KeyPanelManager keyPanelManager))
                {
                    keyPanelManager.ButtonDown();
                }
            };
            action.canceled += ctx =>
            {
                if (panel.TryGetComponent(out KeyPanelManager keyPanelManager))
                {
                    keyPanelManager.ButtonUp();
                }
            };
            action.Enable();

            index++;
        }
    }

    private void OnRestart()
    {
        Debug.Log("spacebar");
    }
}