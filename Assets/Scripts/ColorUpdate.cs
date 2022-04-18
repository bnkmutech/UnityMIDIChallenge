using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ColorUpdate : MonoBehaviour
{
    [SerializeField]
    private int KeyNumber;

    private Image image;


    private void OnEnable()
    {
        image = GetComponent<Image>();
        ColorChange();
    }

    public void ColorChange()
    {
        image.color = Settings.colorDict[KeyNumber];
    }

}
