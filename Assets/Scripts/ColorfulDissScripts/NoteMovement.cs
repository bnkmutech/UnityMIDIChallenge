using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    [SerializeField] private float m_Duration = 2f, m_percentage;
    [SerializeField] [Range(1,5)] private float m_Multiplier;

    public ColorList NoteColor;
    public float Percentage { get { return m_percentage; } }
    private Vector3 SpawnPointPos, TargetPointPos;
    private float m_current, m_start = 0f;

    private void Awake()
    {
        SpawnPointPos.x = this.transform.position.x;
        SpawnPointPos.y = this.transform.position.y;
        SpawnPointPos.z = this.transform.position.z;

        TargetPointPos.x = GameController.Instance.DummyRainbowBtn[(int)NoteColor].transform.position.x;
        TargetPointPos.y = GameController.Instance.DummyRainbowBtn[(int)NoteColor].transform.position.y;
        TargetPointPos.z = GameController.Instance.DummyRainbowBtn[(int)NoteColor].transform.position.z;

        m_current = m_start;
    }



    // Update is called once per frame
    void Update()
    {
        m_percentage = m_current / m_Duration;
        if (SpawnController.Instance.IsGameBegin) 
        {
            if (m_current < m_Duration)
            {
                m_current += Time.deltaTime * m_Multiplier;
                this.transform.position = Vector3.Lerp(SpawnPointPos, TargetPointPos, m_percentage);
            }
            else if (m_current >= m_Duration)
            {
                if (GameController.isCheatMode) return;
                GameController.Instance.PlaySoundReduceHP();
                GameController.Instance.CalculateScore(ScoreType.Miss);
                SpawnController.Instance.RemoveTheNoteInListsDestroy(this.gameObject);
            }
        }


    }





}
