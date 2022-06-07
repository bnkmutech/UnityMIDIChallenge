using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    //lane info
    [SerializeField] private GameObject _notePrefeb;
    [SerializeField] private int _laneNum;
    [SerializeField] private Color _laneColor;
    [SerializeField] private KeyCode _inputKey;
    [SerializeField] private NoteName _noteName;
    [SerializeField] private int _noteOctave;
    public Color LaneColor => _laneColor;
    public KeyCode InputKey => _inputKey;

    [SerializeField] private SongManager _songManager;
    private string _reqNote; //noteName + noteOctave
    private Melanchall.DryWetMidi.Interaction.Note[] _notes;
    private TempoMap _tempoMap;
    private List<double> _times;
    private double _timer = 0.0d;
    private float _pitch = 1;
    public double Timer => _timer;

    private void OnEnable()
    {
        _songManager.OnPressRestart += ResetTimer;
    }
    private void OnDisable()
    {
        _songManager.OnPressRestart -= ResetTimer;
    }

    // Start is called before the first frame update
    void Start()
    {
        _times = new List<double>();

        //merge noteName and noteOctave to reqNote
        string noteName = _noteName.ToString().Replace("Sharp", "#");
        _reqNote = noteName + _noteOctave.ToString();

        _pitch = _songManager.Pitch;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();
        SpawnAtTime();
    }

    //deal with midi data
    public void ReceivedMidiData(Melanchall.DryWetMidi.Interaction.Note[] noteArr, TempoMap tempoMap)
    {
        _notes = noteArr;
        _tempoMap = tempoMap;

        AddTimeToList();
    }
    void AddTimeToList()
    {
        foreach (var note in _notes)
        {
            double timeInSeconds = ((TimeSpan)note.TimeAs<MetricTimeSpan>(_tempoMap)).TotalSeconds;
            if (note.ToString() == _reqNote)
            {
                _times.Add(timeInSeconds);
            }
        }
    }
    //spawn note
    void SpawnAtTime()
    {
        if(_times.Count > 0)
        {
            //checkk is note time from midi match with real play time
            if (_timer >= _times[0])
            {
                CreateNote();
                _times.RemoveAt(0); //remove time that note already spawn
            }
        }
    }
    void CreateNote()
    {
        GameObject newNote = Instantiate(_notePrefeb, transform.position, Quaternion.identity);
        newNote.GetComponent<SpriteRenderer>().color = _laneColor;
        newNote.gameObject.tag = "NoteLane" + _laneNum;
    }
    //count time
    void CountTime()
    {
        _timer += (Time.deltaTime * _pitch);
    }
    //restart
    void ResetTimer()
    {
        _timer = 0;
    }
}
