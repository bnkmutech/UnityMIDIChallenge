using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Zenject;

public class RhythmLaneInputWorker
{
    [Inject] private LaneInputControls _inputControls;
    
    private List<Action<InputAction.CallbackContext>> _uiPressedUIActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _uiReleaseUIActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _logicPresseActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _logicReleaseActions = new List<Action<InputAction.CallbackContext>>();
    
    private List<Action<InputAction.CallbackContext>> _oldUiPressedUIActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _oldUiReleaseUIActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _oldLogicPresseActions = new List<Action<InputAction.CallbackContext>>();
    private List<Action<InputAction.CallbackContext>> _oldLogicReleaseActions = new List<Action<InputAction.CallbackContext>>();
    

    public void SetUIInput(List<LaneUI> laneUis)
    {
        for (int i = 0; i < _oldUiPressedUIActions.Count; i++)
            _inputControls.LaneControls.Get().actions[i].performed -= _oldUiPressedUIActions[i];
        
        for (int i = 0; i < _oldUiReleaseUIActions.Count; i++)
            _inputControls.LaneControls.Get().actions[i].canceled -= _oldUiReleaseUIActions[i];
        
        _oldUiPressedUIActions.Clear();
        _oldUiReleaseUIActions.Clear();
        
        for (int i = 0; i < laneUis.Count; i++)
        {
            laneUis[i].keyText = _inputControls.LaneControls.Get().actions[i].controls[0].name;
            _uiPressedUIActions.Add(laneUis[i].OnPressed);
            _uiReleaseUIActions.Add(laneUis[i].OnReleased);
            _oldUiPressedUIActions.Add(laneUis[i].OnPressed);
            _oldUiReleaseUIActions.Add(laneUis[i].OnReleased);
            _inputControls.LaneControls.Get().actions[i].performed += _uiPressedUIActions[i];
            _inputControls.LaneControls.Get().actions[i].canceled += _uiReleaseUIActions[i];
            
            if (!_inputControls.LaneControls.Get().actions[i].enabled)
                _inputControls.LaneControls.Get().actions[i].Enable();
        }
    }
    public void SetLogicInput(List<IRhythmLaneController> laneControllers)
    {
        for (int i = 0; i < _oldLogicPresseActions.Count; i++)
            _inputControls.LaneControls.Get().actions[i].performed -= _oldLogicPresseActions[i];
        
        for (int i = 0; i < _oldLogicReleaseActions.Count; i++)
            _inputControls.LaneControls.Get().actions[i].canceled -= _oldLogicReleaseActions[i];
        
        _oldLogicPresseActions.Clear();
        _oldLogicReleaseActions.Clear();
        
        for (int i = 0; i < laneControllers.Count; i++)
        {
            _logicPresseActions.Add(laneControllers[i].OnButtonPressed);
            _logicReleaseActions.Add(laneControllers[i].OnButtonReleased);
            _oldLogicPresseActions.Add(laneControllers[i].OnButtonPressed);
            _oldLogicReleaseActions.Add(laneControllers[i].OnButtonReleased);
            _inputControls.LaneControls.Get().actions[i].performed += _logicPresseActions[i];
            _inputControls.LaneControls.Get().actions[i].canceled += _logicReleaseActions[i];
            
            if (!_inputControls.LaneControls.Get().actions[i].enabled)
                _inputControls.LaneControls.Get().actions[i].Enable();
        }
    }
    public void Dispose()
    {
        for (int i = 0; i < _uiPressedUIActions.Count; i++)
        {
            _inputControls.LaneControls.Get().actions[i].performed -= _uiPressedUIActions[i];
            _inputControls.LaneControls.Get().actions[i].canceled -= _uiReleaseUIActions[i];
        }
        
        for (int i = 0; i < _logicPresseActions.Count; i++)
        {
            _inputControls.LaneControls.Get().actions[i].performed -= _logicPresseActions[i];
            _inputControls.LaneControls.Get().actions[i].canceled -= _logicReleaseActions[i];
            
            if (_inputControls.LaneControls.Get().actions[i].enabled)
                _inputControls.LaneControls.Get().actions[i].Disable();
        }
    }
}