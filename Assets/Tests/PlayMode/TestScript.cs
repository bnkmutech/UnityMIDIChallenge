using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestScript
{
    [UnityTest]
    public IEnumerator NoteSpawnPositionTest()
    {
        SceneManager.LoadScene("RhythmGamePlay");
        yield return null;

        SongManager songManager = GameObject.Find("SongManager").GetComponent<SongManager>();
        Assert.AreEqual(17, (Math.Abs(songManager.noteSpawnY) + Math.Abs(songManager.noteTapY)) * 2);
    }
    [UnityTest]
    public IEnumerator NoteIsScrollingTest()
    {
        SceneManager.LoadScene("RhythmGamePlay");
        yield return null;
        SongManager songManager = GameObject.Find("SongManager").GetComponent<SongManager>();
        GameObject laneTest = new GameObject();
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Note.prefab");
        var note = GameObject.Instantiate(prefab, new Vector3(laneTest.transform.position.x, songManager.noteSpawnY, laneTest.transform.position.z), Quaternion.identity);
        note.GetComponent<Note>().assignedTime = 0;
        yield return new WaitForSeconds(songManager.songDelayInSeconds);
        Assert.GreaterOrEqual(-1.5f, note.transform.position.y);
    }
    [UnityTest]
    public IEnumerator RestartGameTest()
    {
        SceneManager.LoadScene("RhythmGamePlay");
        yield return null;
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SongManager songManager = GameObject.Find("SongManager").GetComponent<SongManager>();
        if (songManager.unrestartableDelay > songManager.audioSource.clip.length + songManager.songDelayInSeconds)
        {
            Assert.IsTrue(gameManager.gameHasEnded);
        }
            
    }

}