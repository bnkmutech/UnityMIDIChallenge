using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int score;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        scoreText = GetComponentsInChildren<Text>()[1];
    }


    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void Hit(int score)
    {
        this.score += score;
    }

    public void Reset()
    {
        score = 0;
    }
}
