using UnityEngine;

public class DespawnerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out NoteController noteController))
        {
            Destroy(col.gameObject);
        }
    }
}