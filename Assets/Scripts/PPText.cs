using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PPText : MonoBehaviour
{
    public string name;

    void Start()
    {
        
    }
    // Update is called once per frame
    // เปลี่ยน Text เพื่อนับคะแนน
    void Update()
    {
        GetComponent<Text>().text="SCORE:"+PlayerPrefs.GetInt(name)+"";
    }
}
