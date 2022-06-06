using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    [SerializeField] private GameObject _notePrefeb;
    [SerializeField] private int _laneNum;
    [SerializeField] private Color _laneColor;
    [SerializeField] private KeyCode _inputKey;
    [SerializeField] private NoteName _noteName;
    [SerializeField] private int _noteOctave;
    public Color LaneColor => _laneColor;
    public KeyCode InputKey => _inputKey;

    private string _reqNote;
    private Melanchall.DryWetMidi.Interaction.Note[] _notes;
    private TempoMap _tempoMap;
    private List<double> _times;

    public SongManager SongManager;
    private double _currentAudioTime;

    // Start is called before the first frame update
    void Start()
    {
        _times = new List<double>();

        string noteName = _noteName.ToString().Replace("Sharp", "#");
        _reqNote = noteName + _noteOctave.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _currentAudioTime = SongManager.GetAudioSourceTime();

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
            if (_currentAudioTime >= _times[0])
            {
                CreateNote();
                _times.RemoveAt(0);
            }
        }
    }
    void CreateNote()
    {
        GameObject newNote = Instantiate(_notePrefeb, transform.position, Quaternion.identity);
        newNote.GetComponent<SpriteRenderer>().color = _laneColor;
        newNote.gameObject.tag = "NoteLane" + _laneNum;
    }
}
