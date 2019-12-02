using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundShifter : MonoBehaviour
{
    public Transform currBackground1;
    public Transform currBackground2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cameraXPos = Camera.main.transform.position.x;
        if(cameraXPos >= currBackground2.position.x)
        {
            currBackground1.localPosition = currBackground1.transform.position + new Vector3(382, 0, 0);
            currBackground2.localPosition = currBackground1.transform.position + new Vector3(382, 0, 0);
        }
    }
}
