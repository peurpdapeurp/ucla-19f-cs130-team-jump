using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalkingPatrol : MonoBehaviour
{
    float gravity = 10; // gravity acceleration

    public float speed;
    public Transform groundDetection;
    public float forceScalar;

    private bool movingRight = true;
    private Vector2 normal;

    private void FixedUpdate()
    {
        normal = transform.up;
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody2D>().AddForce(forceScalar * (-gravity * GetComponent<Rigidbody2D>().mass * normal));
        Debug.DrawRay(transform.position, normal, Color.magenta);
    }

    void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, -normal, 2, layerMask);
        Debug.DrawRay(groundDetection.position, -normal, Color.red);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            speed = -speed;
        }
        Debug.Log("Ground info's collider: " + groundInfo.collider);
    }
}