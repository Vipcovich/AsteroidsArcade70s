using UnityEngine;
using System.Collections;

public static class ArrayExtension
{
    public static T Random<T>(this T[] array)
    {
        var count = array.Length;
        return (count > 0) ? array[UnityEngine.Random.Range(0, count)] : default(T);
    }
}
