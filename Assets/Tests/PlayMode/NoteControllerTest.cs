using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class NoteControllerTest
    {
        [UnityTest]
        public IEnumerator NoteControllerTestFallingSpeed()
        {
            GameObject note = new GameObject();
            NoteController noteController = note.AddComponent<NoteController>();

            noteController.startingTime = 0;
            Assert.AreEqual(new Vector3(0, 0, 0), note.transform.position);
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(new Vector3(0, 0, 0), note.transform.position);

            noteController.fallingSpeed = 1f;
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(new Vector3(0, -1, 0), note.transform.position);
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(new Vector3(0, -2, 0), note.transform.position);

            noteController.fallingSpeed = 10f;
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(new Vector3(0, -12, 0), note.transform.position);
        }

        [UnityTest]
        public IEnumerator NoteControllerTestStartingTime()
        {
            GameObject note = new GameObject();
            NoteController noteController = note.AddComponent<NoteController>();

            noteController.fallingSpeed = 1f;
            Assert.AreEqual(new Vector3(0, 0, 0), note.transform.position);
            yield return new WaitForFixedUpdate();
            Assert.AreEqual(new Vector3(0, 0, 0), note.transform.position);

            noteController.startingTime = Time.time + 2f;
            Assert.AreEqual(new Vector3(0, 0, 0), note.transform.position);
            yield return new WaitForSeconds(2f);
            Assert.AreEqual(new Vector3(0, -1, 0), note.transform.position);
        }

        [UnityTest]
        public IEnumerator NoteControllerTestOnKeyHit()
        {
            GameObject note = new GameObject();
            NoteController noteController = note.AddComponent<NoteController>();

            Assert.IsFalse(note == null);
            noteController.OnKeyHit();
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(note == null);

            yield return null;
        }
    }
}