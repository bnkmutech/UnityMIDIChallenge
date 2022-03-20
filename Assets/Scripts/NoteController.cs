using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float fallingSpeed;
    public float startingTime = float.MaxValue;

    private void FixedUpdate()
    {
        if (Time.fixedTime > startingTime)
        {
            transform.position = transform.position - new Vector3(0, fallingSpeed, 0);
        }
    }

    public void OnKeyHit()
    {
        Destroy(gameObject);
    }
}