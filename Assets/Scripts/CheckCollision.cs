using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private LaneScript[] _lanes;

    private void OnTriggerStay2D(Collider2D collision)
    {
        char tag = collision.gameObject.tag.Replace("NoteLane", "").ToCharArray()[0];
        int laneNumber = tag - '0' - 1;

        bool pressLaneInputKey = Input.GetKey(_lanes[laneNumber].InputKey);
        if (pressLaneInputKey)
        {
            Destroy(collision.gameObject);
        }
    }
}
