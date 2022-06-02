using Assets.Core;
using RhythmGame.Model;
using System.Collections;
using UnityEngine;

namespace RhythmGame.Controller
{
    public class KeyController : BaseController<UI.KeyUI>
    {
        public AudioClip KeySound;
        public bool _isKeyPress;
        Coroutine _delayKey;
        KeyInfo _keyId;
        public KeyInfo KeyId
        {
            get => _keyId;
            set
            {
                _keyId = value;
                UIInstance.SetKey(_keyId);
            }
        }
        public void SetKey(KeyInfo key)
        {
            KeyId = key;
        }
        public void ActiveKey()
        {
            KeyId += new KeyInfo
            {
                TextColour = Color.black,
                Colour = new Color(KeyId.Colour.r, KeyId.Colour.g, KeyId.Colour.b, 0.7f)
            };
            _isKeyPress = true;
            if (_delayKey != null)
                StopCoroutine(_delayKey);
        }
        public void KeyUp()
        {
            KeyId += new KeyInfo
            {
                TextColour = Color.white,
                Colour = new Color(KeyId.Colour.r, KeyId.Colour.g, KeyId.Colour.b, 1f)
            };
            _delayKey = StartCoroutine(WaitKeyUp());
        }

        IEnumerator WaitKeyUp()
        {
            yield return new WaitForSeconds(.5f);
            _isKeyPress = false;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_isKeyPress)
                collision.GetComponent<IScore>()?.AdjustScore();
        }
    }
}
