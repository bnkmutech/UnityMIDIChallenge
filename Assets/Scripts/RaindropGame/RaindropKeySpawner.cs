using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RaindropGame
{
    public class RaindropKeySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject notePrefab;

        private List<float> noteTimeList = new List<float>();
        
        public UnityEvent KeyPressEvent;

        private void Start()
        {
            KeyPressEvent = new UnityEvent();
        }

        public void AddNote(float time)
        {
            noteTimeList.Add(time);
        }

        private void Update()
        {
            if (noteTimeList.Count == 0 || !RaindropGameManager.IsPlaying) return;
            float noteSpawnTime = noteTimeList[0];
            if (Time.fixedTime*1000 >= noteSpawnTime )
            {
                noteTimeList.RemoveAt(0);
                RaindropNote newNote = Instantiate(notePrefab,transform).GetComponent<RaindropNote>();
                KeyPressEvent.AddListener(newNote.CheckKeyHit);
                //spawnNote.Add(newNote);
            }
        }
    }
}