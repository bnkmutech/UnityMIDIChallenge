using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject pauseMenuBG;
    public static bool isPausing = false;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        pauseMenuBG.SetActive(false);
        mainCanvas.GetComponent<SoundController>().UnPauseAudio();
        Time.timeScale = 1f;
        isPausing = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseMenuBG.SetActive(true);
        mainCanvas.GetComponent<SoundController>().PauseAudio();
        Time.timeScale = 0f;
        isPausing = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPausing)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
}
