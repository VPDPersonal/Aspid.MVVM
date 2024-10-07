using System;

namespace UnityEngine;

public class Component : Object
{
    public T GetComponent<T>()
    {
        return default;
    }
    
    public T[] GetComponents<T>()
    {
        return Array.Empty<T>();
    }
    
    public T GetComponentInChildren<T>()
    {
        return default;
    }
    
    public T[] GetComponentsInChildren<T>()
    {
        return Array.Empty<T>();
    }
    
    public T GetComponentInParent<T>()
    {
        return default;
    }
    
    public T[] GetComponentsInParent<T>()
    {
        return Array.Empty<T>();
    }
}