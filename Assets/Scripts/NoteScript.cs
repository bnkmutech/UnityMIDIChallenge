using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    [SerializeField] private SongManager _songManager;
    private float _noteVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _noteVelocity = _songManager.NoteVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _noteVelocity * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
