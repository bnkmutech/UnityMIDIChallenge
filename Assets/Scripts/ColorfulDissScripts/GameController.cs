using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get { return _instance; } }
    private static GameController _instance;
    public static bool isCheatMode = false;

    public bool isGameOver;
    [SerializeField] private int m_IntroAt, m_VerseTransitionAt, m_VerseAt, m_OutroAt;
    public int  TotalNotePlayed;
    public AudioClip[] IntroTotalNotesList, VerseTransitionTotalNotesList, VerseTotalNotesList,  OutroTotalNotesList;
    public GameObject[] DummyRainbowBtn, Note;
    public Color[] ButtonColors;
    public KeyCode[] key;
    public List<GameObject> AllNotes;
    public int HPRemaining, MaxHP = 7;
    public float ScoreCount;

    public SongState CurrentSongState;

    private UIController uiController;
    private float CurrentGameDuration;
    private int NotePlayedSectionCount = 0;


    private void Awake()
    {
        HPRemaining = MaxHP;
        isGameOver = false;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        uiController = GameObject.FindObjectOfType<UIController>();
        SetDummyBtnColor();
        //CalculateThisSongTotalNote();
        //CheckSongState();
        ScoreCount = 0;
    }


    private void SetDummyBtnColor()
    {
        for (int i = 0; i < DummyRainbowBtn.Length; i++)
        {
            if (null != DummyRainbowBtn[i].GetComponent<SpriteRenderer>()) DummyRainbowBtn[i].GetComponent<SpriteRenderer>().color = ButtonColors[i];
            //else if (null != DummyRainbowBtn[i].GetComponent<Image>()) DummyRainbowBtn[i].GetComponent<Image>().color = ButtonColors[i];
        }
    }
    private void SetUINotePressKey()
    {
        for (int i = 0; i < DummyRainbowBtn.Length; i++)
        {
            if (Input.GetKey(key[i])) DummyRainbowBtn[i].GetComponent<SpriteRenderer>().color = ButtonColors[(int)ColorList.Gray];
            if (Input.GetKeyUp(key[i])) DummyRainbowBtn[i].GetComponent<SpriteRenderer>().color = ButtonColors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetUINotePressKey();
        CheckCheatMode();
    }

    private void CheckCheatMode() 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            isCheatMode = !isCheatMode;
            SceneManager.LoadScene("GameScene");
            uiController = GameObject.FindObjectOfType<UIController>();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SpawnController.Instance.IsGameBegin = false;
            isGameOver = true;
            uiController.ShowGameoverPanelDelay();
        }

    }

    public void CheckSongState() 
    {
        m_IntroAt = IntroTotalNotesList.Length;
        m_VerseTransitionAt = m_IntroAt + VerseTransitionTotalNotesList.Length;
        m_VerseAt = m_VerseTransitionAt + VerseTotalNotesList.Length;
        m_OutroAt = m_VerseAt + OutroTotalNotesList.Length;

        if (TotalNotePlayed > 0 && TotalNotePlayed < m_IntroAt)
            CurrentSongState = SongState.Intro;
        else if (TotalNotePlayed > m_IntroAt && TotalNotePlayed < m_VerseTransitionAt)
            CurrentSongState = SongState.VerseTransition;
        else if (TotalNotePlayed > m_VerseTransitionAt && TotalNotePlayed < m_VerseAt)
            CurrentSongState = SongState.Verse;
        else if (TotalNotePlayed > m_VerseAt && TotalNotePlayed < m_OutroAt)
            CurrentSongState = SongState.Outro;
    }

    private void PlayNoteInSection(AudioClip[] noteListToPlay, int sectionLoopCount = 0) 
    {
        if (NotePlayedSectionCount >= noteListToPlay.Length)
        {
            NotePlayedSectionCount = 0;
            if (sectionLoopCount == 0)
            {
                CurrentSongState++;
                PlayTheSound();
            }
            else if (sectionLoopCount > 0) 
            {
                SectionVerseLoopCount= sectionLoopCount - 1;
                PlayTheSound(SectionVerseLoopCount);
            }
            
        }
        else
        {
            SoundController.Instance.Play(noteListToPlay[NotePlayedSectionCount].name);
            NotePlayedSectionCount++;
        }
    }

    public int TotalNoteToPlay() 
    {
        return IntroTotalNotesList.Length + 
               VerseTransitionTotalNotesList.Length + 
               VerseTotalNotesList.Length * (3) + 
               OutroTotalNotesList.Length;
    }

    private int SectionVerseLoopCount = 2;

    public void PlayTheSound(int sectionLoopCount = 0) 
    {
        switch (CurrentSongState)
        {
            case SongState.Intro:
                PlayNoteInSection(IntroTotalNotesList);
                break;
            case SongState.VerseTransition:
                PlayNoteInSection(VerseTransitionTotalNotesList);
                break;
            case SongState.Verse:
                PlayNoteInSection(VerseTotalNotesList, SectionVerseLoopCount);
                break;
            case SongState.Outro:
                PlayNoteInSection(OutroTotalNotesList);
                break;
            default:
                SpawnController.Instance.IsGameBegin = false;
                uiController.ShowGameoverPanelDelay();
                break;
        }
    }

    public void PlaySoundReduceHP() 
    {
        if (GameController.isCheatMode) return;
        if (HPRemaining <= 0) 
        {
            Debug.Log("Game Over");
            SpawnController.Instance.IsGameBegin = false;
            isGameOver = true;
            uiController.ShowGameoverPanelDelay();
        }
        if (HPRemaining > 0)
            HPRemaining--;
        uiController.BlinkThePanel();
        uiController.SetHP();
        SoundController.Instance.Play("Wrong");
    }

    public void CalculateScore(ScoreType givingScore) 
    {
        switch (givingScore)
        {
            case ScoreType.PressingPerfect:
                ScoreCount += 5;
                break;
            case ScoreType.PressingGreat:
                ScoreCount += 3;
                break;
            case ScoreType.PressingFair:
                ScoreCount += 1;
                break;
            case ScoreType.Miss:
                ScoreCount -= 1;
                break;
            case ScoreType.PressingWrong:
                break;
            default:
                break;
        }
    }
}

public enum ColorList
{
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    Violet,
    Gray
}

public enum DummyTargetList
{
    DummyTargetCircle_Red,
    DummyTargetCircle_Orange,
    DummyTargetCircle_Yellow,
    DummyTargetCircle_Green,
    DummyTargetCircle_Blue,
    DummyTargetCircle_Violet
}

public enum NoteType
{
    Default,
    Long,
    Double,
    Continuous
}

public enum ScoreType
{
    PressingPerfect, PressingGreat, PressingFair,
    Miss,
    PressingWrong
}

public enum SongState
{
    Intro,
    VerseTransition,
    Verse,
    Outro, End
}
