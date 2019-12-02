using System.Collections;
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
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        dist = initialY - transform.position.y;
        if (dist > 3)
        {
            if (movingDown)
            {
                //transform.eulerAngles = new Vector3(-180, 0, 0);
                speed *= -1;
                movingDown = false;
            }
            
        }
        else if (dist < -3)
        {
            if (!movingDown)
            {
                //transform.eulerAngles = new Vector3(0, 0, 0);
                speed *= -1;
                movingDown = true;
            }
        }
    }
}
