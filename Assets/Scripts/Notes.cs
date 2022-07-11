using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private SongMaster songMaster;
    private ButtonControl buttonControl;
    private float musicTime;
    private float outOfBound = -5.76f;

    private void Awake()
    {
        songMaster = GameObject.Find("SongMaster").GetComponent<SongMaster>();
    }

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * songMaster.speed);

        if (transform.position.y <= outOfBound)
        {
            Destroy(gameObject);
        }
    }
}
