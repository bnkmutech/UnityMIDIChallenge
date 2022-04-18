using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Settings
{
 //   public static GameManager Instance;

    [SerializeField]
    private GameObject GameplayObject;
    [SerializeField]
    private GameObject GameMenu;
    [SerializeField]
    private GameObject SettingPanel;
    public static bool isPlaying;

    private void Awake()
    {
     //   Instance = this;
        DefaultValue();
        DefaultColor();
        DefaultKeyCode();
    }


    // Update is called once per frame
    void Update()
    {
        MenuPanelDisplay();

        if (SettingPanel.activeInHierarchy)
            return;


         
        if (Input.GetKeyDown(KeyCode.Space))   
        {       
           StartGame();
        }

    }


    public void Setting_Active(bool a)
    {
       SettingPanel.SetActive(a);
    }








  public  void StartGame()
    {
        if (!isPlaying)
        {
            GameObject[] a = GameObject.FindGameObjectsWithTag("GamePlay");
            if (a.Length >= 1)
            {
                foreach (var b in a)
                {
                    Destroy(b);
                }
            }

            Instantiate(GameplayObject);
        }
    }

    void MenuPanelDisplay()
    {
        if (!isPlaying)
        {
            MenuPanelActive(true);
            Cursor.visible = true;

        }
        else
        {
            MenuPanelActive(false);
            Cursor.visible = false;
        }
    }

    public void MenuPanelActive(bool a)
    {
        GameMenu.SetActive(a);
    }



}
