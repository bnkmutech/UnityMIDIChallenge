using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrackButton : MonoBehaviour
{
    public Action onclick
    {
        set { _button.onClick.AddListener(value.Invoke); }
    }

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _trackText;
    [SerializeField] private TextMeshProUGUI _difficultyText;
    [SerializeField] private TextMeshProUGUI _durationText;

    public void SetText(TrackData data)
    {
        _trackText.text = data.clip.name;
        var trackSpan = new TimeSpan(0, 0, Mathf.CeilToInt(data.clip.length));
        _difficultyText.text = data.difficulty;
        _durationText.text = $"{trackSpan.Minutes}:{trackSpan.Seconds}";
    }
}