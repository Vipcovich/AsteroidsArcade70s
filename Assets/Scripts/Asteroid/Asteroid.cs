using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Asteroid : MonoBehaviour, IVictim
{
    [SerializeField] private float          health = 1f;
    [SerializeField] private float          collisionDamageValue = 1f;
    [SerializeField] private Rigidbody2D    rigidbody;
    [SerializeField] private int            score = 100;

    public static event Action<Asteroid, Damage> OnKillEvent;
    public static event Action<Asteroid> OnDestroyEvent;

    public Vector3 Velocity { get; private set; }
    public int Score => score;

    private void Start()
    {
        health *= transform.lossyScale.x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var victim = collision.gameObject.GetComponent<IVictim>();
        if (victim != null)
        {
            var damage = new Damage
            {
                Value           = collisionDamageValue,
                Rigidbody       = collision.gameObject.GetComponent<Rigidbody2D>(),
                ContactPoint    = collision.contacts.First(),
                OwnerIsPlayer   = false,
            };
            victim.OnDamage(damage);
        }
    }

    public void OnDamage(Damage damage)
    {
        health -= damage.Value;
        if (health <= 0)
        {
            Death(damage);
        }
    }

    private void Death(Damage lastDamage)
    {
        if (OnKillEvent != null) OnKillEvent.Invoke(this, lastDamage);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (OnDestroyEvent != null) OnDestroyEvent.Invoke(this);
    }

    private void Update()
    {
        Velocity = rigidbody.velocity;
    }
}
