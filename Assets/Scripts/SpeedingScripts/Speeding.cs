using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeding : MonoBehaviour
{
    public float speedBonus;
    public int speedBonusDuration;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speedBonusDuration > 0)
        {
            speedBonusDuration--;
        }
        if (speedBonusDuration == 0)
        {
            RemoveSpeedUpgrade();
        }
    }

    public void GetSpeedUpgrade(float bonus, int duration)
    {
        speedBonus += bonus;
        speedBonusDuration += duration;
        GetComponent<PlayerMovement>().runSpeed += speedBonus;
    }

    public void RemoveSpeedUpgrade()
    {
        GetComponent<PlayerMovement>().runSpeed -= speedBonus;
        speedBonus = 0.0f;
    }
}
