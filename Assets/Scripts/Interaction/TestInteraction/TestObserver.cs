using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObserver : Observer
{
    protected override void OnEventFired()
    {
        Debug.Log("Test event fired.");
    }
}