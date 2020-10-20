using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody;
    [Space]
    [SerializeField] private MinMax velocity = new MinMax(1f, 5f);
    [SerializeField] private float  accel = 1f;
    [SerializeField] private float  autoDeaccel = 0.1f;
    [SerializeField] private float  angelAccel = 1f;
    [SerializeField] private float  autoAngleDeaccel = 0.1f;
    [Space]
    [SerializeField] private KeyCode runKey     = KeyCode.UpArrow;
    [SerializeField] private KeyCode stopKey    = KeyCode.DownArrow;
    [SerializeField] private KeyCode leftKey    = KeyCode.LeftArrow;
    [SerializeField] private KeyCode rightKey   = KeyCode.RightArrow;

    private void Update()
    {
        rigidbody.velocity -= rigidbody.velocity * autoDeaccel * Time.deltaTime;
        rigidbody.angularVelocity -= rigidbody.angularVelocity * autoAngleDeaccel * Time.deltaTime;

        if (Input.GetKey(runKey))
        {
            rigidbody.AddForce((Vector2)transform.up * accel * Time.deltaTime);
        }

        if (Input.GetKey(stopKey))
        {
            rigidbody.AddForce(-(Vector2)rigidbody.velocity.normalized * accel * Time.deltaTime);
        }

        if (Input.GetKey(leftKey))
        {
            rigidbody.AddTorque(angelAccel * Time.deltaTime);
        }

        if (Input.GetKey(rightKey))
        {
            rigidbody.AddTorque(-angelAccel * Time.deltaTime);
        }
    }
}
