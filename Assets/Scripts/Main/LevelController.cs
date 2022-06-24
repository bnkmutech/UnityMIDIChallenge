using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    private void RestartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);         
    }

    // Update is called once per frame
    private void Update()
    {
        /* check if press space and finished the song and didn't pausing */
        if (Input.GetKeyDown("space") && !GameObject.FindGameObjectWithTag("Canvas").GetComponent<SoundController>().IsAudioPlaying() && !MenuEvent.isPausing)
        {
            RestartLevel();
        }
    }
}
