using System;
using System.Collections.Generic;
using SOTemplate.NoteSO;
using UnityEngine;

namespace RaindropGame
{
    public class RaindropKeyRow : MonoBehaviour
    {
        private bool isInit = false;
        
        [Header("Setting")]
        [SerializeField] private RaindropKeyIndicator KeyIndicator;

        [SerializeField] private GameObject NotePoolParent;
        
        private NoteLineSO KeyDetail;
        
        public void InitKeyRow(NoteLineSO _KeyDetail)
        {
            isInit = true;
            KeyDetail = _KeyDetail;
            KeyIndicator.SetColor(KeyDetail.color);
        }

        private void KeyPressed()
        {
            KeyIndicator.KeyPressed();
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

