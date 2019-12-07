using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    private Material flashMat;
    private Material defaultMat;   
    SpriteRenderer sr;
    private UnityEngine.Object explosionRef;

    // Particle System Setup
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        flashMat = Resources.Load("PostProcessing/Flash", typeof(Material)) as Material;
        explosionRef = Resources.Load("PostProcessing/PlayerExplosion");
    }

    public void Flash()
    {
        sr.material = flashMat;
        Invoke("ResetMaterial", 1.0f);
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
    }

    private void ResetMaterial()
    {
        sr.material = defaultMat;
    }
}
