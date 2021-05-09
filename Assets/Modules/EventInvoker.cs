using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    [Serializable]
    public enum InvokeCycle
    {
        Awake,
        Start,
        None
    }
    
    [SerializeField] private float _delay = 0f;
    [SerializeField] private InvokeCycle _cycle;
    [SerializeField] private UnityEvent _event;

    private async void Awake()
    {
        if (_cycle != InvokeCycle.Awake)
            return;

        await Task.Delay(Mathf.CeilToInt(1000 * _delay));
        
        _event.Invoke();
    }
    private async void Start()
    {
        if (_cycle != InvokeCycle.Start)
            return;

        await Task.Delay(Mathf.CeilToInt(1000 * _delay));
        
        _event.Invoke();
    }

    public async void Invoke()
    {
        if (_cycle != InvokeCycle.None)
            return;

        await Task.Delay(Mathf.CeilToInt(1000 * _delay));
        
        _event.Invoke();
    }
}