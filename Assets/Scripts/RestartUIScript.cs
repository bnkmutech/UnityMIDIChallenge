using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartUIScript : MonoBehaviour
{
    [SerializeField] private SongManager _songManager;
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();

        //hide text when start
        _text.enabled = false;
    }

    //check event
    private void OnEnable()
    {
        _songManager.OnGameFinish += ShowText;
        _songManager.OnPressRestart += HideText;
    }
    private void OnDisable()
    {
        _songManager.OnGameFinish -= ShowText;
        _songManager.OnPressRestart += HideText;
    }

    //function
    void ShowText()
    {
        _text.enabled = true;
    }
    void HideText()
    {
        _text.enabled = false;
    }
}
