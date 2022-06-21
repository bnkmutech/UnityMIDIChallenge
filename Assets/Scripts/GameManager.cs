using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public KeyCode restartKey;
    [HideInInspector] public bool gameHasEnded;
    [SerializeField] [HideInInspector] GameObject restartText;
    Text text;
    void Start()
    {
        text = restartText.GetComponent<Text>();

        if (Instance == null)
        {

            Instance = this;

        }
        else if (Instance != this)
        {

            Destroy(gameObject);

        }

    }
    void Update()
    {
        if (Input.GetKeyDown(restartKey) && gameHasEnded)
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        text.enabled = false;
        gameHasEnded = false;
        SongManager.Instance.Reset();
        SongManager.Instance.unrestartableDelay = 0;
        ScoreManager.Instance.Reset();
        CancelInvoke(); 
    }

    public void GameHasEnded()
    {
        text.enabled = true;
        gameHasEnded = true;
    }
}
