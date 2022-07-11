using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    //For Different file use

    //For Editor
    public KeyCode chooseKey;

    //For in file Component
    private Button _button;
    private TextMeshProUGUI _buttonText;
    public Color _col;
    private SongMaster songMaster;
    private Text noteIndicator;

    //For in file variable
    private ColorBlock standardColor;
    public bool isHitZone = false;
    private GameObject notePrefab;


    //==================================================================================================================

    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _col = GetComponent<Image>().color;
        songMaster = GameObject.Find("SongMaster").GetComponent<SongMaster>();
        noteIndicator = GameObject.Find("NoteIndicator").GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _buttonText.SetText(chooseKey.ToString());
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
        }
    }

    public void OnPressed()
    {
        if (isHitZone)
        {
            ColorBlock color = _button.colors;
            color.normalColor = Color.blue;
            _button.colors = color;
            Destroy(notePrefab);
            noteIndicator.text = notePrefab.name;
            noteIndicator.color = songMaster.noteColorData[notePrefab.name];
            songMaster.score += 20;
        }
        else if (!isHitZone)
        {
            ColorBlock failColor = _button.colors;
            failColor.normalColor = Color.red;
            _button.colors = failColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        notePrefab = other.gameObject;
        if (other.gameObject.CompareTag("Note"))
        {
            isHitZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            isHitZone = false;
        }
    }
}
