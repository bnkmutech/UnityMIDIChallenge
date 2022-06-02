using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPanelScript : MonoBehaviour
{
    [SerializeField] private KeyCode key_rimSnare, key_cymbal, key_snare, key_bassDrum, key_highTom, key_floorTom;
    [SerializeField] private Color clr_rimSnare, clr_cymbal, clr_snare, clr_bassDrum, clr_highTom, clr_floorTom;
    [SerializeField] private Button btn_rimSnare, btn_cymbal, btn_snare, btn_bassDrum, btn_highTom, btn_floorTom;

    private Text text_rimSnare, text_cymbal, text_snare, text_bassDrum, text_highTom, text_floorTom;

    // Start is called before the first frame update
    void Start()
    {
        //get text from button
        text_rimSnare = btn_rimSnare.GetComponentInChildren<Text>();
        text_cymbal = btn_cymbal.GetComponentInChildren<Text>();
        text_snare = btn_snare.GetComponentInChildren<Text>();
        text_bassDrum = btn_bassDrum.GetComponentInChildren<Text>();
        text_highTom = btn_highTom.GetComponentInChildren<Text>();
        text_floorTom = btn_floorTom.GetComponentInChildren<Text>();

        //set text string to match with keycode from inspector
        text_rimSnare.text = key_rimSnare.ToString();
        text_cymbal.text = key_cymbal.ToString();
        text_snare.text = key_snare.ToString();
        text_bassDrum.text = key_bassDrum.ToString();
        text_highTom.text = key_highTom.ToString();
        text_floorTom.text = key_floorTom.ToString();

        //set button color to match with color from inspector
        btn_rimSnare.GetComponent<Image>().color = clr_rimSnare;
        btn_cymbal.GetComponent<Image>().color = clr_cymbal;
        btn_snare.GetComponent<Image>().color = clr_snare;
        btn_bassDrum.GetComponent<Image>().color = clr_bassDrum;
        btn_highTom.GetComponent<Image>().color = clr_highTom;
        btn_floorTom.GetComponent<Image>().color = clr_floorTom;
    }
}
