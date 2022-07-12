using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTester
{
    [UnityTest]
    public IEnumerator Test_SongMaster_ColorChanger()
    {
        //เนื่องจากทุก code ผมใช้คำสั่ง GameObject.Find().GetComponent<T>();
        //ผมจึงไม่สามารถ AddComponent และอ้างอิงค่าจากใน Script ได้ ไม่งั้นจะเกิด error object reference.
        //ผมเลยต้องสร้าง Script ใหม่ ก้อปค่าจากตัวจริงมาและอ้างอิงจากในนั้นแทน
        Test_Song_Master testSongMaster = new Test_Song_Master();

        string[] noteName = new string[6] { "C#2", "C#3", "D2", "C2", "C3", "G2" };

        GameObject test = new GameObject();
        test.AddComponent<SpriteRenderer>();

        for (var i = 0; i < noteName.Length; i++)
        {
            test.name = noteName[i];
            testSongMaster.NoteColorChanger(test);
            yield return null;
            Assert.AreEqual(testSongMaster.editorNoteColor[i], test.GetComponent<SpriteRenderer>().material.color);
        }

    }
}
