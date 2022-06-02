using Assets.Core;
using RhythmGame.Model;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    #region Instance
    public static GameManage Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        KeyData = new KeyInfo[]
        {
            new KeyInfo
            {
                Key = KeyCode.A,
                Colour = GetColor("#B05AE0")
            },
            new KeyInfo
            {
                Key = KeyCode.S,
                Colour = GetColor("#5AC6E0")
            },
            new KeyInfo
            {
                Key = KeyCode.D,
                Colour = GetColor("#7DE05A")
            },
            new KeyInfo
            {
                Key = KeyCode.F,
                Colour = GetColor("#E0DD5A")
            },
            new KeyInfo
            {
                Key = KeyCode.G,
                Colour = GetColor("#E09E5A")
            },
            new KeyInfo
            {
                Key = KeyCode.H,
                Colour = GetColor("#E05A5A")
            },
        };
    }
    #endregion

    public int Score = 0;
    public int StartPointSpawn = -25;
    public KeyInfo[] KeyData;
    public GameUI GameUIManage;

    public void AdjustScore(int score)
    {
        Score += score;
        GameUIManage.AdjustScore(Score.ToString());
    }

    Color GetColor(string hex)
    {
        return ColorUtility.TryParseHtmlString(hex, out Color colour) ? colour : Color.gray;
    }
}
