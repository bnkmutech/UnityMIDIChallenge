using System;
using System.Collections.Generic;
using SOTemplate.NoteSO;
using UnityEngine;
using UnityEngine.Events;

namespace RaindropGame
{
    public class RaindropKeyRow : MonoBehaviour
    {
        [Header("Setting")] [SerializeField] public RaindropKeyIndicator keyIndicator;

        [SerializeField] public RaindropKeySpawner notePoolParent;

        private bool isInit = false;

        private NoteLineSO _keyDetail;

        public UnityEvent onKeyPressedEvent;

        private void Start()
        {
            onKeyPressedEvent = new UnityEvent();
        }

        public void InitKeyRow(NoteLineSO keyDetail)
        {
            isInit = true;
            
            _keyDetail = keyDetail;

            keyIndicator?.InitKeyIndicator(_keyDetail.keyCode, _keyDetail.color);

            notePoolParent?.InitKeySpawner(this, _keyDetail.color);
        }

        public void AddNote(float time)
        {
            notePoolParent.AddNote(time);
        }

        private void KeyPressed()
        {
            keyIndicator.OnKeyPressed();
            onKeyPressedEvent.Invoke();
        }

        private void KeyReleased()
        {
            keyIndicator.OnKeyReleased();
        }

        private void Update()
        {
            if (!isInit) return;

            if (Input.GetKeyDown(_keyDetail.keyCode))
            {
                KeyPressed();
            }

            if (Input.GetKeyUp(_keyDetail.keyCode))
            {
                KeyReleased();
            }
        }

        public void OnNoteHit()
        {
            ScoreManager.Instance.AddScore(_keyDetail.scorePoint);
            keyIndicator.PlayHitEffect();
        }
    }
}