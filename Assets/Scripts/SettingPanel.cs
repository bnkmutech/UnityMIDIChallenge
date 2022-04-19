using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class SettingPanel : Settings
{
    // Start is called before the first frame update

   
    [SerializeField]
    private TextMeshProUGUI noteSpeed_txt;
    [SerializeField]
    private TextMeshProUGUI score_txt;


    [SerializeField]
    private GameObject keyPanel;

    // Update is called once per frame
    void Update()
    {
    //    DifficultyUpdate();
        NoteSpeedUpdate();
        ScorePointUpdate();
    }



    void NoteSpeedUpdate()
    {
        switch (NoteSpeed)
        {
            case 0:
                noteSpeed_txt.text = "SLOW";
                break;
            case 1:
                noteSpeed_txt.text = "NORMAL";
                break;
            case 2:
                noteSpeed_txt.text = "FAST";
                break;
        }
    }

    void ScorePointUpdate()
    {
        score_txt.text = scorePoint.ToString();
    }


    public void KeyCodeSet(int a)
    {
        keyPanel.SetActive(true);
        KeyCodeDetector b = keyPanel.GetComponent<KeyCodeDetector>();
        b.boxNumber = a;
    }


    public void NoteSpeed_Add()
    {
        if (NoteSpeed < 2)
            ++NoteSpeed;
    }


    public void NoteSpeed_Substract()
    {
        if (NoteSpeed > 0)
            --NoteSpeed;
    }

    public void ScorePoint_Add()
    {
        scorePoint += 5;
    }

    public void ScorePoint_Substract()
    {
        if (scorePoint > 5)
            scorePoint -= 5;
    }



    public void ColorSet(int a)
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);


        colorDict[a] = new Color(r, g, b);
    }






}
