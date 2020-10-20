using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerBounds : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float  margin = 0.03f;

    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;
    }

    private void Update()
    {
        transform.position = camera.transform.position;

        var min = camera.ViewportToWorldPoint(Vector2.zero);
        var max = camera.ViewportToWorldPoint(Vector2.one);
        collider.size = (Vector2)(max - min) * (1f - margin);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if (obj.GetComponent<Fighter>())
        {
            var min = collider.bounds.min;
            var max = collider.bounds.max;
            var size = max - min;

            var pos = obj.transform.position;
            if (pos.x >= max.x) pos.x -= size.x;
            if (pos.x <  min.x) pos.x += size.x;
            if (pos.y >= max.y) pos.y -= size.y;
            if (pos.y <  min.y) pos.y += size.y;

            obj.transform.position = pos;
        }
    }
}
