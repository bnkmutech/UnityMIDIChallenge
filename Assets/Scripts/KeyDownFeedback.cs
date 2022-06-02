using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDownFeedback : MonoBehaviour
{
    public Text textKey;

    // Start is called before the first frame update
    void Start()
    {
        textKey = GetComponent<Text>();
    }

    private void OnGUI()
    {
        Event e = Event.current;

        //check keyboard event
        if (e.isKey)
        {
            //check keycode match with text on key panel
            if(e.keyCode.ToString() == textKey.text)
            {
                //if key hold change color to black
                if (Input.GetKey(e.keyCode))
                {
                    textKey.color = Color.black;
                }
                else
                {
                    textKey.color = Color.white;
                }
            }
        }
    }
}
