using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script that used to implement a simple infinite scrolling background. </para>  	
/// </summary>
public class BackgroundShifter : MonoBehaviour
{
    /// <summary>
    /// The position of left half of the background image. This is a duplicate of the right half.
    /// </summary>
    public Transform currBackground1;
    /// <summary>
    ///  The position of right half of the background image. This is a duplicate of the left half.
    /// </summary>
    public Transform currBackground2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /// <summary>
        /// Simply checks whether the current position of the cammera has reached the center of the right image.
        /// If the center has been traversed, then shift the two images over the distance the camera has traveled.
        /// </summary>
        var cameraXPos = Camera.main.transform.position.x;
        if(cameraXPos >= currBackground2.position.x)
        {
            currBackground1.localPosition = currBackground1.transform.position + new Vector3(382, 0, 0);
            currBackground2.localPosition = currBackground1.transform.position + new Vector3(382, 0, 0);
        }
    }
}
