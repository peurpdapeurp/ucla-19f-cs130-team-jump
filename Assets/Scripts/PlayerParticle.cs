using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    private Material flashMat;
    private Material defaultMat;   
    SpriteRenderer sr;
    private UnityEngine.Object explosionRef;
    private int health;
    private bool invulnerable;

    // Particle System Setup
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        flashMat = Resources.Load("Flash", typeof(Material)) as Material;
        explosionRef = Resources.Load("PlayerExplosion");

        health = 3;
        invulnerable = false;
}

    public void Flash()
    {
        if (!invulnerable)
        {
            sr.material = flashMat;
            Invoke("ResetMaterial", 0.5f);
            GameObject explosion = (GameObject)Instantiate(explosionRef);
            explosion.transform.position = transform.position;

            health--;
            invulnerable = true;
            Invoke("Invulnerability", 1.0f);
        }  
    }

    private void Invulnerability()
    {
        invulnerable = false;
    }

    public void Kill()
    {
        health = -1;
    }

    public int Health()
    {
        return health;
    }

    private void ResetMaterial()
    {
        sr.material = defaultMat;
    }
}
