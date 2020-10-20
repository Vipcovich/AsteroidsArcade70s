using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour, IVictim
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject deathVfx;

    public static event Action<Fighter, Damage> OnKillEvent;

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

        if (deathVfx)
        {
            GameObject.Instantiate(deathVfx, transform.position, Quaternion.identity);
        }
    }
}
