using Assets.Core;
using RhythmGame.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmGame.UI
{
    public class KeyUI : BaseUI
    {
        [SerializeField] SpriteRenderer _keySpt;
        [SerializeField] TextMeshPro _keyTmp;

        public void SetKey(KeyInfo key)
        {
            _keySpt.color = key.Colour;
            _keyTmp.text = key.Key.ToString();
            _keyTmp.color = key.TextColour;
        }
    }
}
