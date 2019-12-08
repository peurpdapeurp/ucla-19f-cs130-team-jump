using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para> Script that increases the player speed after encounter the coin for a certain duration </para>
/// </summary>

public class Speeding : MonoBehaviour
{
    public float speedBonus;
    public int speedBonusDuration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /// <summary>
    /// <para> Funtion that removes speed upgrade when time is up </para>
    /// </summary>
    void Update()
    {
        if (speedBonusDuration == 0)
        {
            RemoveSpeedUpgrade();
        }
    }

    /// <summary>
    /// <para> Funtion that decrease the time left when called </para>
    /// </summary>
    public void DecreaseDuration()
    {
        if (speedBonusDuration > 0)
        {
            speedBonusDuration--;
        }
    }

    /// <summary>
    /// <para> Funtion that add speed when encounter a coin </para>
    /// </summary>
    /// <param name="bonus"> the bonus speed </param>
    /// <param name="duration"> the bonus duration </param>
    public void GetSpeedUpgrade(float bonus, int duration)
    {
        // Debug.Log("duration is: " + duration);
        speedBonus += bonus;
        speedBonusDuration += duration;
        // Debug.Log("speedBonusDuration is: " + speedBonusDuration);
        GetComponent<PlayerMovement>().runSpeed += bonus;
    }

    /// <summary>
    /// <para> Funtion that remove speed upgrade when time is up </para>
    /// </summary>
    public void RemoveSpeedUpgrade()
    {
        GetComponent<PlayerMovement>().runSpeed -= speedBonus;
        speedBonus = 0.0f;
    }
}
