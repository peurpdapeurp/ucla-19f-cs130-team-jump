﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the patrol movement of aerial enemies
/// </summary>
public class AerialPatrol : Patrol
{
    private bool movingDown = true;
    private float initialY;
    private float dist;
    private bool attack = false;
    public Animator animator;

    /// <summary>
    /// This function is called before the first frame update. It stores the initial position of the enemy
    /// </summary>
    void Start()
    {
        base.Start();
        initialY = transform.position.y;
    }

    /// <summary>
    /// This function is called once per frame. It calculates the translation function and when to turn the other direction
    /// </summary>
    void Update()
    {
        if (!attack)
        {
            //Note that Raycast works ONLY with the tilemap, not with enemies and player
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

    protected void TryDeallocate()
    {
        var cameraXBoundary = Camera.main.transform.position.x - (Camera.main.aspect * Camera.main.orthographicSize);
        if (gameObject.transform.position.x < cameraXBoundary)
        {
            Destroy(gameObject, 1);
        }
    }
}
