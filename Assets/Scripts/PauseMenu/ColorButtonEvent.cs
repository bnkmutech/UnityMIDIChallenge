using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonEvent : MonoBehaviour
{
    public void OnColorChange(int buttonNumber)
    {
        List<Button> buttons = GameObject.FindGameObjectWithTag("Canvas").GetComponent<KeyboardReceiver>().GetAllButtons();
        NoteController noteController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>();

        PlayerPrefs.SetInt("buttonNumber", buttonNumber);
        
        gameObject.GetComponent<Button>().interactable = false;
        gameObject.transform.parent.transform.Find("ColorButton" + (buttonNumber == 1 ? 2 : 1)).GetComponent<Button>().interactable = true;

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            buttons[i].GetComponent<Image>().color = gameObject.transform.GetChild(i).GetComponent<Image>().color;
        }

        noteController.ChangeNoteColor();
    }
}
