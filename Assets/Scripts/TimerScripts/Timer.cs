using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para> Script that is called by the TimerText game object, to keep track of player's survival time. </para>  	
/// </summary>

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Reference to the object which displays the timer to the user.
    /// </summary>
    GameObject timerText;
    /// <summary>
    /// The current time in seconds.
    /// </summary>
    public float currentTime;

    /// <summary>
    /// Last rounded time
    /// </summary>
    public float roundedTime;

    /// <summary>
    /// Whether or not the timer has been stopped.
    /// </summary>
    private bool stopped;

    /// <summary>
    /// Player game object
    /// </summary>
    public GameObject Player;

    // Start is called before the first frame update
    public void Start()
    {
        /// <summary>
        /// <para> Function to initialize the timer. Finds the timerText object and initializes currentTime to 0. </para>  	
        /// </summary>

        timerText = GameObject.Find("TimerText");
        Player = GameObject.Find("Player");

        currentTime = 0.0f;
        roundedTime = 0.0f;
    }

    public void Update()
    {
        /// <summary>
        /// <para> Function that that updates the timer. Increments the currentTime so that it reflects 
        /// how long the player has survived, and displays it through the timerText object. </para>  	
        /// </summary>
        /// 
        if (!stopped)
            currentTime += Time.deltaTime;

        if (roundedTime != Mathf.Round(currentTime))
        {
            Player.GetComponent<Speeding>().DecreaseDuration();
            roundedTime = Mathf.Round(currentTime);
        }
        timerText.GetComponent<Text>().text = "Current time: " + roundedTime;
    }

    public void StopTimer()
    {
        /// <summary>
        /// <para> Function that stops the timer. </para>  	
        /// </summary>
        /// 
        stopped = true;
    }
}
