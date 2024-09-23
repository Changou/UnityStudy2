using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepMode : MonoBehaviour
{
    public int _sleepTime = SleepTimeout.NeverSleep;

    private void Start()
    {
        Screen.sleepTimeout = _sleepTime;
    }
}
