using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteController : MonoBehaviour
{
    private int currentScore = 0;
    public static List<List<GameObject>> notesOnScreen;
    private List<Vector3> keysPosition;
    private float timeToMoveNotes = 0.02f;

    [SerializeField] private float noteSpeed = 5;
    [SerializeField] private float noteSize = 10f;
    [SerializeField] private int scorePerNote = 20;
    [SerializeField] private List<Color> noteColors;

    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject notePrefab;

    public void UpdateScore(int increasingScore)
    {
        currentScore += increasingScore;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + currentScore.ToString();
    }

    /* do when start or restart the game */
    public void Init()
    {
        List<Button> allButtons = gameObject.GetComponent<KeyboardReceiver>().GetButtons();
        timeToMoveNotes = 0.1f / noteSpeed;
        notesOnScreen = new List<List<GameObject>>();
        keysPosition = new List<Vector3>();
        for(int i = 0; i < allButtons.Count; i++)
        {
            notesOnScreen.Add(new List<GameObject>());
            keysPosition.Add(allButtons[i].GetComponent<RectTransform>().anchoredPosition3D);
            allButtons[i].GetComponent<Image>().color = noteColors[i];
        }
    }

    /* change color of note by option menu */
    public void ChangeNoteColor()
    {
        List<Button> allButtons = gameObject.GetComponent<KeyboardReceiver>().GetButtons();
        noteColors = new List<Color>();
        foreach(Button button in allButtons)
        {
            noteColors.Add(button.targetGraphic.color);
        }
        for(int i = 0; i < notesOnScreen.Count; i++)
        {
            foreach(GameObject note in notesOnScreen[i])
            {
                note.GetComponent<Image>().color = noteColors[i];
            }
        }
    }

    public List<Color> GetNoteColors()
    {
        return noteColors;
    }
    
    /* generate new note on map */
    public void GenerateNote(int lineNumber)
    {
        GameObject generatedNote = Instantiate(notePrefab, new Vector3(keysPosition[lineNumber].x, 300f, keysPosition[lineNumber].z), Quaternion.identity);
        generatedNote.GetComponent<NoteLifeCycle>().Init(lineNumber);
        generatedNote.GetComponent<Image>().color = noteColors[lineNumber];
        generatedNote.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        notesOnScreen[lineNumber].Add(generatedNote);
    }

    public float GetTimeToMoveNotes()
    {
        return timeToMoveNotes;
    }

    public float GetNoteSize()
    {
        return noteSize;
    }

    public void SetTimeToMoveNotes(float value)
    {
        noteSpeed = 0.1f / value;
        timeToMoveNotes = value;
    }

    public void SetNoteSize(float value)
    {
        noteSize = value;
    }

    public int GetScorePerNote()
    {
        return scorePerNote;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }
}
