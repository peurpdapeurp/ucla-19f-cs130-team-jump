using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para> Script that detects when player leaves the camera area. </para>  	
/// </summary>
public class CameraTriggerEvent : MonoBehaviour
{
    /// <summary>
    /// A reference to the text object which displays when the user loses.
    /// </summary>
    public GameObject loseText;
    /// <summary>
    /// A reference to the camera movement script.
    /// </summary>
    public CameraMover cameraMover;

    // Start is called before the first frame update
    void Start()
    {
        /// <summary>
        /// Initializes this script. Sets the loseText to be invisible.
        /// </summary>
        loseText.GetComponent<Text>().enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /// <summary>
        /// Detects an object leaves the camera area. 
        /// <param name="collision"> The collider object associated with the object that left the camera area. If it
        /// is the player, this script will destroy the player and stop the camera from moving, as well as display the losing text. </param>
        /// </summary>
        if (collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2D");
            loseText.GetComponent<Text>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerHealth>().cameraKill();
            cameraMover.movementSpeed = 0;          
        }
    }
}
