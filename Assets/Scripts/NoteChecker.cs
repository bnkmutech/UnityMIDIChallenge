using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = default;

    private bool isPlaying = false;
    private Rigidbody gameCameraRb = default;
    private float velocity = default;

    public void Active(Rigidbody gameCameraRb, float velocity)
    {
        if (!isPlaying)
        {
            this.gameCameraRb = gameCameraRb;
            this.velocity = velocity;
            isPlaying = true;
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        if (isPlaying)
        {
            gameCameraRb.velocity = new Vector3(0, velocity, 0);
        }
    }
}
