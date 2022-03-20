using System.Collections;
using NUnit.Framework;
using ScriptableObjectTemplates;
using ScriptableObjectTemplates.Structs;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests.PlayMode
{
    public class GameManagerTest
    {
        [UnityTest]
        public IEnumerator GameManagerMainTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<AudioSource>();
            GameManager gameManager = gameObject.AddComponent<GameManager>();

            GameObject startMessage = new GameObject();
            gameManager.startMessage = startMessage;

            NoteSet noteSet = ScriptableObject.CreateInstance<NoteSet>();
            noteSet.notes = new[]
            {
                new Note("C#2", "rim snare", 37, new Color(0.75f, 0.4f, 1f), 20, "A"),
                new Note("C#3", "cymbal", 49, new Color(0.4f, 0.42f, 1f), 20, "S"),
            };

            PlayableTrack playableTrack = ScriptableObject.CreateInstance<PlayableTrack>();
            playableTrack.noteSet = noteSet;
            playableTrack.audioTrack = null;
            playableTrack.trackDelay = 0;
            playableTrack.midiPath = "Assets/AssetData/Midi/DrumTrack1.mid";
            gameManager.currentTrack = playableTrack;

            GameObject dummyObject = new GameObject();
            dummyObject.transform.position = new Vector3(0, 2, 0);
            gameManager.spawningPoint = dummyObject.transform;
            gameManager.noteLine = dummyObject.transform;

            GameObject notesParent = new GameObject();
            GameObject keyPanelsParent = new GameObject();
            gameManager.notesParent = notesParent.transform;
            gameManager.keyPanelsParent = keyPanelsParent.transform;

            gameManager.notePrefab = dummyObject;
            gameManager.keyPanelPrefab = dummyObject;

            Assert.AreEqual(0, keyPanelsParent.transform.childCount);
            gameManager.StartGame();
            Assert.AreEqual(noteSet.notes.Length, keyPanelsParent.transform.childCount);
            Assert.AreEqual(new Vector3(-0.5f, dummyObject.transform.position.y, dummyObject.transform.position.z), keyPanelsParent.transform.GetChild(0).position);
            Assert.AreEqual(new Vector3(0.5f, dummyObject.transform.position.y, dummyObject.transform.position.z), keyPanelsParent.transform.GetChild(1).position);

            yield return null;
        }
    }
}