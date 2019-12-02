using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script that creates particle explosions around the player and makes the player sprite flash white. It is called by the PlayerHealth Script when the player takes damage. </para>  	
/// </summary>

public class PlayerParticle : MonoBehaviour
{
    private Material flashMat;
    private Material defaultMat;   
    SpriteRenderer sr;
    private UnityEngine.Object explosionRef;

    // Particle System Setup
   public  void Start()
    {
        /// <summary>
        /// <para> Initializes parameters to their default value </para>  	
        /// </summary>
        /// <param name="flashMat"> The Sprite material that is displayed when the player takes damage </param>
        /// <param name="defaultMat"> The Sprite material that is displayed when the player did not take damage </param>
        /// <param name="explosionRef"> An external reference to the particle explosion that is instantiated at the position of the player when he takes damage </param> 

        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        flashMat = Resources.Load("PostProcessing/Flash", typeof(Material)) as Material;
        explosionRef = Resources.Load("PostProcessing/PlayerExplosion");
    }

    public void Flash()
    {
        ///<summary> Makes the player's sprite material be the flashMaterial for one second, and instantiates a particle explosion </summary>
        /// <param name="flashMat"> The Sprite material that is displayed when the player takes damage </param>
        ///  <param name="explosionRef"> An external reference to the particle explosion that is instantiated at the position of the player when he takes damage </param> 
        
        sr.material = flashMat;
        Invoke("ResetMaterial", 1.0f);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
    }

    private void ResetMaterial()
    {
        ///<summary> Makes the player's sprite material to be the default one </summary>
        sr.material = defaultMat;
    }
}
