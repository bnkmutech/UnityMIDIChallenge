using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private int scorePerNote;

    public void press_A()
    {
        try
        {
            if(NoteController.notesOnScreen[0][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }
    public void press_S()
    {
        try
        {
            if(NoteController.notesOnScreen[1][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }
    public void press_D()
    {
        try
        {
            if(NoteController.notesOnScreen[2][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }
    public void press_F()
    {
        try
        {
            if(NoteController.notesOnScreen[3][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }
    public void press_G()
    {
        try
        {
            if(NoteController.notesOnScreen[4][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }
    public void press_H()
    {
        try
        {
            if(NoteController.notesOnScreen[5][0].GetComponent<NoteLifeCycle>().IsScore())
            {
                gameObject.transform.parent.GetComponent<NoteController>().UpdateScore(scorePerNote);
            }
        }
        catch
        {
            // do nothing because not have any note on same button line
        }
    }

    private void Start()
    {
        scorePerNote = GameObject.FindGameObjectWithTag("Canvas").GetComponent<NoteController>().GetScorePerNote();
    }
}
