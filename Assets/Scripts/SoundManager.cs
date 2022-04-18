using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  //  public static SoundManager instance;

    [SerializeField]
    private AudioSource _Music;
    [SerializeField]
    private AudioSource _Metronome;
    [SerializeField]
    private AudioSource _hitSFX;
    [SerializeField]
    private AudioSource _missSFX;
    [SerializeField]
    private AudioSource _pressSFX;



    public static AudioSource Music { get; private set; }
    public static AudioSource Metronome { get; private set; }
    public static AudioSource hitSFX { get; private set; }
    public static AudioSource missSFX { get; private set; }
    public static AudioSource pressSFX { get; private set; }


    // Start is called before the first frame update
    void Start()
    {

        Music = _Music;
        Metronome = _Metronome;
        hitSFX = _hitSFX;
        missSFX = _missSFX;
        pressSFX = _pressSFX;






//        instance = this;
    }

    public static void Hit()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);


        Camera.main.backgroundColor = new Color(r, g, b);

        hitSFX.Play();
    }

    public static void Miss()
    {
        missSFX.Play();
    }

    public static void Press()
    {
        pressSFX.Play();
    }
}
