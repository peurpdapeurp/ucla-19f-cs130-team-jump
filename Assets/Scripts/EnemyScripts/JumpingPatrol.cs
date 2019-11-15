using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpingPatrol : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;           // Amount of force added when the enemy jumps.
    [SerializeField] private LayerMask m_WhatIsGround;           // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;            // A position marking where to check if the player is grounded.
    [SerializeField] private float m_JumpInterval;

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }
    }

    IEnumerator JumpRoutine()
    {
        while (true)
        {
            Debug.Log("Entered jump routine loop");
            //bool jump = (Random.value > 0.5f);
            bool jump = true;
            // If the player should jump...
            if (m_Grounded && jump)
            {
                Debug.Log("Trying to jump");

                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector3(Random.Range(-1, 1) * m_JumpForce * 0.5f, m_JumpForce, 0));
            }
            yield return new WaitForSeconds(m_JumpInterval);
        }
    }
}

