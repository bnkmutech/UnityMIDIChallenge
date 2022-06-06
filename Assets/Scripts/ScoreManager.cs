using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private int _score = 0;

    [SerializeField] private CheckCollision _checkCollision;
    [SerializeField] private SongManager _songManager;

    private void OnEnable()
    {
        _checkCollision.OnPressNote += AddScore;
        _songManager.OnPressRestart += ResetScore;
    }
    private void OnDisable()
    {
        _checkCollision.OnPressNote -= AddScore;
        _songManager.OnPressRestart -= ResetScore;
    }

    public void AddScore()
    {
        _score++;
        _scoreText.text = "Score: " + _score;
    }
    public void ResetScore()
    {
        _score = 0;
        _scoreText.text = "Score: " + _score;
    }
}
