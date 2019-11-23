using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerEvent : MonoBehaviour
{
    public GameObject loseText;
    public GameObject cameraObject;

    /**
     * \brief The Start function automatically assigns the cameraObject and loseText objects 
     * to the corresponding game objects in the scene, and makes sure the losing text attached 
     * to this script is invisible upon intialization.
     */
    public void Start()
    {
        cameraObject = GameObject.Find("MainCamera");
        loseText = GameObject.Find("LoseText");
        loseText.SetActive(false);
    }

    /**
     * \brief The OnTriggerEnter2D function is triggered whenever an object enters the BoxCollider2D trigger attached to the object.
     * It currently sets the losing text to be visible and stops the camera object associated withe script from moving.
     */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy: OnTriggerEnter2D");
            loseText.SetActive(true);
            Destroy(collision.gameObject);
            cameraObject.GetComponent<CameraMover>().movementSpeed = 0;
        }
    }

}
