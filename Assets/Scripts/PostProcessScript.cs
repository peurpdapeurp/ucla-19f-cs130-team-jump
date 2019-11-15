using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script that is called by the Camera object after it renders a frame, so that performs PostProcessing before displaying the frame </para>  	
/// </summary>
/// <param name="mat"> The material on which to display the resulting image, a planar screen spanning the Camera's view </param>
/// <param name="mat.shader"> The Shader program bound to the material, shader program that processes pictures displayed onto the material </param>
/// <param name="src"> The color buffer holding the frame after the camera initially renders the scene </param>
/// <param name="dest"> The buffer in which the post processed image will be stored, what is ultimately displayed onto the screen  </param>
/// <param name="player.health"> Health Points of the player, so can apply graphical effects depending on the player's health </param>
/// <param name="player.transform.position"> Where the player is in the world space </param> 
/// <param name="camera"> Reference to the Camera Object, so can extract its projection matrix and situate the player onto the screen </param> 

public class PostProcessScript : MonoBehaviour
{

    public GameObject player;
    public Camera cam;
    public Material mat;

    
    public void Start()
    {
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }
    
    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        //src is color buffer
        if (player != null && cam != null)
        {
            mat.SetVector("_PlayerPos", cam.WorldToViewportPoint(player.transform.position));

            int health = 0;
            float radius;

            switch (health)
            {
                case 3: //No Damage
                    radius = 1.0f;
                    break;
                case 2:
                    radius = 0.4f;
                    break;
                case 1:
                    radius = 0.2f;
                    break;
                case 0: //If player about to die 
                    float time = 5.0f * Time.realtimeSinceStartup;
                    time = time % Mathf.PI / 15.0f;
                    radius = Mathf.Sin(time);
                    break;
                default:
                    radius = 1.0f;
                    break;

            }
            mat.SetFloat("_Radius", radius);
        }
        Graphics.Blit(src, dest, mat);
    }
}
