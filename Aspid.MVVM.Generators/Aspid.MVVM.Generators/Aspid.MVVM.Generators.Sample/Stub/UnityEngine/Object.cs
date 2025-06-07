namespace UnityEngine;

public class Object
{
    public static T Instantiate<T>(T original)
        where T : Object
    {
        return default;
    }

    public static T Instantiate<T>(
        T original,
        Transform parent)
        where T : Object
    {
        return default;
    }

    public static T Instantiate<T>(
        T original,
        Transform parent,
        bool worldPositionStays)
        where T : Object
    {
        return default;
    }

    public static T Instantiate<T>(
        T original,
        Vector3 position,
        Quaternion rotation)
        where T : Object
    {
        return default;
    }

    public static T Instantiate<T>(
        T original,
        Vector3 position,
        Quaternion rotation,
        Transform parent)
        where T : Object
    {
        return default;
    }

    public static void Destroy(Object obj) { }

    public static implicit operator bool(Object? exists) => exists != null;
}