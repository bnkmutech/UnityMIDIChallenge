using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    //For Different file use
    public bool isPressed = false;

    //For Editor
    [SerializeField] private KeyCode chooseKey;

    //For in file Component
    private Button _button;
    private TextMeshProUGUI _text;
    public Color _col;
    private SongMaster songMaster;

    //For in file variable
    private ColorBlock standardColor;


    //==================================================================================================================

    private void Awake()
    {
        _button = GetComponent<Button>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _col = GetComponent<Image>().color;
        songMaster = GameObject.Find("SongMaster").GetComponent<SongMaster>();
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
        }
        if (Input.GetKeyUp(chooseKey))
        {
            _button.colors = standardColor;
            isPressed = false;
        }
    }

    public void OnPressed()
    {
        isPressed = true;
        ColorBlock color = _button.colors;
        color.normalColor = new Color32(25, 147, 255, 255);
        _button.colors = color;
    }
}
