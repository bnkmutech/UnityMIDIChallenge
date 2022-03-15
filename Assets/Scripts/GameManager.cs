using System;
using ScriptableObjectTemplates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NoteSet currentNoteSet;

    [SerializeField]
    private GameObject notePrefab;

    [SerializeField]
    private GameObject keyPanelPrefab;

    private float _keyPanelSpacing = 1.0f;

    private void Start()
    {
        PopulateKeyPanels();
    }

    private void PopulateKeyPanels()
    {
        float offset = (currentNoteSet.notes.Length % 2 == 0) ? _keyPanelSpacing / 2 : 0;
        int index = currentNoteSet.notes.Length / -2;
        foreach (var note in currentNoteSet.notes)
        {
            GameObject temp = Instantiate(keyPanelPrefab, new Vector3(offset + index * _keyPanelSpacing, 0, 0), Quaternion.identity);
            temp.GetComponent<SpriteVisualManager>().label = note.inputKey;
            temp.GetComponent<SpriteVisualManager>().color = note.color;
            index++;
        }
    }
}