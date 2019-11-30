using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{

    public Transform target;
    public float movementSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        target.position = target.position + new Vector3(movementSpeed, 0);
    }

}
