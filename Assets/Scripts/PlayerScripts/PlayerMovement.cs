using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script to control the player's movement and animations. </para>  	
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The controller object used to take in input and control the player.
    /// </summary>
    public CharacterController2D controller;
    /// <summary>
    /// The Animator object used to control the player animation.
    /// </summary>
    public Animator animator;
    /// <summary>
    /// The scale for the movement speed of the player sprite.
    /// </summary>
    public float runSpeed = 40f;

    /// <summary>
    /// The horizontal speed of the player.
    /// </summary>
    float horizontalMove = 0;
    /// <summary>
    /// A simple variable to log whether the player is in the middle of jumping.
    /// </summary>
    bool jump = false;
    /// <summary>
    /// A simple variable to log whether the player is in the middle of falling.
    /// </summary>
    bool crouch = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /// <summary>
        /// Function that is called once per frame in order to continuously update the state of the player in the current frame.
        /// Such conditions would include whether the player is running, jumping, falling, or crouching, and would would also
        /// notify the Animator object what state the player is in to have the animation correspond to the current state.
        /// </summary>
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("IsCrouching", false);
        }
        if(Physics2D.Raycast(transform.position, Vector2.down, 1.5f, (1 << LayerMask.NameToLayer("Level"))))
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else
        {
            float vertSpeed = gameObject.GetComponent<Rigidbody2D>().velocity.y;
            if(vertSpeed > 0.75f)
            {
                animator.SetBool("IsJumping", true);
                animator.SetBool("IsFalling", false);
            }
            else if (vertSpeed < -0.75f)
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
                animator.SetBool("IsFalling", false);
            }
        }
        
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// Function that is called a fixed number of times per second in order to update the player movement at a constant
        /// real-time basis (unlike with the Update function which is dependent on the FPS).
        /// </summary>
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
