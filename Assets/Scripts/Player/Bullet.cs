using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float damageValue  = 1f;
    [SerializeField] private float maxTime      = 2f;
    [Space]
    [SerializeField] private GameObject boomVfx;

    private float delay;

    private void Awake()
    {
        delay = maxTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var victim = collision.gameObject.GetComponent<IVictim>();
        if (victim != null)
        {
            var damage = new Damage
            {
                Value           = damageValue,
                Rigidbody       = collision.gameObject.GetComponent<Rigidbody2D>(),
                ContactPoint    = collision.contacts.First(),
                OwnerIsPlayer   = true,
            };

            victim.OnDamage(damage);
        }

        if (boomVfx)
        {
            GameObject.Instantiate(boomVfx, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        delay -= Time.deltaTime;
        if (delay <= 0)
        {
            Destroy(gameObject);
        }
    }
}
