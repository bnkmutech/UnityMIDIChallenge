using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class Note : Settings
{
    double timeInstantiated;
    public float assignedTime;

    // Start is called before the first frame update

    private Image image;



    void Start()
    {
      image = GetComponent<Image>();
        // Image Blinking Fix
        image.enabled = false;
      timeInstantiated =   GamePlay.GetAudioSourceTime();
    }

    // Update is called once per frame
    void Update()
    {

        double timeSinceInstantiated = GamePlay.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (GamePlay.gamePlay.actualSpeed * 2));

        if( t>1)
        {
            Destroy(gameObject);

        }
        else
        {
            NoteFalling(t);
        }


        if (assignedTime != 2)
            return;


    }

    void NoteFalling(float t)
    {
        image.enabled = true;
        transform.localPosition = Vector3.Lerp(Vector3.up * GamePlay.gamePlay.SpawnDistance, Vector3.up * GamePlay.gamePlay.NoteMissed, t);

    }



}
