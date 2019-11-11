using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWalkingPatrol : MonoBehaviour
{
    float gravity = 10; // gravity acceleration

    public float speed;
    public Transform groundDetection;
    public float forceScalar;

    private bool movingRight = true;
    private Vector2 normal;

    private void FixedUpdate()
    {
        normal = transform.up;
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody2D>().AddForce(forceScalar * (-gravity * GetComponent<Rigidbody2D>().mass * normal));
        Debug.DrawRay(transform.position, normal, Color.magenta);
    }

    void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, -normal, 2, layerMask);
        Debug.DrawRay(groundDetection.position, -normal, Color.red);
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
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            speed = -speed;
        }
        Debug.Log("Ground info's collider: " + groundInfo.collider);
    }
}

//public class wallwalkingpatrol : monobehaviour
//{
//    float movespeed = 6; // move speed
//    float turnspeed = 90; // turning speed (degrees/second)
//    float lerpspeed = 10; // smoothing speed
//    float gravity = 10; // gravity acceleration
//    bool isgrounded;
//    float deltaground = 0.2f; // character is grounded up to this distance
//    float jumpspeed = 10; // vertical jump initial speed
//    float jumprange = 10; // range to detect target wall

//    private vector2 surfacenormal; // current surface normal
//    private vector2 mynormal; // character normal
//    private float distground; // distance from character position to ground
//    private bool jumping = false; // flag &quot;i'm jumping to wall&quot;
//    private float vertspeed = 0; // vertical jump current speed 

//    void start()
//    {
//        mynormal = transform.up; // normal starts as character up direction 
//        getcomponent<rigidbody2d>().freezerotation = true; // disable physics rotation
//                                                           // distance from transform.position to ground
//        distground = getcomponent<boxcollider2d>().bounds.extents.y - getcomponent<boxcollider2d>().offset.y;
//    }

//    void fixedupdate()
//    {
//        // apply constant weight force according to character normal:
//        getcomponent<rigidbody2d>().addforce(-gravity * getcomponent<rigidbody2d>().mass * mynormal);
//    }

//    void update()
//    {
//        // jump code - jump to wall or simple jump
//        if (jumping) return;  // abort update while jumping to a wall
//        ray ray;
//        raycasthit hit;
//        if (input.getbuttondown("jump"))
//        { // jump pressed:
//            ray = new ray(transform.position, transform.forward);
//            if (physics.raycast(ray, hit, jumprange))
//            { // wall ahead?
//                jumptowall(hit.point, hit.normal); // yes: jump to the wall
//            }
//            else if (isgrounded)
//            { // no: if grounded, jump up
//                getcomponent<rigidbody2d>().velocity += jumpspeed * mynormal;
//            }
//        }

//        // movement code - turn left/right with horizontal axis:
//        transform.rotate(0, input.getaxis("horizontal") * turnspeed * time.deltatime, 0);
//        // update surface normal and isgrounded:
//        ray = new ray(transform.position, -mynormal); // cast ray downwards
//        if (physics.raycast(ray, hit))
//        { // use it to update mynormal and isgrounded
//            isgrounded = hit.distance <= distground + deltaground;
//            surfacenormal = hit.normal;
//        }
//        else
//        {
//            isgrounded = false;
//            // assume usual ground normal to avoid "falling forever"
//            surfacenormal = vector3.up;
//        }
//        mynormal = vector3.lerp(mynormal, surfacenormal, lerpspeed * time.deltatime);
//        // find forward direction with new mynormal:
//        var myforward = vector3.cross(transform.right, mynormal);
//        // align character to the new mynormal while keeping the forward direction:
//        var targetrot = quaternion.lookrotation(myforward, mynormal);
//        transform.rotation = quaternion.lerp(transform.rotation, targetrot, lerpspeed * time.deltatime);
//        // move the character forth/back with vertical axis:
//        transform.translate(0, 0, input.getaxis("vertical") * movespeed * time.deltatime);
//    }

//    void jumptowall(vector3 point, vector3 normal)
//    {
//        // jump to wall 
//        jumping = true; // signal it's jumping to wall
//        getcomponent<rigidbody2d>().iskinematic = true; // disable physics while jumping
//        var orgpos = transform.position;
//        var orgrot = transform.rotation;
//        var dstpos = point + normal * (distground + 0.5f); // will jump to 0.5 above wall
//        var myforward = vector3.cross(transform.right, normal);
//        var dstrot = quaternion.lookrotation(myforward, normal);
//        for (float t = 0.0f; t < 1.0;)
//        {
//            t += time.deltatime;
//            transform.position = vector3.lerp(orgpos, dstpos, t);
//            transform.rotation = quaternion.slerp(orgrot, dstrot, t);
//            yield; // return here next frame
//        }
//        mynormal = normal; // update mynormal
//        getcomponent<rigidbody2d>().iskinematic = false; // enable physics
//        jumping = false; // jumping to wall finished
//    }
//}