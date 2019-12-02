using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script that manages effect of the power ups which, when hit by the player, changes the pitch of the background music </para>
/// </summary>

public class PowerUp : MonoBehaviour
{
    public GameObject theAM;
    public GameObject ES;
    public GameObject Camera;

    /// <summary>
    /// <para> Function that retrieves game objects needed to modify game elements including audio pitch, environment slice generation, and moving speed of the main camera </para>
    /// 
    /// </summary>
    private void Start()
    {

        theAM = GameObject.FindGameObjectWithTag("Audio");
        ES = GameObject.FindGameObjectWithTag("EnvironmentSliceGenerator");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// <para> Funtion that handles detection of collision between the player and power ups. If collision is detected, call Pickup() to make changes to game objects </para>
    /// </summary>
    /// <param name="collision"> A Collider2D object used to detech collsion </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Pickup();
        }
    }

    /// <summary>
    /// <para> Function that make changes to BGM's pitch, generator of environment slice, and the moving speed of the camera </para>
    /// </summary>
    void Pickup()
    {
        AudioSource myAS = theAM.GetComponent<AudioSource>();
        EnvironmentSlice ES_Scipt = ES.GetComponent<EnvironmentSlice>();
        CameraMover CM = Camera.GetComponent<CameraMover>();
        //Debug.Log("Powerup picked up!");

        myAS.pitch = Random.Range(0.7f, 1.3f);

        if (myAS.pitch < 0.9)
        {
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.Low);
            CM.movementSpeed = 0.1f;
        }
        else if (myAS.pitch > 1.1)
        {
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.High);
            CM.movementSpeed = 0.13f;
        }
        else
        {
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.Medium);
            CM.movementSpeed = 0.115f;
        }

        Destroy(gameObject);
    }
}
