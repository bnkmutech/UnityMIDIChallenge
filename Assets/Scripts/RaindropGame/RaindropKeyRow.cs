using System;
using System.Collections.Generic;
using SOTemplate.NoteSO;
using UnityEngine;
using UnityEngine.Events;

namespace RaindropGame
{
    public class RaindropKeyRow : MonoBehaviour
    {
        private bool isInit = false;
        
        [Header("Setting")]
        [SerializeField] private RaindropKeyIndicator KeyIndicator;

        [SerializeField] private RaindropKeySpawner NotePoolParent;

        private NoteLineSO KeyDetail;
        
        public void InitKeyRow(NoteLineSO _KeyDetail)
        {
            isInit = true;
            KeyDetail = _KeyDetail;
            KeyIndicator.SetUpIndicator(KeyDetail.keyCode,KeyDetail.color);
        }

        public void AddNote(float time)
        {
            NotePoolParent.AddNote(time);
        }

        private void KeyPressed()
        {
            KeyIndicator.KeyPressed();
            NotePoolParent.KeyPressEvent.Invoke();
        }
        private void KeyReleased()
        {
            KeyIndicator.KeyReleased();
        }

        private void Update()
        {
            if (!isInit) return;

            if (Input.GetKeyDown(KeyDetail.keyCode))
            {
                KeyPressed();
            }

            if (Input.GetKeyUp(KeyDetail.keyCode))
            {
                KeyReleased();
            }
        }
    }
}

