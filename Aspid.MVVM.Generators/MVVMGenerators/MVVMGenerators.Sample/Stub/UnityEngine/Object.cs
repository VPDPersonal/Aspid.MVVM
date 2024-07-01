namespace UnityEngine;

public class Object
{
    public static implicit operator bool(Object? exists) => exists != null;
}