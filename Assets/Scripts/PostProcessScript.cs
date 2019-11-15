using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessScript : MonoBehaviour
{

    private GameObject player;
    Camera cam;
    public Material mat;

    
    void Start()
    {
        player = GameObject.Find("Player");
        cam = GetComponent<Camera>();
    }

    /// <summary>
    /// You would not believe what amazing things this method does.   	
    /// </summary>
    /// <param name="src">A explanation of this really important number </param>
    /// <param name="dest">Another explanation  </param>
    void OnRenderImage(RenderTexture src, RenderTexture dest)
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
