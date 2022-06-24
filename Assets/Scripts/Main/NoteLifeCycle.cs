using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteLifeCycle : MonoBehaviour
{
    private float nextMove;
    private int lineNumber;
    private bool isNoteCollider = false;

    /* destroy the note when note passed indicator and did't collide */
    private void IsLateMissed()
    {
        if(
            gameObject.GetComponent<RectTransform>().anchoredPosition3D.y + (gameObject.GetComponent<RectTransform>().sizeDelta.y / 2) < 
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<KeyboardReceiver>().GetIndicator().GetComponent<RectTransform>().anchoredPosition3D.y
        )
        {
            NoteController.notesOnScreen[lineNumber].Remove(gameObject);
            Destroy(gameObject);
        }
    }

    /* chcek if the note is colliding with indicator and destroy it */
    public bool IsScore()
    {
        NoteController.notesOnScreen[lineNumber].Remove(gameObject);
        Destroy(gameObject);
        return isNoteCollider;
    }

    /* initialize the note's size and position */ 
    public void Init(int lineNumber)
    {
        float noteSize = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>().GetNoteSize();
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(75f, noteSize);
        this.GetComponent<BoxCollider>().size = new Vector3(75f, noteSize, 10f);
        this.lineNumber = lineNumber;
    }

    private void Start()
    {
        nextMove = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        isNoteCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isNoteCollider = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.time >= nextMove)
        {
            IsLateMissed();
            gameObject.transform.position = new Vector3 (
                gameObject.transform.position.x,
                gameObject.transform.position.y - (0.01f * Screen.height),
                gameObject.transform.position.z
            );
            nextMove = Time.time + GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>().GetTimeToMoveNotes();
        }
    }
}
