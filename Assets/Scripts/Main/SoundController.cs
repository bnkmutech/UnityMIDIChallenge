using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundController : MonoBehaviour
{
    private AudioSource audioData;
    private float delayTimeToPlay;
    private bool isAlreadyPlayedAudio;

    /* check if a level finished */
    public bool IsAudioPlaying()
    {
        return audioData.isPlaying;
    }

    public void PauseAudio()
    {
        audioData.Pause();
    }

    public void UnPauseAudio()
    {
        audioData.UnPause();
    }

    public float GetDelayTime(float timeSecToMoveNotes)
    {
        /*((note height - button height) * secToMoveNote) / (px move each time * prefer windows height) */
        return ((300f + 131f) * timeSecToMoveNotes) / (0.01f * 720);
    }

    // Start is called before the first frame update
    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        delayTimeToPlay = Time.time + GetDelayTime(gameObject.GetComponent<NoteController>().GetTimeToMoveNotes());
        isAlreadyPlayedAudio = false;
    }

    private void Update()
    {
        if(Time.time >= delayTimeToPlay && !isAlreadyPlayedAudio)
        {
            audioData.Play(0);
            isAlreadyPlayedAudio = true;
        }
    }
}
