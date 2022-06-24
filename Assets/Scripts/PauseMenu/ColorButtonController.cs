using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorButtonController : MonoBehaviour
{
    private int buttonNumber;
    public void Start()
    {
        List<Color> noteColors = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>().GetNoteColors();
        Transform button1 = gameObject.transform.Find("PauseMenu").transform.Find("ColorButton1");


        for(int i = 0; i < button1.childCount; i++)        
        {
            button1.GetChild(i).GetComponent<Image>().color = noteColors[i];
        }

        if(PlayerPrefs.HasKey("buttonNumber"))
        {
            buttonNumber = PlayerPrefs.GetInt("buttonNumber");
        }
        else
        {
            buttonNumber = 1;
        }
        ExecuteEvents.Execute(
            gameObject.transform.Find("PauseMenu").transform.Find("ColorButton" + buttonNumber).gameObject, 
            new PointerEventData(EventSystem.current), 
            ExecuteEvents.submitHandler
        );
    }
}
