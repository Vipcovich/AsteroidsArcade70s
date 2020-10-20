using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class MinMax
{
    public float min = 0f;
    public float max = 1f;

    public MinMax()
    {
    }

    public MinMax(float minValue, float maxValue)
    {
        min = minValue;
        max = maxValue;
    }

    public float Random()
    {
        return UnityEngine.Random.Range(min, max);
    }
}
