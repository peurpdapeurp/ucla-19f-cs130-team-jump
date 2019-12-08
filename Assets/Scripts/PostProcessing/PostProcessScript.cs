using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para> Script that is called by the Camera object after it renders a frame, so that performs PostProcessing before displaying the frame </para>  	
/// </summary>


public class PostProcessScript : MonoBehaviour
{

    private float radius;

    /// <summary>
    ///  Reference to the Player Object, so can later extract its position and health points
    /// </summary>
    public GameObject player;
    /// <summary>
    /// Reference to the Camera Object, so can extract its projection matrix and situate the player onto the screen
    /// </summary>
    public Camera cam;
    /// <summary>
    /// The material on which to display the resulting image, a planar screen spanning the Camera's view.
    /// mat.shader is the shader program bound to the material, shader program that processes pictures displayed onto the material
    /// </summary>
    public Material mat;

    public const float kFullHealthRadius = 1.0f;
    public const float kTwoHealthRadius = 0.3f;

    public float GetRadius()
    {
        return radius;
    }

    public void Start()
    {
        /// <summary>
        /// Function to start post processing. Finds the player and camera.
        /// </summary>
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        ///<summary> Function called by the camera every time it finished rendered a frame. Transforms applies the PostProcessing shader to src and return the final picture to dest. </summary>
        /// <param name="src"> The color buffer holding the frame after the camera initially renders the scene </param>
        /// <param name="dest"> The buffer in which the post processed image will be stored, what is ultimately displayed onto the screen  </param>

        /// <param name="player.PlayerHealth()"> Script to call for Health Points of the player, so can apply graphical effects depending on the player's health </param>
        /// <param name="player.transform.position"> Where the player is in the world space so can do greyscale relative to player position </param> 


        //src is color buffer
        if (player != null && cam != null)
        {
            mat.SetVector("_PlayerPos", cam.WorldToViewportPoint(player.transform.position));
            mat.SetFloat("_BlurSize", 0.02f);
            mat.SetInt("_Dead", 0);

            int health = player.GetComponent<PlayerHealth>().getCurrentHealth();

            switch (health)
            {
                case 3:
                    radius = kFullHealthRadius;
                    break;
                case 2:
                    radius = kTwoHealthRadius;
                    break;
                case 1: //If player about to die 
                    float time = 5.0f * Time.realtimeSinceStartup;
                    time = time % Mathf.PI / 15.0f;
                    radius = Mathf.Sin(time);
                    break;
                default:
                    radius = 0.0f;
                    mat.SetInt("_Dead", 1);
                    mat.SetVector("_PlayerPos", new Vector2(0.5f, 0.5f));
                    mat.SetFloat("_BlurSize", 0.15f);
                    Destroy(GameObject.Find("Player"));
                    break;

            }
            mat.SetFloat("_Radius", radius);
        }

        var temporaryTexture = RenderTexture.GetTemporary(src.width, src.height);

        mat.SetInt("_Blur", 0);
        Graphics.Blit(src, temporaryTexture, mat);
        mat.SetInt("_Blur", 1);
        Graphics.Blit(temporaryTexture, dest, mat);
        RenderTexture.ReleaseTemporary(temporaryTexture);
    }
}
