using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private GameObject noteSizeSlider;

    // Start is called before the first frame update
    private void Start()
    {
        Slider sizeController = noteSizeSlider.GetComponent<Slider>();
        NoteController noteController = mainCanvas.GetComponent<NoteController>();

        if(PlayerPrefs.HasKey("noteSize"))
        {
            sizeController.value = PlayerPrefs.GetFloat("noteSize");
        }
        else
        {
            sizeController.value = noteController.GetNoteSize();
        }
    }

}
