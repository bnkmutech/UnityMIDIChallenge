using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    SpriteRenderer noteSprite;
    Color color;
    void Start()
    {
        timeInstantiated = assignedTime - SongManager.Instance.noteTime;
        noteSprite = GetComponent<SpriteRenderer>();
        noteSprite.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float lerpTime = (float)(timeSinceInstantiated / (SongManager.Instance.noteTime * 2));

        if (lerpTime > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, lerpTime); 
        }
    }
    public void SetColor(Color color)
    {
        this.color = color;
    }
}
