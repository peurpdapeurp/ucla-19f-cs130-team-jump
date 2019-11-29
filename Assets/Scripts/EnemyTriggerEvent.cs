using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerEvent : MonoBehaviour
{
    public GameObject loseText;
    public GameObject cameraObject;

    // Start is called before the first frame update
    void Start()
    {
        loseText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy: OnTriggerEnter2D");
                     
            if (collision.gameObject.GetComponent<PlayerParticle>().Health() < 0)
            {
                loseText.SetActive(true);
                cameraObject.GetComponent<CameraMover>().movementSpeed = 0;
            }
            else
                collision.gameObject.GetComponent<PlayerParticle>().Flash();
        }
    }

}
