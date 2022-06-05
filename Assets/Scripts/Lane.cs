using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    [SerializeField] private GameObject _notePrefeb;
    [SerializeField] private Color _laneColor;
    [SerializeField] private  KeyCode _inputKey;

    public Color LaneColor => _laneColor;
    public KeyCode InputKey => _inputKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_inputKey))
        {
            _notePrefeb.GetComponent<SpriteRenderer>().color = _laneColor;
            Instantiate(_notePrefeb, transform.position, Quaternion.identity);
        }
    }
}
