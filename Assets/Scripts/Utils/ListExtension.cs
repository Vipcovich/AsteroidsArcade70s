using UnityEngine;
using System.Collections.Generic;

public static class ListExtension
{
    public static T Random<T>(this List<T> list)
    {
        var count = list.Count;
        return (count > 0) ? list[UnityEngine.Random.Range(0, count)] : default(T);
    }
}
