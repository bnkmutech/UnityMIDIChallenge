using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Settings
{


    [SerializeField]
    TMPro.TextMeshProUGUI comboText;
     [SerializeField]
    TMPro.TextMeshProUGUI scoreText;
   public static int comboScore;
   public static int score;




    // Start is called before the first frame update
    void Start()
    {
    //    Instance = this;
        comboScore = 0;
    }

    public static void Hit()
    {

        BgSwitch();
       


        score += scorePoint;
        comboScore += 1;
      

    }

    public static void Miss()
    {
        comboScore = 0;
    //    Instance.missSFX.Play();
    }


    private static void BgSwitch()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);
        Camera.main.backgroundColor = new Color(r, g, b);
    }



    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";
        comboText.text =  $"Combo: {comboScore}";
    }
}
