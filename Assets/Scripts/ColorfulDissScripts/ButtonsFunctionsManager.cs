using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonsFunctionsManager : MonoBehaviour
{
    public void GoToHome() 
    {
        SceneManager.LoadScene("SongSelectionScene");
    }
    public void Replay() 
    {
        SceneManager.LoadScene("GameScene");
    }

}
