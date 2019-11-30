using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject theAM;
    public GameObject ES;
    public GameObject Camera;

    static public int counter = 0;

    private void Start()
    {
        theAM = GameObject.FindGameObjectWithTag("Audio");
        ES = GameObject.FindGameObjectWithTag("Environment");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        AudioSource myAS = theAM.GetComponent<AudioSource>();
        EnvironmentSlice ES_Scipt = ES.GetComponent<EnvironmentSlice>();
        CameraMover CM = Camera.GetComponent<CameraMover>();
        //Debug.Log("Powerup picked up!");
        if (counter == 0)
        {
            myAS.pitch = 1.2f;
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.High);
            CM.movementSpeed = 0.13f;
            counter++;
        }
        else if (counter == 1)
        {
            myAS.pitch = 0.8f;
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.Medium);
            CM.movementSpeed = 0.1f;
            counter++;
        }
        else if (counter == 2)
        {
            myAS.pitch = 1f;
            ES_Scipt.ChooseNextSlice(EnvironmentSlice.MusicLevel.Low);
            CM.movementSpeed = 0.115f;
            counter = 0;
        }
       
        //theAS.pitch = 2;
        //Debug.Log(counter);
        Destroy(gameObject);
    }
}
