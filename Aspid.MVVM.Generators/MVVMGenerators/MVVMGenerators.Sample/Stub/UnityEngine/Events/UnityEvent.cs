using System;

namespace UnityEngine.Events;

public class UnityEvent
{
    public void AddListener(Action call) { }
    
    public void RemoveListener(Action call) { }
}

public class UnityEvent<T1>
{
    public void AddListener(Action<T1> call) { }
    
    public void RemoveListener(Action<T1> call) { }
}