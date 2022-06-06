using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using RaindropGame;
using SOTemplate.NoteSO;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Test.PlayModeTests
{
    public class NoteFallTest
    {
        [UnityTest]
        public IEnumerator NoteFallTestWithEnumeratorPasses()
        {
            
            GameObject dummyGameObj = new GameObject();
            RaindropGameManager gameManager = dummyGameObj.AddComponent<RaindropGameManager>();
            gameManager.noteSpeed = 1.0f;
            gameManager.spawnY = 200;
            gameManager.perfectY = 100;
            gameManager.missY = 0;
            
            RaindropKeyRow dummyKeyRow = dummyGameObj.AddComponent<RaindropKeyRow>();
            
            GameObject NoteObject = new GameObject();
            NoteObject.AddComponent<RectTransform>();
            NoteObject.AddComponent<Image>();
            
            RaindropNote Note = NoteObject.AddComponent<RaindropNote>();
            Note.InitNote(dummyKeyRow,Color.white);
            
            yield return new WaitForSeconds(gameManager.noteSpeed); 
            Assert.AreEqual(100, Note.rectTransform.anchoredPosition3D.y);
        }
    }
}