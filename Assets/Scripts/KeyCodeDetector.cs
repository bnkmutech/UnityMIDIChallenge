using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCodeDetector : Settings
{

   public int boxNumber;
    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log($"{e.keyCode} Detected");

            KeyDetect(e);
        }

    }

     void KeyDetect(Event e)
    {

        if (keyCodeDict.ContainsKey(boxNumber))
        {
            KeyCodeUpdate[] a = FindObjectsOfType<KeyCodeUpdate>();

            foreach (KeyCodeUpdate b in a)
            {
                if (b.keyNumber == (boxNumber))
                    b.KeyCodeChange(e.keyCode);
            }

            keyCodeDict[boxNumber] = e.keyCode;


        }
        this.gameObject.SetActive(false);

    }


}
