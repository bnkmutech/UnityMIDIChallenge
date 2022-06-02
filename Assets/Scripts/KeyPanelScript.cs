using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanelScript : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public KeyCode key_rimSnare, key_cymbal, key_snare, key_bassDrum, key_highTom, key_floorTom;
    public Text text_rimSnare, text_cymbal, text_snare, text_bassDrum, text_highTom, text_floorTom;

    // Start is called before the first frame update
    void Start()
    {
        keys["rim_snare"] = key_rimSnare;
        keys["cymbal"] = key_cymbal;
        keys["snare"] = key_snare;
        keys["bass_drum"] = key_bassDrum;
        keys["high_tom"] = key_highTom;
        keys["floor_tom"] = key_floorTom;

        text_rimSnare.text = keys["rim_snare"].ToString();
        text_cymbal.text = keys["cymbal"].ToString();
        text_snare.text = keys["snare"].ToString();
        text_bassDrum.text = keys["bass_drum"].ToString();
        text_highTom.text = keys["high_tom"].ToString();
        text_floorTom.text = keys["floor_tom"].ToString();
    }
}
