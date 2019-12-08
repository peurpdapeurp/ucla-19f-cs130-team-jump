using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the patrol movement of aerial enemies
/// </summary>
public class AerialPatrol : Patrol
{
    /// <summary>
    /// Flag to indicate vertical direction of enemy.
    /// </summary>
    private bool movingDown = true;
    /// <summary>
    /// The initial vertical position of the enemy GameObject.
    /// </summary>
    private float initialY;
    /// <summary>
    /// The current distance the enemy GameObject is from initialY.
    /// </summary>
    private float dist;
    /// <summary>
    /// Flag to set if the enemy is attacking. Flag will be used for animator and movement.
    /// </summary>
    private bool attack = false;
    /// <summary>
    /// The animator object to which the GameObject is attached to, which controls the animation of the enemy Gameobject.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// This function is called before the first frame update. It stores the initial position of the enemy
    /// </summary>
    void Start()
    {
        base.Start();
        initialY = transform.position.y;
        checkCollisions = false;
    }

    /// <summary>
    /// This function is called once per frame. It calculates the translation function, when to turn the other direction, and detects when to attack the player
    /// </summary>
    void Update()
    {
        if (!attack)
        {
            float offset = gameObject.transform.position.y - gameObject.transform.position.x;
            float playerOffset = GameObject.FindWithTag("Player").transform.position.y - GameObject.FindWithTag("Player").transform.position.x;
            if ((GameObject.FindWithTag("Player").transform.position.x < gameObject.transform.position.x) && ((playerOffset <= offset)))
            {
                attack = true;
                animator.SetBool("IsAttacking", true);
                speed = Mathf.Abs(speed);
            }
        }
        if (!attack)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            dist = initialY - transform.position.y;
            if (dist > 12)
            {
                if (movingDown)
                {
                    speed *= -1;
                    movingDown = false;
                }
            }
            else if (dist < -5)
            {
                if (!movingDown)
                {
                    speed *= -1;
                    movingDown = true;
                }
            }
        }
        else
        {
            transform.Translate(new Vector3(-3, -3, 0) * speed * Time.deltaTime);
        }
        TryDeallocate();
    }

    /// <summary>
    /// This function is called once per frame in the Update function, checking whether the enemy has left the leftmost boundary of the camera and needs to be deallocated.
    /// </summary>
    protected void TryDeallocate()
    {
        var cameraXBoundary = Camera.main.transform.position.x - (Camera.main.aspect * Camera.main.orthographicSize);
        if (gameObject.transform.position.x < cameraXBoundary)
        {
            Destroy(gameObject, 1);
        }
    }
}
