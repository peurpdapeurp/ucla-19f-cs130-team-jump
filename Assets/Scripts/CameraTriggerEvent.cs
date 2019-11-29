using System.Collections;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("OnTriggerExit2D");

            if (collision.gameObject.GetComponent<PlayerParticle>().Health() < 0)
            {
                loseText.SetActive(true);
                cameraMover.movementSpeed = 0;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerParticle>().Flash();
                collision.gameObject.GetComponent<PlayerParticle>().Kill();
            }
        }
    }
}
