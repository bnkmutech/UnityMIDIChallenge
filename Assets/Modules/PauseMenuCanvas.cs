using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuCanvas : MonoBehaviour
{
    public GameObject countDownGameObject => _countDownText.gameObject;

    public Action resumeOnClick
    {
        set
        {
            _resumeButton.onClick.AddListener(value.Invoke);
        }
    }
    public Action selectTrackOnClick
    {
        set { _seleckTrackButton.onClick.AddListener(value.Invoke); }
    }
    public Action quitGameOnClick
    {
        set { _quitGameButton.onClick.AddListener(value.Invoke); }
    }
    
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _seleckTrackButton;
    [SerializeField] private Button _quitGameButton;
    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private TextMeshProUGUI _countDownText;

    public void SetTime(int countTime)
    {
        if (_buttonPanel.activeInHierarchy)
            _buttonPanel.SetActive(false);
        
        if (!_countDownText.gameObject.activeInHierarchy)
            _countDownText.gameObject.SetActive(true);
        
        _countDownText.text = countTime.ToString();
    }

    public void SetActive(bool enabled)
    {
        _buttonPanel.SetActive(enabled);
        gameObject.SetActive(enabled);
    }
}