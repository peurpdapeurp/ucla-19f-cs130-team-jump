using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpingPatrol : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;           // Amount of force added when the enemy jumps.
    [SerializeField] private LayerMask m_WhatIsGround;           // A mask determining what is ground to the character
    [SerializeField] private float m_MaxJumpInterval = 5;

    private bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        RaycastHit2D groundInfoBottom = Physics2D.Raycast(transform.position, new Vector2(0, -2), 2, m_WhatIsGround);
        RaycastHit2D groundInfoLeft = Physics2D.Raycast(transform.position, new Vector2(-2, 0), 2, m_WhatIsGround);
        RaycastHit2D groundInfoRight = Physics2D.Raycast(transform.position, new Vector2(2, 0), 2, m_WhatIsGround);
        RaycastHit2D groundInfoTop = Physics2D.Raycast(transform.position, new Vector2(0, 2), 2, m_WhatIsGround);
        m_Grounded = groundInfoBottom.collider || groundInfoLeft.collider || groundInfoRight.collider || groundInfoTop.collider;

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
            yield return new WaitForSeconds(Random.Range(0, m_MaxJumpInterval));
        }
    }
}

