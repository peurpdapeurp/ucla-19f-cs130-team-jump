using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <para> General enemy patrol script from which wall walking, jumping, normal, and aerial enemies inherit. </para>  	
/// </summary>
public class Patrol : MonoBehaviour
{
    /// <summary>
    /// Speed at which enemy patrols.
    /// </summary>
    public float speed = 5f;
    /// <summary>
    /// What the enemy considers to be the ground, which affects when the enemy will turn around.
    /// </summary>
    public LayerMask whatIsGround;
    /// <summary>
    /// X offset for the ray used to detect the ground.
    /// </summary>
    private float groundDetectionXOffset;
    /// <summary>
    /// Y offset for the ray used to detect the ground.
    /// </summary>
    private float groundDetectionYOffset;
    /// <summary>
    /// Scalar which affects the length of the ray used to detect the ground.
    /// </summary>
    private float groundDetectionRayLengthScalar;
    /// <summary>
    /// Scalar which affects the length of the ray used to detect if the enemy has walked into something.
    /// </summary>
    private float sideDetectionRayLengthScalar;
    /// <summary>
    /// Normal vector, which is always perpendicular to the surface the enemy is walking on.
    /// </summary>
    private Vector2 normal;
    /// <summary>
    /// Flag to set if one wants the enemy to check for collisions with other enemies.
    /// </summary>
    protected bool checkCollisions = true;

    /// <summary>
    /// Whether or not the enemy is currently moving to the right.
    /// </summary>
    protected bool movingRight = true;

    /// <summary>
    /// The renderer from which the enemy's size can be determined.
    /// </summary>
    protected Renderer renderer;
    /// <summary>
    /// The top right and bottom left corners of the enemy's rendering, used to determine its size.
    /// </summary>
    protected Vector3 topRightCorner, bottomLeftCorner;
    /// <summary>
    /// The width and height of the enemy, determined from the renderer.
    /// </summary>
    protected float w, h;

    public void Start()
    {
        /// <summary>
        /// Function to initialize the enemy. Finds the enemy's width and height based on its renderer. Sets the ground detect ray's
        /// X and Y offsets based on the enemy's width and height.
        /// </summary>
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
        /// <summary>
        /// Just calls the Move function, and sets the normal vector.
        /// </summary>
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
        /// <summary>
        /// Moves the object which the script is attached to. If the object detects that it has reached the end of the
        /// platform or wall it is currently patrolling, it will turn around and move in the opposite direction.
        /// </summary>
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
        /// <summary>
        /// The OnTriggerEnter2D function is triggered whenever the player enters the BoxCollider2D trigger attached to the object.
        /// Applies damage to the player.
        /// </summary>
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            Rigidbody2D playerRigidbody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
            Vector2 knockbackVector = Vector2.up * 10.0f;
            playerRigidbody2D.velocity = (knockbackVector);
            playerHealth.applyDamage();
        }
        else if (checkCollisions && collision.CompareTag("Enemy"))
        {
            movingRight = !movingRight;
            groundDetectionXOffset = -groundDetectionXOffset;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            speed = -speed;
        }
    }

    /**
     * \brief Gets the camera bounds to check if the enemy has left the visible camera range, and delete it if it is.
     */
    protected void TryDeallocate()
    {
        /// <summary>
        /// The TryDeallocate function is to be called once per frame in the Update function.
        /// The purpose is to check check whether the enemy GameObject has left the left, upper, or lower bound of camera, and if it has, deallocate the enemy.
        /// </summary>
        var cameraXLowerBound = Camera.main.transform.position.x - (Camera.main.aspect * Camera.main.orthographicSize);
        var cameraYLowerBound = Camera.main.transform.position.y - Camera.main.orthographicSize;
        var cameraYUpperBound = Camera.main.transform.position.y + Camera.main.orthographicSize;
        if((gameObject.transform.position.x < cameraXLowerBound) || (gameObject.transform.position.y < cameraYLowerBound) || (gameObject.transform.position.y > cameraYUpperBound))
        {
            Destroy(gameObject, 1);
        }
    }
}
