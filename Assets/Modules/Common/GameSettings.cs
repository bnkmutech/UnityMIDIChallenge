using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create GameSettings", fileName = "GameSettings", order = 0)]
public class GameSettings : ScriptableObject
{
    public float startDelay = 3f;
    public List<Color> laneColors = new List<Color>();
    public LaneUI laneUIPrefab;
    public LaneRhythm laneRhythmPrefab;
}