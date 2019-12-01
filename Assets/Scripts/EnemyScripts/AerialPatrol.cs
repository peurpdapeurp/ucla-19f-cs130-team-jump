using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the patrol movement of aerial enemies
/// </summary>
public class AerialPatrol : Patrol
{
    private bool movingDown = true;

    private float maxDistance = 5.0f;
    private float wallDetectionRayLengthScalar = 0.2f;
    private float wallDetectionYOffset;
    private Vector3 lastTurnPosition;

    /// <summary>
    /// This function is called before the first frame update. It stores the initial position of the enemy
    /// </summary>
    void Start()
    {
        base.Start();
        wallDetectionYOffset = h / 2 + (3 * h / 8);
    }

    /// <summary>
    /// This function is called once per frame. It calculates the translation function and when to turn the other direction
    /// </summary>
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        Vector3 wallDetectionRayStart = transform.position + new Vector3(0, -1 * wallDetectionYOffset * Mathf.Sign(transform.localScale[1]), 0);
        Vector2 wallDetectionRay = Vector2.down * wallDetectionRayLengthScalar * Mathf.Sign(transform.localScale[1]);
        Debug.DrawRay(wallDetectionRayStart, wallDetectionRay);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetectionRayStart, wallDetectionRay, wallDetectionRayLengthScalar, whatIsGround);
        Debug.Log(wallInfo.collider);
        if (wallInfo.collider || transform.position[1] - lastTurnPosition[1] > maxDistance)
        {
            if (movingDown)
            {
                movingDown = false;
            }
            else
            {
                movingDown = true;
            }
            Vector3 newScale = transform.localScale;
            newScale.y *= -1;
            transform.localScale = newScale;
            speed = -speed;
            lastTurnPosition = transform.position;
        }
    }
}
