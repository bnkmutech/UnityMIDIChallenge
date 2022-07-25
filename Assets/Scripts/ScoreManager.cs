using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI score;
    public int totalScore = 0;

    void Start()
    {
        totalScore = 0;
    }

    void Update()
    {
        score.text = totalScore.ToString();
    }
}
