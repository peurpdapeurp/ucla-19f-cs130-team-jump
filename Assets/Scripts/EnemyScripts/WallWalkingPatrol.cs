/** @file */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalkingPatrol : Patrol
{
    private float forceScalar = 10f;
    private Vector2 normal;

    public void Start()
    {
        base.Start();
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
    public void Update()
    {
        normal = transform.up;
        GetComponent<Rigidbody2D>().AddForce(forceScalar * (-1 * GetComponent<Rigidbody2D>().mass * normal));
        Debug.DrawRay(transform.position, normal, Color.magenta);
        base.Update();
    }
}