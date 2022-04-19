using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //   public static Settings Instance;

    public static int scorePoint { get; set; }

    public static int NoteSpeed { get;  set; }

    public static bool Metronome { get;  set; }


    public static Dictionary<int, KeyCode> keyCodeDict;

    public static Dictionary<int, Color> colorDict;



   public  void DefaultKeyCode()
    {
        Debug.Log("Set Default Key");

        keyCodeDict = new Dictionary<int, KeyCode>();
        keyCodeDict.Add(1, KeyCode.A);
        keyCodeDict.Add(2, KeyCode.S);
        keyCodeDict.Add(3, KeyCode.D);
        keyCodeDict.Add(4, KeyCode.F);
        keyCodeDict.Add(5, KeyCode.G);
        keyCodeDict.Add(6, KeyCode.H);
    }

   public void DefaultColor()
    {
        Debug.Log("Set Default Color");
        colorDict = new Dictionary<int, Color>();
        colorDict.Add(1, new Color(0.5f,0,0.6f));
        colorDict.Add(2, new Color(0f, 0.6f, 1f));
        colorDict.Add(3, new Color(0f, 0.6f, 0));
        colorDict.Add(4, new Color(1f, 0.9f, 0f));
        colorDict.Add(5, new Color(1f, 0.6f, 0f));
        colorDict.Add(6, new Color(0.8f, 0, 0.6f));
    }

   public void DefaultValue()
    {
        Debug.Log("Set Default Value");
        scorePoint = 20;
        NoteSpeed = 1;
        Metronome = true;
    }

 





}
