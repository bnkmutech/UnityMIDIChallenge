using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonControl : MonoBehaviour
{
    //For Editor
    [SerializeField] private KeyCode chooseKey;

    //For in file Component
    private Button button;
    private TextMeshProUGUI buttonText;
    private SongMaster songMaster;
    private Text noteIndicator;

    //For in file variable
    private ColorBlock standardColor;
    private bool isHitZone = false; //Check ว่าโน้ตอยู่ในโซนปุ่มหรือป่าว
    private GameObject notePrefab;


    //==================================================================================================================

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        songMaster = GameObject.Find("SongMaster").GetComponent<SongMaster>();
        noteIndicator = GameObject.Find("NoteIndicator").GetComponent<Text>();
    }

    void Start()
    {
        buttonText.SetText(chooseKey.ToString()); //เปลี่ยนDisplayปุ่มตามKeyที่เลือก
        standardColor = button.colors;
    }

    void Update()
    {
        if (Input.GetKeyDown(chooseKey))
        {
            button.onClick.Invoke();
        }
        if (Input.GetKeyUp(chooseKey))
        {
            button.colors = standardColor;
        }
    }

    //ถ้าหากNoteอยู่ในZoneปุ่ม ตอนกดจะ เปลี่ยนสี, เพิ่มคะแนน, ทำลายPrefab, และแสดงโน้ตในNoteIndicator
    public void OnPressed()
    {
        if (isHitZone)
        {
            ColorBlock color = button.colors;
            color.normalColor = Color.blue;
            button.colors = color;
            Destroy(notePrefab);
            noteIndicator.text = notePrefab.name;
            noteIndicator.color = songMaster.noteColorData[notePrefab.name];
            songMaster.score += 20;
        }
        else if (!isHitZone)
        {
            ColorBlock failColor = button.colors;
            failColor.normalColor = Color.red;
            button.colors = failColor;
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
