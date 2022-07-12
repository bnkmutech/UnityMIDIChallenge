using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Song_Master
{
    public Color[] editorNoteColor = new Color[6]
    {new Color(177,71,255,255),new Color(69,232,255,255),new Color(146,255,83,255),new Color(255,229,35,255),new Color(255,184,66,255),new Color(255,90,108,255)};

    public Vector3 noteSpawnPoint = new Vector3(-2.49f, 5.5f, 0);
    public float speed = 5;


    public IDictionary<string, Color> noteColorData
    {
        get
        {
            return new Dictionary<string, Color>(){
                {"C#2",editorNoteColor[0]},
                {"C#3",editorNoteColor[1]},
                {"D2",editorNoteColor[2]},
                {"C2",editorNoteColor[3]},
                {"C3",editorNoteColor[4]},
                {"G2",editorNoteColor[5]},
            };
        }
    }

    public float distanceToHit
    {
        get
        {
            return noteSpawnPoint.y - (-1.53f);
        }
    }
    //คำนวนระยะเวลาที่ด้งเดินทางจากจุด Spawn จนถึงปุ่ม
    public float timeToHit
    {
        get
        {
            return distanceToHit / speed;
        }
    }

    public void NoteColorChanger(GameObject ob)
    {
        var obColor = ob.GetComponent<SpriteRenderer>().material;
        obColor.SetColor("_Color", noteColorData[ob.name]);
    }
}
