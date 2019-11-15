using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;

    private bool movingRight = true;

    public Transform groundDetection;

    /**
     * \brief The update function simply calls the Move function.
     */
    public void Update()
    {
        Move();
    }

    /**
     * \brief Moves the object which the script is attached to. If the object detects that it has reached the end of the
     *  platform or wall it is currently patrolling, it will turn around and move in the opposite direction.
     */
    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2, layerMask);
        if (groundInfo.collider == false)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        Debug.Log("Ground info's collider: " + groundInfo.collider);
    }
}
