using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public KeyCode key;
    // Start is called before the first frame update
    // เริ่มใหม่คะแนนเท่ากับ 0
    void Start()
    {
        PlayerPrefs.SetInt("Score",0);
    }

    // Update is called once per fram
    // เมื้อกด spacebar เริ่มเกมใหม่
    void Update()
    {
         if(Input.GetKeyDown(key))
         Application.LoadLevel(0);
    }

    //เลือกคะแนน
    public int GetScore(){
        return score;
    }
}
