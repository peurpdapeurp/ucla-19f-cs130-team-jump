using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int invulnerabilityPeriodSeconds = 1;
    private int currentHealth;
    private GameObject currentHealthText;
    public GameObject loseText;
    public GameObject cameraObject;
    public GameObject player;
    public GameObject timerText;
    private float lastDamageTime = 0f;

    public void Start()
    {
        currentHealth = maxHealth;
        currentHealthText = GameObject.Find("CurrentHealth");
        currentHealthText.GetComponent<Text>().text = "Health: " + currentHealth;
        cameraObject = GameObject.Find("MainCamera");
        loseText = GameObject.Find("LossText");
        loseText.GetComponent<Text>().enabled = false;
        timerText = GameObject.Find("TimerText");
        player = GameObject.Find("Player");
    }

    public void applyDamage()
    {
        if (Time.time - lastDamageTime < invulnerabilityPeriodSeconds)
        {
            return;
        }

        currentHealth--;
        player.GetComponent<PlayerParticle>().Flash();

        if (currentHealth <= 0)
        {
            cameraObject.GetComponent<CameraMover>().movementSpeed = 0;
            currentHealthText.GetComponent<Text>().enabled = false;
            timerText.GetComponent<Timer>().StopTimer();
            timerText.GetComponent<Text>().enabled = false;
            loseText.GetComponent<Text>().enabled = true;
            loseText.GetComponent<Text>().text =
                "WASTED" + "\n" +
                "You lasted " + Mathf.Round(timerText.GetComponent<Timer>().currentTime) + " seconds.";
        }
        else
            currentHealthText.GetComponent<Text>().text = "Health: " + currentHealth;

        lastDamageTime = Time.time;
    }
    public int getCurrentHealth()
    {
        return currentHealth;
    }
    
    public void cameraKill()
    {
        currentHealth = 0;
        invulnerabilityPeriodSeconds = 0;
        applyDamage();
    }
}
