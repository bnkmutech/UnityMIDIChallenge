using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Image RimSnare_Image;
    [SerializeField] public KeyCode RimSnare_Button;
    [SerializeField] protected TextMeshProUGUI RimSnare_Text;

    [SerializeField] Image Cymbal_Image;
    [SerializeField] public KeyCode Cymbal_Button;
    [SerializeField] protected TextMeshProUGUI Cymbal_Text;

    [SerializeField] Image Snare_Image;
    [SerializeField] public KeyCode Snare_Button;
    [SerializeField] protected TextMeshProUGUI Snare_Text;

    [SerializeField] Image BassDrum_Image;
    [SerializeField] public KeyCode BassDrum_Button;
    [SerializeField] protected TextMeshProUGUI BassDrum_Text;

    [SerializeField] Image HighTom_Image;
    [SerializeField] public KeyCode HighTom_Button;
    [SerializeField] protected TextMeshProUGUI HighTom_Text;

    [SerializeField] Image FloorTom_Image;
    [SerializeField] public KeyCode FloorTom_Button;
    [SerializeField] protected TextMeshProUGUI FloorTom_Text;

    void Start()
    {
        RimSnare_Text.text = RimSnare_Button.ToString();
        Cymbal_Text.text = Cymbal_Button.ToString();
        Snare_Text.text = Snare_Button.ToString();
        BassDrum_Text.text = BassDrum_Button.ToString();
        HighTom_Text.text = HighTom_Button.ToString();
        FloorTom_Text.text = FloorTom_Button.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(RimSnare_Button))
        {
            RimSnare_Image.color = new Color32(150,50,255,255);
            RimSnare_Text.color = new Color(0,0,0,255);
            StartCoroutine(RimSnare_AnimationDelay());
        } 

        if(Input.GetKeyDown(Cymbal_Button))
        {
            Cymbal_Image.color = new Color32(50,150,255,255);
            Cymbal_Text.color = new Color(0,0,0,255);
            StartCoroutine(Cymbal_AnimationDelay());
        } 

        if(Input.GetKeyDown(Snare_Button))
        {
            Snare_Image.color = new Color32(50,150,0,255);
            Snare_Text.color = new Color(0,0,0,255);
            StartCoroutine(Snare_AnimationDelay());
        } 

        if(Input.GetKeyDown(BassDrum_Button))
        {
            BassDrum_Image.color = new Color32(180,180,0,255);
            BassDrum_Text.color = new Color(0,0,0,255);
            StartCoroutine(BassDrum_AnimationDelay());
        } 

        if(Input.GetKeyDown(HighTom_Button))
        {
            HighTom_Image.color = new Color32(205,85,0,255);
            HighTom_Text.color = new Color(0,0,0,255);
            StartCoroutine(HighTom_AnimationDelay());
        } 

        if(Input.GetKeyDown(FloorTom_Button))
        {
            FloorTom_Image.color = new Color32(205,0,0,255);
            FloorTom_Text.color = new Color(0,0,0,255);
            StartCoroutine(FloorTom_AnimationDelay());
        } 
    }

    public IEnumerator RimSnare_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        RimSnare_Image.color = new Color32(200,100,255,255);
        RimSnare_Text.color = new Color(255,255,255,255);
    }

    public IEnumerator Cymbal_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        Cymbal_Image.color = new Color32(100,200,255,255);
        Cymbal_Text.color = new Color(255,255,255,255);
    }

    public IEnumerator Snare_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        Snare_Image.color = new Color32(100,200,0,255);
        Snare_Text.color = new Color(255,255,255,255);
    }

    public IEnumerator BassDrum_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        BassDrum_Image.color = new Color32(230,230,0,255);
        BassDrum_Text.color = new Color(255,255,255,255);
    }

    public IEnumerator HighTom_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        HighTom_Image.color = new Color32(255,135,0,255);
        HighTom_Text.color = new Color(255,255,255,255);
    }

    public IEnumerator FloorTom_AnimationDelay()
    {
        yield return new WaitForSeconds(0.15f);
        FloorTom_Image.color = new Color32(255,0,0,255);
        FloorTom_Text.color = new Color(255,255,255,255);
    }
}
