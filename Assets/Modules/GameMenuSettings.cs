using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameMenuSettings
{
    public KeyCode pauseKey;
    public TrackData[] tracks;
    public TrackButton trackButtonPrefab;
    public Transform trackButtonSlot;
    public PauseMenuCanvas gamePauseMenu;
    public GameObject trackMenu;
    public GameObject pauseHint;
}