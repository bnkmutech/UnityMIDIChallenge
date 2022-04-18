using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class KeyCodeUpdate : MonoBehaviour
{
  


    public int keyNumber;

    private TextMeshProUGUI currentKey;

    // Start is called before the first frame update


    private void OnEnable()
    {
        currentKey = GetComponentInChildren<TextMeshProUGUI>();

        KeyCodeChange();
    }
    public void KeyCodeChange()
    {
        currentKey.text = Settings.keyCodeDict[keyNumber].ToString();
     
     
    }

    public void KeyCodeChange(KeyCode a)
    {
        currentKey.text = a.ToString();
    }
}
