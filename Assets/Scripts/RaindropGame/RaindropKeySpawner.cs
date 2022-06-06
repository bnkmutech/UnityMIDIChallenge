using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RaindropGame
{
    public class RaindropKeySpawner : MonoBehaviour
    {
        private RaindropKeyRow ParentRow;
        private Color NoteColor = Color.white;
        

        [SerializeField] private GameObject notePrefab;

        private List<float> noteTimeList = new List<float>();
        
        public void InitKeySpawner(RaindropKeyRow parent,Color color)
        {
            ParentRow = parent;
            NoteColor = color;
        }

        public void AddNote(float time)
        {
            noteTimeList.Add(time);
        }

        private void Update()
        {
            if (noteTimeList.Count == 0||!RaindropGameManager.IsPlaying) return;
            float noteSpawnTime = noteTimeList[0];
            if (Time.fixedTime * 1000 >= noteSpawnTime)
            {
                SpawnNote();
            }
        }

        private void SpawnNote()
        {
            noteTimeList.RemoveAt(0);
            RaindropNote newNote = Instantiate(notePrefab, transform).GetComponent<RaindropNote>();
            newNote.InitNote(ParentRow,NoteColor);
            ParentRow.onKeyPressedEvent.AddListener(newNote.CheckKeyHit);
        }
    }
}