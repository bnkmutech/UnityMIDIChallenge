using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private LaneScript[] _lanes;

    public System.Action OnPressNote;
    public System.Action OnTouchLine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTouchLine?.Invoke();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //get lane number from tag of note
        char tag = collision.gameObject.tag.Replace("NoteLane", "").ToCharArray()[0];
        int laneNumber = tag - '0' - 1;

        //check press key input on that lane
        bool pressLaneInputKey = Input.GetKey(_lanes[laneNumber].InputKey);
        if (pressLaneInputKey)
        {
            //destroy pressed note
            Destroy(collision.gameObject);
            OnPressNote?.Invoke();
        }
    }
}
