using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanelScript : MonoBehaviour
{
    [SerializeField] private Lane[] _lanes;
    [SerializeField] private Button[] _buttons;

    // Start is called before the first frame update
    void Start()
    {
        ChangeEachButtonInfo();
    }

    void ChangeEachButtonInfo()
    {
        for (int i = 0; i < 6; i++)
        {
            _buttons[i].GetComponent<Image>().color = _lanes[i].LaneColor;
            _buttons[i].GetComponentInChildren<Text>().text = _lanes[i].InputKey.ToString();
        }
    }
}
