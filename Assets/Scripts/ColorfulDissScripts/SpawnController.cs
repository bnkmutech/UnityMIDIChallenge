using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController Instance { get { return _instance; } }
    private static SpawnController _instance;

    public List<GameObject> RedNoteStorage, OrangeNoteStorage, YellowNoteStorage, GreenNoteStorage, BlueNoteStorage, VioletNoteStorage;
    public bool IsGameBegin = false;

    [SerializeField] private float counter ;
    [SerializeField] [Range(1,5)]private float m_Multiplier;
    [SerializeField] private GameObject m_SpawnPoint;

    public int SpawnCount = 0;
    private void Awake()
    {
        m_SpawnPoint = this.gameObject;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        if (_instance == null)
            _instance = this;
    }

    void Update()
    {
        Debug.Log("Spawn: "+SpawnCount + "TotalNoteToPlay: "+ GameController.Instance.TotalNoteToPlay());
        if (GameController.Instance.CurrentSongState == SongState.End || !IsGameBegin) return;

        if (counter >= 0f)
            counter -= Time.deltaTime*m_Multiplier;
        else
        {
            counter = 0f;
            if (this.transform.childCount < 6 && SpawnCount< GameController.Instance.TotalNoteToPlay() )
                SpawnNotes();
            counter = 0.5f;
        }
    }

    private void SpawnNotes()
    {
        int noteIndex = (!GameController.isCheatMode) ? Random.Range(0, GameController.Instance.Note.Length - 1) : 0; //1);//
        GameObject newNote = Instantiate(GameController.Instance.Note[noteIndex], this.transform.position, Quaternion.identity) as GameObject;
        newNote.transform.SetParent(m_SpawnPoint.transform, false);
        newNote.name = GameController.Instance.Note[noteIndex].name;
        StoreTheNoteInLists(newNote);
        SpawnCount++;
    }

    private void StoreTheNoteInLists(GameObject noteIndex) 
    {
        GameController.Instance.AllNotes.Add(noteIndex);
        switch (noteIndex.GetComponent<NoteMovement>().NoteColor)
        {
            case ColorList.Red:
                RedNoteStorage.Add(noteIndex);
                break;
            case ColorList.Orange:
                OrangeNoteStorage.Add(noteIndex);
                break;
            case ColorList.Yellow:
                YellowNoteStorage.Add(noteIndex);
                break;
            case ColorList.Green:
                GreenNoteStorage.Add(noteIndex);
                break;
            case ColorList.Blue:
                BlueNoteStorage.Add(noteIndex);
                break;
            case ColorList.Violet:
                VioletNoteStorage.Add(noteIndex);
                break;
            case ColorList.Gray:
                break;
            default:
                break;
        }
    }


    public void RemoveTheNoteInListsDestroy(GameObject noteIndex)
    {
        GameController.Instance.AllNotes.RemoveAt(0);
        switch (noteIndex.GetComponent<NoteMovement>().NoteColor)
        {
            case ColorList.Red:
                RedNoteStorage.RemoveAt(0);
                break;
            case ColorList.Orange:
                OrangeNoteStorage.RemoveAt(0);
                break;
            case ColorList.Yellow:
                YellowNoteStorage.RemoveAt(0);
                break;
            case ColorList.Green:
                GreenNoteStorage.RemoveAt(0);
                break;
            case ColorList.Blue:
                BlueNoteStorage.RemoveAt(0);
                break;
            case ColorList.Violet:
                VioletNoteStorage.RemoveAt(0);
                break;
            case ColorList.Gray:
                break;
            default:
                break;
        }
        Destroy(noteIndex.gameObject);
    }

}
