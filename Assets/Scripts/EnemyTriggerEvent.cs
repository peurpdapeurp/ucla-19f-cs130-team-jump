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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
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
