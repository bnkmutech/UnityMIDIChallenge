using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInput : MonoBehaviour
{
    [SerializeField] private KeyCode m_PlayerKey;
    [SerializeField] private NoteType noteType;
    public bool IsReceiveInput;

    // Start is called before the first frame update
    private void Awake()
    {
        IsReceiveInput = false;
        SetKey();
    }

    // Update is called once per frame
    void Update()
    {
            IsReceiveInput = SpawnController.Instance.transform.GetChild(0).gameObject == this.gameObject;
            PressTheNote();
    }

    private void PressTheNote()
    {
        if (IsReceiveInput)
            switch (noteType)
            {
                case NoteType.Default:
                    foreach (var item in GameController.Instance.key)
                    {
                        if (Input.GetKeyDown(item))
                        {
                            if (item != m_PlayerKey) 
                            {
                                PressingWrong();
                                return;
                            } 
                            PressingRight();
                        }
                    }
                    break;
                case NoteType.Long:
                    if (Input.GetKey(m_PlayerKey))
                    {
                        Debug.Log("this hasn't been implemented yet. I'll just destroy it");
                        SpawnController.Instance.RemoveTheNoteInListsDestroy(this.gameObject);
                    }
                    break;
                case NoteType.Double:
                    break;
                case NoteType.Continuous:
                    break;
            }
    }


    private void SetKey() 
    {
        switch (this.gameObject.GetComponent<NoteMovement>().NoteColor)
        {
            case ColorList.Red:
                m_PlayerKey = KeyCode.A;
                break;
            case ColorList.Orange:
                m_PlayerKey = KeyCode.S;
                break;
            case ColorList.Yellow:
                m_PlayerKey = KeyCode.D;
                break;
            case ColorList.Green:
                m_PlayerKey = KeyCode.J;
                break;
            case ColorList.Blue:
                m_PlayerKey = KeyCode.K;
                break;
            case ColorList.Violet:
                m_PlayerKey = KeyCode.L;
                break;
            default:
                break;
        }
    }

    private ScoreType scoreType() 
    {
        if (this.gameObject.GetComponent<NoteMovement>().Percentage > 0.97f && this.gameObject.GetComponent<NoteMovement>().Percentage <= 1f)
            return ScoreType.PressingPerfect;
        else if (this.gameObject.GetComponent<NoteMovement>().Percentage > 0.9f && this.gameObject.GetComponent<NoteMovement>().Percentage <= 0.97f)
            return ScoreType.PressingGreat;
        else if (this.gameObject.GetComponent<NoteMovement>().Percentage > 0.3f && this.gameObject.GetComponent<NoteMovement>().Percentage <= 0.9f)
            return ScoreType.PressingFair;
        else
            return ScoreType.Miss;

    }

    private void PressingRight() 
    {
        Color tmp = GameController.Instance.ButtonColors[(int)ColorList.Gray + 1];
        this.gameObject.GetComponent<SpriteRenderer>().color = tmp;
        this.IsReceiveInput = false;
        GameController.Instance.CalculateScore(scoreType());
        GameController.Instance.CheckSongState();
        if (scoreType() != ScoreType.Miss) 
        {
            GameController.Instance.PlayTheSound();
            SpawnController.Instance.RemoveTheNoteInListsDestroy(this.gameObject);
            return;
        }
        if (GameController.isCheatMode) return;
        GameController.Instance.PlaySoundReduceHP();
    }

    private void PressingWrong() 
    {
        if (GameController.isCheatMode) return;
        GameController.Instance.PlaySoundReduceHP();
        GameController.Instance.CalculateScore(ScoreType.PressingWrong);
        SpawnController.Instance.SpawnCount--;
        //SpawnController.Instance.RemoveTheNoteInListsDestroy(this.gameObject);
    }
}
