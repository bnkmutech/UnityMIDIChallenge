using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public string noteName;
    private double timeInstantiated;
    private double timeSinceInstantiated;
    public float assignedTime;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Color chosen_color;
    private bool canDelete;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        timeInstantiated = SongManager.GetSongTime();
        sprite.enabled = false;
        if(SongManager.Instance.song.isPlaying == true) canDelete = true;
        else canDelete = false;
    }

    void Update()
    {
        sprite.color = chosen_color;
        timeSinceInstantiated = SongManager.GetSongTime() - timeInstantiated;
        float time = (float)(timeSinceInstantiated  / (SongManager.Instance.noteDisplayTime * 2));

        if(time > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnPosY, Vector3.up * SongManager.Instance.noteDespawnPosY, time); //Move notes from spawn position to despawn position.
            sprite.enabled = true;
        }

        if(SongManager.Instance.song.isPlaying == false && canDelete == true) Destroy(gameObject); //To delete all remain notes after song end. (Fixed Bug.)
    }
}
