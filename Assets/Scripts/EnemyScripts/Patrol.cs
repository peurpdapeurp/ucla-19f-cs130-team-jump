using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask whatIsGround;
    private float groundDetectionXOffset;
    private float groundDetectionYOffset;
    private float groundDetectionRayLengthScalar;
    private Vector2 normal;

    protected bool movingRight = true;

    public void Start()
    {
        var p1 = gameObject.transform.TransformPoint(0, 0, 0);
        var p2 = gameObject.transform.TransformPoint(1, 1, 0);
        var w = p2.x - p1.x;
        var h = p2.y - p1.y;
        groundDetectionXOffset = w * 6;
        groundDetectionYOffset = h * 5;
        groundDetectionRayLengthScalar = h * 4;
    }

    /**
     * \brief The update function simply calls the Move function.
     */
    public void Update()
    {
        normal = transform.up;
        Move();
    }

    /**
     * \brief Moves the object which the script is attached to. If the object detects that it has reached the end of the
     *  platform or wall it is currently patrolling, it will turn around and move in the opposite direction.
     */
    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        Vector3 groundRayStart = new Vector3(transform.position[0] + groundDetectionXOffset, 
                                             transform.position[1] - groundDetectionYOffset,
                                             transform.position[2]);
        Vector2 groundRay = new Vector2(normal[0], -1 * normal[1] * groundDetectionRayLengthScalar);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundRayStart, groundRay, groundDetectionRayLengthScalar, whatIsGround);
        Debug.DrawRay(groundRayStart, groundRay, Color.red);
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
            groundDetectionXOffset = -groundDetectionXOffset;
        }
        Debug.Log("Ground info's collider: " + groundInfo.collider);
    }
}
