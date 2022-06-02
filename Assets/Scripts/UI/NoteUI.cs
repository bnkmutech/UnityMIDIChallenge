using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmGame.UI
{
    public class NoteUI : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _noteSpt;

        public void SetNote(Color colour)
        {
            _noteSpt.color = colour;
        }
    }
}
