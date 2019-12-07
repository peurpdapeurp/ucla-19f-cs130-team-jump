using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// <para> Script used to control behavior of jumping enemies. </para>  	
/// </summary>
public class JumpingPatrol : Patrol
{
    /// <summary>
    /// The force with which the enemy jumps.
    /// </summary>
    public float jumpForce = 4000f;
    /// <summary>
    /// The maximum interval between jumps.
    /// </summary>
    public float maxJumpInterval = 5;
    /// <summary>
    /// The force with which the enemy is pushed against walls it is sticking to.
    /// </summary>
    private float forceScalar = 500f;
    /// <summary>
    /// Whether or not the enemy is sticking against a wall.
    /// </summary>
    private bool grounded;
    /// <summary>
    /// The enemy's rigidBody2D, used to apply force to the enemy to make it jump.
    /// </summary>
    private Rigidbody2D rigidbody2D;
    /// <summary>
    /// The length of the rays the enemy uses to detect when it is against a surface.
    /// </summary>
    private float sideDetectionRayLength;
    /// <summary>
    /// Normal vector of the enemy, set to be perpendicular to whatever wall the enemy is sticking to.
    /// </summary>
    private Vector2 normal;
    /// <summary>
    /// The force of gravity which affects the enemy.
    /// </summary>
    private float gravityForce = 5f;

    /// <summary>
    /// The animator object to which the GameObject is attached to, which controls the animation of the enemy Gameobject.
    /// </summary>
    public Animator animator;

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
        /// <summary>
        /// Start function initializes the jumping enemy. Calls Patrol's start, and also sets the length of the jumping enemy's
        /// side detection rays. Starts the enemy's jumping coroutine.
        /// </summary>
        base.Start();
        sideDetectionRayLength = (h / 2) + (0.05f * h);
    }

    /**
     * \brief The FixedUpdate function periodically checks if the object is in contact with any surface, and sets m_Grounded to true
     * if it is.
     */
    public void FixedUpdate()
    {
        /// <summary>
        /// The FixedUpdate function periodically checks if the object is in contact with any surface, and sets m_Grounded to true
        /// if it is.
        /// </summary>
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
        {
            rigidbody2D.AddForce(new Vector2(0, -1), ForceMode2D.Impulse);
        }
        Debug.DrawRay(transform.position, normal);
        if (grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(forceScalar * (-1 * GetComponent<Rigidbody2D>().mass * normal));
            animator.SetBool("IsJumping", false);
        }
        if ((gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.75f))
        {
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }
    }

    /**
     * \brief The update function does nothing.
     */
    public void Update()
    {
        /// <summary>
        /// Does nothing. Overrides the Update function of the Patrol script, since the jumping patrol does not use the Update
        /// function to move.
        /// </summary>
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
        /// <summary>
        /// A coroutine which periodically makes the object jump if it is grounded. The object will attempt to jump at an interval from 
        /// 0 to m_MaxJumpInterval seconds.The object will randomly jump either to the left, straight up, or to the right. The object
        /// will not jump if it is not in contact with a surface(i.e.m_Grounded is false).
        /// </summary>
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
                Vector3 newScale = transform.localScale;
                if((jumpVector.x * newScale.x) > 0)
                {
                    newScale.x *= -1;
                    transform.localScale = newScale;
                }
                rigidbody2D.AddForce(jumpVector);
                if (jumpVector != Vector3.zero)
                {
                    animator.SetBool("IsJumping", true);
                }
            }
            yield return new WaitForSeconds(Random.Range(0, maxJumpInterval));
        }
    }
}

