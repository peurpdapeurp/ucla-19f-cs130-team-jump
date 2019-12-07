/** @file */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script used to control behavior of wall walking enemies. </para>  	
/// </summary>
public class WallWalkingPatrol : Patrol
{
    /// <summary>
    /// Force scalar which decides how hard the enemy will be pushed against the wall it is walking on.
    /// </summary>
    private float forceScalar = 10f;
    /// <summary>
    /// The normal vector, which will always be perpendicular to the wall the enemy is walking on.
    /// </summary>
    private Vector2 normal;

    public void Start()
    {
        /// <summary>
        /// <para> Function to initialize the wall walking enemy. Sets gravity and velocity to be 0. </para>  	
        /// </summary>
        base.Start();
        GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
    }
    public void Update()
    {
        /// <summary>
        /// <para> Updates the wall waling enemy. Pushes enemy against wall it's walking on, and then calls basic patrol script's update method. </para>  	
        /// </summary>
        normal = transform.up;
        GetComponent<Rigidbody2D>().AddForce(forceScalar * (-1 * GetComponent<Rigidbody2D>().mass * normal));
        Debug.DrawRay(transform.position, normal, Color.magenta);
        base.Update();
        TryDeallocate();
    }
}