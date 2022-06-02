using RhythmGame.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RhythmGame
{
    public class KeyManage : MonoBehaviour
    {
        Dictionary<KeyCode, KeyController> _keyAction = new Dictionary<KeyCode, KeyController>();
        [SerializeField] Transform _spawnKeyPoint;
        [SerializeField] KeyController _keyPrefab;

        void Start()
        {
            SpawnKey();
        }

        void SpawnKey()
        {
            for (int i = 0; i < GameManage.Instance.KeyData.Length; i++)
            {
                var piece = Instantiate(_keyPrefab, _spawnKeyPoint);
                piece.transform.localPosition = new Vector2(GameManage.Instance.StartPointSpawn + (i * 10), 0);
                piece.SetKey(GameManage.Instance.KeyData[i]);
                _keyAction.Add(GameManage.Instance.KeyData[i].Key, piece);
            }
        }

        void SetKeys()
        {

        }

        void OnGUI()
        {
            Event e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                    return;
                }

                if (Input.GetKeyDown(e.keyCode) && _keyAction.ContainsKey(e.keyCode))
                {
                    _keyAction[e.keyCode].ActiveKey();
                }
            }
            else if (e.type == EventType.KeyUp)
            {
                if (Input.GetKeyUp(e.keyCode) && _keyAction.ContainsKey(e.keyCode))
                {
                    _keyAction[e.keyCode].KeyUp();
                }
            }

        }
    }
}
