using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para> General class to keep track of player health and manage conditions relating to it like display and game termination. </para>  	
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// Maximum player health the play starts with.
    /// </summary>
    public int maxHealth = 3;
    /// <summary>
    /// The duration of the invincibility frame after the player gets damaged.
    /// </summary>
    public int invulnerabilityPeriodSeconds = 1;
    /// <summary>
    /// The current remaining health the player has left.
    /// </summary>
    private int currentHealth;
    /// <summary>
    /// The current remaining health the player has left.
    /// </summary>
    private GameObject currentHealthText;
    /// <summary>
    /// The text to be displayed on the screen when the player loses.
    /// </summary>
    public GameObject loseText;
    /// <summary>
    /// The camera that handles the display of the entire screen.
    /// </summary>
    public GameObject cameraObject;
    /// <summary>
    /// A reference to the player GameObject.
    /// </summary>
    public GameObject player;
    /// <summary>
    /// The text to be displayed on the timer on the screen.
    /// </summary>
    public GameObject timerText;
    /// <summary>
    /// The timestamp of the last time the player took damage.
    /// </summary>
    private float lastDamageTime = 0f;

    public void Start()
    {
        /// <summary>
        /// Function to initialize the player's health and retrieve the camera,and textboxes to operate.
        /// </summary>
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
        /// <summary>
        /// Function to execute on the event the player takes damage.
        /// </summary>
        if (Time.time - lastDamageTime < invulnerabilityPeriodSeconds)
        {
            return;
        }

        player.GetComponent<Animator>().SetBool("IsDamaged", true);
        Invoke("setDamageOff", 0.75f);
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
        /// <summary>
        /// Function to simply return the player's current health.
        /// </summary>
        return currentHealth;
    }
    
    public void cameraKill()
    {
        /// <summary>
        /// Function to be called on the event to modify the camera screen when the player loses.
        /// </summary>
        currentHealth = 0;
        invulnerabilityPeriodSeconds = 0;
        applyDamage();
    }

    private void setDamageOff()
    {
        /// <summary>
        /// Private function to disable the player damage animation, preferably when the animation is already in motion and is to be stopped.
        /// </summary>
        player.GetComponent<Animator>().SetBool("IsDamaged", false);
    }
}
