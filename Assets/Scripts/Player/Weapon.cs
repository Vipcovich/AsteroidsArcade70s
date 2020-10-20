using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private KeyCode    fireKey = KeyCode.Space;
    [SerializeField] private Bullet     bullet;
    [SerializeField] private float      bulletVelocity = 10f;
    [SerializeField] private Transform  barrel;
    [SerializeField] private float      timeout = 0.5f;

    private float delay;

    private void Update()
    {
        delay -= Time.deltaTime;

        if (Input.GetKey(fireKey) && delay <= 0)
        {
            Fire();
        }
    }

    private void Fire()
    {
        delay = timeout;

        var newBullet = GameObject.Instantiate(bullet);
        newBullet.transform.position = barrel.position;
        newBullet.transform.rotation = barrel.rotation;
        newBullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletVelocity;
    }
}
