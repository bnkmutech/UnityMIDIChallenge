using RhythmGame.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RhythmGame
{
    public class NoteManager : MonoBehaviour
    {
        List<Transform> _spawnNotePoint = new List<Transform>();
        List<NoteController> _noteData = new List<NoteController>();
        [SerializeField] NoteController _notePrefab;
        void Start()
        {
            SpawnPointNote();
            StartCoroutine(Wait());
        }
        public void SpawnPointNote()
        {
            for (int i = 0; i < GameManage.Instance.KeyData.Length; i++)
            {
                var spawnPoint = Instantiate(new GameObject($"spawnPoint{i}"), this.transform);
                spawnPoint.transform.localPosition = new Vector2(GameManage.Instance.StartPointSpawn + (i * 10), 0);
                _spawnNotePoint.Add(spawnPoint.transform);
            }
        }
        IEnumerator Wait()
        {
            while (true)
            {
                SpawnNote();
                yield return new WaitForSeconds(2);
            }
        }

        public void SpawnNote()
        {
            var ran = Random.Range(0, 6);
            var note = GetNote();
            note.transform.SetParent(_spawnNotePoint[ran], false);
            note.SetNote(GameManage.Instance.KeyData[ran].Colour);
            note.gameObject.SetActive(true);
        }

        NoteController GetNote()
        {
            NoteController note;
            if (_noteData.Any(item => !item.isActiveAndEnabled))
            {
                note = _noteData.Where(item => !item.isActiveAndEnabled).FirstOrDefault();
            }
            else
            {
                note = Instantiate(_notePrefab);
                _noteData.Add(note);
            }

            return note;
        }
    }
}
