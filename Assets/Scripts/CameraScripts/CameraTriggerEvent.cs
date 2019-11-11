﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerEvent : MonoBehaviour
{

    public GameObject loseText;
    public CameraMover cameraMover;

    // Start is called before the first frame update
    void Start()
    {
        loseText.SetActive(false);
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
            loseText.SetActive(true);
            Destroy(collision.gameObject);
            cameraMover.movementSpeed = 0;
        }
    }
}
