using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private GameObject currentHealthText;
    public GameObject loseText;
    public GameObject cameraObject;
    public GameObject player;

    public void Start()
    {
        currentHealth = maxHealth;
        currentHealthText = GameObject.Find("CurrentHealth");
        currentHealthText.GetComponent<Text>().text = "Current health: " + currentHealth;
        cameraObject = GameObject.Find("MainCamera");
        loseText = GameObject.Find("LossText");
        loseText.GetComponent<Text>().enabled = false;
        player = GameObject.Find("Player");
    }

    public void applyDamage()
    {
        currentHealth--;
        currentHealthText.GetComponent<Text>().text = "Current health: " + currentHealth;
        if (currentHealth == 0)
        {
            cameraObject.GetComponent<CameraMover>().movementSpeed = 0;
            loseText.GetComponent<Text>().enabled = true;
            Destroy(player);
        }
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
}
