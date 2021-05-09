using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create GameModeSettings", fileName = "GameModeSettings", order = 0)]
[Serializable]
public class GameMode : ScriptableObject
{
    public string modeName;
    public float laneDelay = 5f;
    public List<RhythmLaneSettings> laneSettings = new List<RhythmLaneSettings>();
}