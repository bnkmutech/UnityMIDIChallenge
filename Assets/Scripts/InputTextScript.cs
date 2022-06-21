using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputTextScript : MonoBehaviour
{
    [SerializeField] GameObject lane;
    Lane laneScript;
    Text inputText;
    // Start is called before the first frame update
    void Start()
    {
        laneScript = lane.GetComponent<Lane>();
        inputText = GetComponent<Text>();
        inputText.text = laneScript.input.ToString();
    }

}
