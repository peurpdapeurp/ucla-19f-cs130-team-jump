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

    // Start is called before the first frame update
    void Start()
    {
        /// <summary>
        /// Sets the camera mode to orthographic and the transparent object sorting mode to orthographic
        /// </summary>
        Camera.main.orthographic = false;
        Camera.main.transparencySortMode = TransparencySortMode.Orthographic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /// <summary>
        /// Moves the object to the right at movementSpeed.
        /// </summary>
        target.position = target.position + new Vector3(movementSpeed, 0);
    }

}
