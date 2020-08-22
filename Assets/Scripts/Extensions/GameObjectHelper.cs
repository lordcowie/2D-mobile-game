using UnityEngine;

public static class GameObjectHelper
{
    public static bool HasComponent<T>(this GameObject g)
    {
        return g.GetComponent<T>() != null;
    }

    public static bool HasComponent<T>(this GameObject g, out T c)
    {
        c = g.GetComponent<T>();
        return c != null;
    }
}
