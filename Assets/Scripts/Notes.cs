using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    private SongMaster songMaster;
    private ButtonControl buttonControl;
    private float musicTime;
    private bool isHitZone = false;
    private float outOfBound = -5.76f;

    // Start is called before the first frame update
    private void Awake()
    {
        songMaster = GameObject.Find("SongMaster").GetComponent<SongMaster>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * songMaster.speed);

        if (gameObject.tag != "Untagged" && songMaster.checkButton[gameObject.tag].isPressed && isHitZone)
        {
            Destroy(gameObject);
        }

        if (transform.position.y <= outOfBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isHitZone = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isHitZone = false;
    }
}
