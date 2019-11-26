using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Patrol : MonoBehaviour
{
    public GameObject loseText;
    public GameObject cameraObject;
    public float speed = 5f;
    public LayerMask whatIsGround;
    private float groundDetectionXOffset;
    private float groundDetectionYOffset;
    private float groundDetectionRayLengthScalar;
    private Vector2 normal;

    protected bool movingRight = true;

    protected Renderer renderer;
    protected Vector3 topRightCorner, bottomLeftCorner;
    protected float w, h;

    public void Start()
    {
        cameraObject = GameObject.Find("MainCamera");
        loseText = GameObject.Find("LossText");
        loseText.GetComponent<Text>().enabled = false;
        renderer = gameObject.GetComponent<Renderer>();
        topRightCorner = renderer.bounds.max;
        bottomLeftCorner = renderer.bounds.min;
        w = topRightCorner.x - bottomLeftCorner.x;
        h = topRightCorner.y - bottomLeftCorner.y;
        groundDetectionXOffset = w / 2 + 0.25f * w;
        groundDetectionYOffset = h / 2;
        groundDetectionRayLengthScalar = 0.5f;
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
    }

    /**
     * \brief Moves the object which the script is attached to. If the object detects that it has reached the end of the
     *  platform or wall it is currently patrolling, it will turn around and move in the opposite direction.
     */
    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        Vector3 groundRayStart = new Vector3(transform.position[0] + groundDetectionXOffset * Mathf.Sign(normal[1]),
                                             transform.position[1] - groundDetectionYOffset * Mathf.Sign(normal[1]),
                                             transform.position[2]);
        Vector2 groundRay = new Vector2(normal[0], -1 * normal[1] * groundDetectionRayLengthScalar);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundRayStart, groundRay, groundDetectionRayLengthScalar, whatIsGround);
        Debug.DrawRay(groundRayStart, groundRay, Color.red);
        if (groundInfo.collider == false)
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
            Debug.Log("Enemy: OnTriggerEnter2D");
            loseText.GetComponent<Text>().enabled = true;
            Destroy(collision.gameObject);
            cameraObject.GetComponent<CameraMover>().movementSpeed = 0;
        }
    }
}
