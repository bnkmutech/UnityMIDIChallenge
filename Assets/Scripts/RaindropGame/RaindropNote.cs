using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RaindropGame
{
    public class RaindropNote : MonoBehaviour
    {
        
        private RaindropKeyRow ParentRow;
        
        public RectTransform rectTransform;
        
        private bool isInit = false;
        private float hitMinY;
        private float hitMaxY;
        private float missY;

        private int travelDistant;


        public void InitNote(RaindropKeyRow parent, Color color)
        {
            ParentRow = parent;
            GetComponent<Image>().color = color;

            hitMinY = RaindropGameManager.NotePerfectY + RaindropGameManager.NoteHitMargin;

            hitMaxY = RaindropGameManager.NotePerfectY - RaindropGameManager.NoteHitMargin;

            missY = RaindropGameManager.NoteMissY;

            rectTransform = GetComponent<RectTransform>();

            Vector3 pos = rectTransform.anchoredPosition3D;
            pos.y = RaindropGameManager.NoteSpawnY;
            rectTransform.anchoredPosition3D = pos;
            
            travelDistant = RaindropGameManager.NoteSpawnY - RaindropGameManager.NotePerfectY;

            isInit = true;
        }

        private void FixedUpdate()
        {
            if (!isInit) return;

            Vector3 pos = rectTransform.anchoredPosition3D;
            pos.y -= travelDistant * RaindropGameManager.NoteSpeed * Time.deltaTime;
            rectTransform.anchoredPosition3D = pos;

            if (pos.y <= missY)
            {
                Destroy(gameObject);
            }
        }

        public void CheckKeyHit()
        {
            rectTransform = GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                float posY = rectTransform.anchoredPosition3D.y;
                if (posY <= hitMinY && posY >= hitMaxY)
                {
                    ParentRow.OnNoteHit();
                    Destroy(gameObject);
                }
            }
        }
    }
}