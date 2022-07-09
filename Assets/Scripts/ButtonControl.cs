using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    //For Different file use


    //For Editor
    [SerializeField] private KeyCode chooseKey;

    //For in file Component
    private Button _button;
    private ColorBlock standardColor;
    private TextMeshProUGUI _text;
    public Color _col;

    //For in file variable


    //==================================================================================================================

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _col = GetComponent<Image>().color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.SetText(chooseKey.ToString());
        standardColor = _button.colors;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(chooseKey))
        {
            _button.onClick.Invoke();

            ColorBlock color = _button.colors;
            color.normalColor = new Color32(25, 147, 255, 255);
            _button.colors = color;
        }
        if (Input.GetKeyUp(chooseKey))
        {
            _button.colors = standardColor;
        }
    }

    public void OnPressed()
    {
        Debug.Log("Pressed");
    }
}
