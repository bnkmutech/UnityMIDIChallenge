using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = default;

    private bool isPlaying = false;

    // keycode
    private Dictionary<string, Note> currentNotes = default;

    public void Active(Rigidbody gameCameraRb, float velocity)
    {
        if (!isPlaying)
        {
            isPlaying = true;
            audioSource.Play();
            gameCameraRb.velocity = new Vector3(0, velocity, 0);
        }
    }

    private void Awake()
    {
        currentNotes = new Dictionary<string, Note>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (Input.anyKeyDown)
            {
                var keypressed = Input.inputString.ToUpper();

                if (currentNotes.TryGetValue(keypressed, out Note note))
                { 
                    note.gameObject.SetActive(false);
                    currentNotes.Remove(keypressed);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Note>() is Note note)
        {
            currentNotes.Add(note.KeyCodeStr, note);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Note>() is Note note)
        {
            currentNotes.Remove(note.KeyCodeStr);
        }
    }
}