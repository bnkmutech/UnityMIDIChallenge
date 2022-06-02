using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text ScoreTxt;
    public void AdjustScore(string score)
    {
        ScoreTxt.text = $"Score: {score}";
    }
}
