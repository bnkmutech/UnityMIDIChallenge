using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderEvent : MonoBehaviour
{
    public void OnChangeSize()
    {
        float value = gameObject.GetComponent<Slider>().value;
        Text noteSizeValueText = gameObject.transform.Find("NoteSizeValueText").GetComponent<Text>();
        NoteController noteController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>();

        noteSizeValueText.text = value.ToString();
        noteController.SetNoteSize(value);
        PlayerPrefs.SetFloat("noteSize", value);
        
        foreach(List<GameObject> line in NoteController.notesOnScreen)
        {
            foreach(GameObject note in line)
            {
                note.GetComponent<RectTransform>().sizeDelta = new Vector2(75f, value);
                note.GetComponent<BoxCollider>().size = new Vector3(75f, value, 10f);
            }
        }
    }
}
