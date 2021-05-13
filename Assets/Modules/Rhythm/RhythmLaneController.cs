using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class RhythmLaneController : IRhythmLaneController
{
    public float currentTime
    {
        set => _currentTime = value;
    }

    private bool _isPressed;
    private float _currentTime;

    private SignalBus _signalBus;
    private RhythmLaneSettings _laneSettings;
    
    private List<RhythmPalette> _palettes = new List<RhythmPalette>();
    private List<RhythmPalette> _discarded = new List<RhythmPalette>();
    private List<RhythmPalette> _pressedRhythm = new List<RhythmPalette>();

    public RhythmLaneController(SignalBus signalBus, RhythmLaneSettings laneSettings)
    {
        _signalBus = signalBus;
        _laneSettings = laneSettings;
    }

    public void Renew(RhythmLaneSettings laneSettings)
    {
        _palettes.Clear();
        _laneSettings = laneSettings;
    }
    public void AddPalette(RhythmPalette palette)
    {
        if (_laneSettings.targetNotes.All(notes => !notes.IsMatch(palette.note)))
            return;
        
        _palettes.Add(palette);
    }
    public void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (_palettes.Count <= 0)
            return;
        
        for (int i = 0; i < _palettes.Count; i++)
        {
            var lateInput = (_palettes[i].startTime + 0.15f) >= _currentTime;
            var earlyInput = (_palettes[i].startTime - 0.15f) <= _currentTime;
            
            if (lateInput && earlyInput)
            {
                //Note: Implementation of long note press logic
                // if (_palettes[i].isLongNote)
                //     _pressedRhythm.Add(_palettes[i]);
                // else
                //     _signalBus.Fire(new NoteInteractSignal(_palettes[i].id));
                //
                _signalBus.Fire(new NoteInteractSignal(_palettes[i].id, _palettes[i].note));
                _palettes.RemoveAt(i);
            }
        }
    }
    public void OnButtonReleased(InputAction.CallbackContext context)
    {
        //Note: Implementation of long note release logic
        
        // if (_pressedRhythm.Count <= 0)
        //     return;
        //
        // for (int i = 0; i < _pressedRhythm.Count; i++)
        // {
        //     var lateRelease = (_palettes[i].endTime + 0.3f) >= _currentTime;
        //     var earlyRelease = (_palettes[i].endTime - 0.3f) <= _currentTime;
        //
        //     if (lateRelease && earlyRelease)
        //     {
        //         _pressedRhythm.RemoveAt(i);
        //     }
        // }
    }
}