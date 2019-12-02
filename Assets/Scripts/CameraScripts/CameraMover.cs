using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script which moves the camera. </para>  	
/// </summary>
public class CameraMover : MonoBehaviour
{

    /// <summary>
    /// The object to be moved.
    /// </summary>
    public Transform target;
    /// <summary>
    /// The speed to move the object.
    /// </summary>
    public float movementSpeed = 0.1f;

    public void FixedUpdate()
    {
        /// <summary>
        /// Moves the object to the right at movementSpeed.
        /// </summary>
        target.position = target.position + new Vector3(movementSpeed, 0);
    }

}
