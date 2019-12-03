﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patrol : MonoBehaviour
{
    public float speed = 5f;
    public LayerMask whatIsGround;
    private float groundDetectionXOffset;
    private float groundDetectionYOffset;
    private float groundDetectionRayLengthScalar;
    private float sideDetectionRayLengthScalar;
    private Vector2 normal;

    protected bool movingRight = true;

    protected Renderer renderer;
    protected Vector3 topRightCorner, bottomLeftCorner;
    protected float w, h;

    public void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        topRightCorner = renderer.bounds.max;
        bottomLeftCorner = renderer.bounds.min;
        w = topRightCorner.x - bottomLeftCorner.x;
        h = topRightCorner.y - bottomLeftCorner.y;
        groundDetectionXOffset = w / 2; // + 0 * w;
        groundDetectionYOffset = h / 2;
        groundDetectionRayLengthScalar = 0.5f;
        sideDetectionRayLengthScalar = 3.0f; // w;
    }

    /**
     * \brief The update function simply calls the Move function.
     */
    public void Update()
    {
        topRightCorner = renderer.bounds.max;
        bottomLeftCorner = renderer.bounds.min;
        normal = transform.up;
        Move();
        TryDeallocate();
    }

    /**
     * \brief Moves the object which the script is attached to. If the object detects that it has reached the end of the
     *  platform or wall it is currently patrolling, it will turn around and move in the opposite direction.
     */
    public void Move()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        Vector3 groundRayStart = new Vector3(transform.position[0] - (groundDetectionXOffset * Mathf.Sign(normal[1]) * 0.75f),
                                             transform.position[1] - (groundDetectionYOffset * Mathf.Sign(normal[1]) * 0.9f),
                                             transform.position[2]);
        Vector2 groundRay = new Vector2(normal[0], -1 * normal[1] * groundDetectionRayLengthScalar);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundRayStart, groundRay, groundDetectionRayLengthScalar, whatIsGround);
        Debug.DrawRay(groundRayStart, groundRay, Color.red);

        Vector3 sideRayStart = transform.position + new Vector3((w / 2 - w * 0.2f) * Mathf.Sign(transform.localScale[0]) * -1.0f, -w*1/4, 0);
        Vector2 sideRay = Vector2.left * sideDetectionRayLengthScalar * Mathf.Sign(transform.localScale[0]);
        RaycastHit2D sideInfo = Physics2D.Raycast(sideRayStart, sideRay, sideDetectionRayLengthScalar, (whatIsGround | (1 << LayerMask.NameToLayer("Enemy"))));
        Debug.DrawRay(sideRayStart, sideRay, Color.white);
        Debug.Log("Side info collider: " + sideInfo.collider);

        if (!(groundInfo.collider) || sideInfo.collider)
        {
            if (movingRight)
            {
                movingRight = false;
            }
            else
            {
                movingRight = true;
            }
            groundDetectionXOffset = -groundDetectionXOffset;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            speed = -speed;
        }
    }

    /**
     * \brief The OnTriggerEnter2D function is triggered whenever an object enters the BoxCollider2D trigger attached to the object.
     * It currently sets the losing text to be visible and stops the camera object associated withe script from moving.
     */
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            Rigidbody2D playerRigidbody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            Vector2 knockbackVector = Vector2.up * 10.0f;
            //playerRigidbody2D.AddForce(knockbackVector * 500f * playerRigidbody2D.mass);
            //Debug.Log("knockbackVector: " + knockbackVector);
            playerRigidbody2D.velocity = (knockbackVector);
            playerHealth.applyDamage();
        }
    }

    protected void TryDeallocate()
    {
        var cameraXLowerBound = Camera.main.transform.position.x - (Camera.main.aspect * Camera.main.orthographicSize);
        var cameraYLowerBound = Camera.main.transform.position.y - Camera.main.orthographicSize;
        var cameraYUpperBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        if((gameObject.transform.position.x < cameraXLowerBound) || (gameObject.transform.position.y < cameraYLowerBound) || (gameObject.transform.position.y > cameraYUpperBound))
        {
            Destroy(gameObject, 1);
        }
    }
}
