using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraTriggerEvent : MonoBehaviour
{

    public GameObject loseText;
    public CameraMover cameraMover;

    // Start is called before the first frame update
    void Start()
    {
        loseText.GetComponent<Text>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2D");
            loseText.GetComponent<Text>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerHealth>().cameraKill();
            cameraMover.movementSpeed = 0;          
        }
    }
}
