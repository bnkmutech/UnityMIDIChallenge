using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDownFeedback : MonoBehaviour
{
    private Text _textKey;

    // Start is called before the first frame update
    void Start()
    {
        _textKey = GetComponent<Text>();
    }

    private void OnGUI()
    {
        Event e = Event.current;

        //check keyboard event
        if (e.isKey)
        {
            //check keycode match with text on key panel
            if(e.keyCode.ToString() == _textKey.text)
            {
                //if key hold change color to black
                if (Input.GetKey(e.keyCode))
                {
                    _textKey.color = Color.black;
                }
                else
                {
                    _textKey.color = Color.white;
                }
            }
        }
    }
}
