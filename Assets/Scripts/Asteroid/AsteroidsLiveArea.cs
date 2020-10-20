using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AsteroidsLiveArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Asteroid>())
        {
            Destroy(collision.gameObject);
        }
    }
}
