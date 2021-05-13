using System.Collections.Generic;
using UnityEngine.InputSystem;

public interface IRhythmLaneController
{
    float currentTime { set; }
    void Renew(RhythmLaneSettings laneSettings);
    void AddPalette(RhythmPalette palette);
    void OnButtonPressed(InputAction.CallbackContext context);
    void OnButtonReleased(InputAction.CallbackContext context);
}