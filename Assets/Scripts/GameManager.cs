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

    [SerializeField]
    private float noteWidth = 1;

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
            GameObject temp = Instantiate(keyPanelPrefab, new Vector3(offset + index * noteWidth, 0, 0), Quaternion.identity);
            temp.GetComponent<SpriteVisualManager>().label = note.inputKey;
            temp.GetComponent<SpriteVisualManager>().color = note.color;
            temp.GetComponent<SpriteVisualManager>().width = noteWidth;
            index++;
        }
    }
}