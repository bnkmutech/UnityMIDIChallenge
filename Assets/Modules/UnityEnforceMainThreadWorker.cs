using System;
using System.Collections.Generic;
using Zenject;

public class UnityEnforceMainThreadWorker : IInitializable, ITickable, ILateDisposable
{
    private Queue<Action> _jobs;
    
    public void Tick()
    {
        if (_jobs.Count <= 0)
            return;
        
        _jobs?.Dequeue()?.Invoke();
    }
    public void AddJob(Action newJob)
    {
        _jobs.Enqueue(newJob);
    }
    public void Initialize()
    {
        _jobs = new Queue<Action>();
    }
    public void LateDispose()
    {
        _jobs.Clear();
        _jobs = null;
    }
}