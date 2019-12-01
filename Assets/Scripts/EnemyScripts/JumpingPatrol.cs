using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpingPatrol : Patrol
{
    public float jumpForce = 4000f;
    public float maxJumpInterval = 5;

    private float forceScalar = 500f;
    private bool grounded;
    private Rigidbody2D rigidbody2D;
    private float sideDetectionRayLength;
    private Vector2 normal;
    private float gravityForce = 5f;

    /**
     * \brief The Awake function starts the jumping loop through the JumpRoutine coroutine.
     */
    public void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
        rigidbody2D.mass = 3f;
        rigidbody2D.gravityScale = gravityForce;
    }

    public void Start()
    {
        base.Start();
        sideDetectionRayLength = (h / 2) + (0.05f * h);
    }

    /**
     * \brief The FixedUpdate function periodically checks if the object is in contact with any surface, and sets m_Grounded to true
     * if it is.
     */
    public void FixedUpdate()
    {
        grounded = false;

        Debug.DrawRay(transform.position, new Vector2(0, -sideDetectionRayLength), Color.red);
        RaycastHit2D groundInfoBottom = Physics2D.Raycast(transform.position, 
                                                          new Vector2(0, -1),
                                                          sideDetectionRayLength, 
                                                          whatIsGround);
        RaycastHit2D groundInfoLeft = Physics2D.Raycast(transform.position, 
                                                        new Vector2(-1, 0),
                                                        sideDetectionRayLength, 
                                                        whatIsGround);
        RaycastHit2D groundInfoRight = Physics2D.Raycast(transform.position, 
                                                         new Vector2(1, 0),
                                                         sideDetectionRayLength, 
                                                         whatIsGround);
        RaycastHit2D groundInfoTop = Physics2D.Raycast(transform.position, 
                                                       new Vector2(0, 1),
                                                       sideDetectionRayLength, 
                                                       whatIsGround);
        grounded = groundInfoBottom.collider || groundInfoLeft.collider || groundInfoRight.collider || groundInfoTop.collider;
        normal = new Vector2(0, 0);
        if (groundInfoBottom.collider) 
            normal = new Vector2(0, 1);
        else if (groundInfoLeft.collider) 
            normal = new Vector2(1, 0);
        else if (groundInfoRight.collider) 
            normal = new Vector2(-1, 0);
        else if (groundInfoTop.collider)
            normal = new Vector2(0, -1);
        Debug.DrawRay(transform.position, normal);
        if (grounded)
        {
            //rigidbody2D.gravityScale = 0f;
            GetComponent<Rigidbody2D>().AddForce(forceScalar * (-1 * GetComponent<Rigidbody2D>().mass * normal));
        }
        /*
        else
        {
            rigidbody2D.gravityScale = gravityForce;
        }
        */
    }

    /**
     * \brief The update function does nothing.
     */
    public void Update()
    {
    }

    /**
     * \brief The move function does nothing.
     */
    public void Move()
    {
        
    }

    /**
     * \brief A coroutine which periodically makes the object jump if it is grounded. The object will attempt to jump at an interval from 
     * 0 to m_MaxJumpInterval seconds. The object will randomly jump either to the left, straight up, or to the right. The object
     * will not jump if it is not in contact with a surface (i.e. m_Grounded is false).
     */
    IEnumerator JumpRoutine()
    {
        while (true)
        {
            TryDeallocate();
            bool jump = true;
            if (grounded && jump)
            {
                grounded = false;
                Vector3 jumpVector =
                    new Vector3(
                        Random.Range(-1, 2) * 0.5f * jumpForce * (normal[0] == 0 ? 1 : 2),
                        jumpForce * Mathf.Sign(normal[1]) * (normal[0] == 0 ? 1 : 0.5f),
                        0);
                Debug.Log("normal: " + normal);
                Debug.Log("jump vector: " + jumpVector);
                rigidbody2D.AddForce(jumpVector);
            }
            yield return new WaitForSeconds(Random.Range(0, maxJumpInterval));
        }
    }
}

