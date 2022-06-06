using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextUI;
    
    #region Instance

    public static ScoreManager Instance;

    public static int Score;
    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void Reset()
    {
        Score = 0;
        UpdateScoreUI();
    }

    public void AddScore(int value)
    {
        Score += value;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreTextUI.text = "Score\n" + Score.ToString("D3");
    }
}
