using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Damage
{
    public float            Value = 1f;
    public bool             OwnerIsPlayer = false;
    public ContactPoint2D   ContactPoint;
    public Rigidbody2D      Rigidbody;      
}