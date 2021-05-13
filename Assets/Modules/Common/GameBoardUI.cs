using UnityEngine;
using Zenject;

public class GameBoardUI : MonoBehaviour
{
    public Transform laneSlot;
    public Transform rhythmField;
    
    public class Factory : PlaceholderFactory<GameBoardUI>
    {
        
    }
}