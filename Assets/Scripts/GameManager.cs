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
    private Transform spawningPoint;

    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    private GameObject keyPanelPrefab;

    [Range(0.5f, 3)]
    [SerializeField]
    private float noteWidth = 1;

    [Range(0.1f, 10)]
    [SerializeField]
    private float noteSpeed = 1;

    private float _pressedModifier = -0.3f;

    private void Start()
    {
        PopulateKeyPanels();
    }

    private Vector3 GetPositionByIndex(int index)
    {
        float origin = -((currentNoteSet.notes.Length - 1) / 2.0f) * noteWidth;
        return new Vector3(origin + index * noteWidth, 0, 0);
    }

    private void PopulateKeyPanels()
    {
        int index = 0;
        foreach (var note in currentNoteSet.notes)
        {
            GameObject panel = Instantiate(keyPanelPrefab, spawningPoint.position + GetPositionByIndex(index), spawningPoint.rotation);
            if (panel.TryGetComponent(out SpriteVisualManager visualManager))
            {
                visualManager.Label = note.inputKey;
                visualManager.Color = note.color;
                visualManager.PressedColor = new Color(note.color.r + _pressedModifier, note.color.g + _pressedModifier, note.color.b + _pressedModifier, note.color.a + _pressedModifier);
                visualManager.Width = noteWidth;
            }

            InputAction action = new InputAction(note.note, InputActionType.Button, $"<Keyboard>/#({note.inputKey.ToLower()})");
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