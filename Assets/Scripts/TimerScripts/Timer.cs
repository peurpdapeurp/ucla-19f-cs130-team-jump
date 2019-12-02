using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    GameObject timerText;
    public float currentTime;
    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.FindGameObjectWithTag("TimerText");
        currentTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
            currentTime += Time.deltaTime;
        timerText.GetComponent<Text>().text = "Current time: " + Mathf.Round(currentTime);
    }

    public void StopTimer()
    {
        stopped = true;
    }
}
