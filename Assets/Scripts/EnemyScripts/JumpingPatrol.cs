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

    /**
     * \brief The Awake function starts the jumping loop through the JumpRoutine coroutine.
     */
    public void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(JumpRoutine());
    }

    /**
     * \brief The FixedUpdate function periodically checks if the object is in contact with any surface, and sets m_Grounded to true
     * if it is.
     */
    public void FixedUpdate()
    {
        m_Grounded = false;

        RaycastHit2D groundInfoBottom = Physics2D.Raycast(transform.position, new Vector2(0, -2), 2, m_WhatIsGround);
        RaycastHit2D groundInfoLeft = Physics2D.Raycast(transform.position, new Vector2(-2, 0), 2, m_WhatIsGround);
        RaycastHit2D groundInfoRight = Physics2D.Raycast(transform.position, new Vector2(2, 0), 2, m_WhatIsGround);
        RaycastHit2D groundInfoTop = Physics2D.Raycast(transform.position, new Vector2(0, 2), 2, m_WhatIsGround);
        m_Grounded = groundInfoBottom.collider || groundInfoLeft.collider || groundInfoRight.collider || groundInfoTop.collider;
    }

    /**
     * \brief A coroutine which periodically makes the object jump if it is grounded. The object will attempt to jump at an interval from 
     * 0 to m_MaxJumpInterval seconds. The object will randomly jump either to the left, straight up, or to the right. The object
     * will not jump if it is not in contact with a surface (i.e. m_Grounded is false).
     */
    IEnumerator JumpRoutine()
    {
        while (true)
        {
            Debug.Log("Entered jump routine loop");
            bool jump = true;
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

