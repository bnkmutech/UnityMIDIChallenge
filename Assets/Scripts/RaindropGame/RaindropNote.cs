using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RaindropGame
{
    public class RaindropNote : MonoBehaviour
    {
        private float noteVelocity = 0;

        private RectTransform rectTransform;
        private float hitMin;
        private float hitMax;

        private void Start()
        {
            hitMin = RaindropGameManager.NotePerfectY + RaindropGameManager.NoteHitMargin;

            hitMax = RaindropGameManager.NotePerfectY - RaindropGameManager.NoteHitMargin;

            rectTransform = GetComponent<RectTransform>();

            Vector3 pos = rectTransform.anchoredPosition3D;
            pos.y = RaindropGameManager.NoteSpawnY;
            rectTransform.anchoredPosition3D = pos;

            int travelDistant = RaindropGameManager.NoteSpawnY - RaindropGameManager.NotePerfectY;
            noteVelocity = travelDistant / RaindropGameManager.NoteSpeed;
        }

        private void FixedUpdate()
        {
            Vector3 pos = rectTransform.anchoredPosition3D;
            pos.y -= noteVelocity;
            rectTransform.anchoredPosition3D = pos;

            if (pos.y <= hitMax)
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
                if (posY <= hitMin)
                {
                    print("HIT");
                    Destroy(gameObject);
                }
            }
        }
    }
}