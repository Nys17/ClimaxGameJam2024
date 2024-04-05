using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;


public class Subject : MonoBehaviour
{
    public event Action eventHappened;

    public void FireEvent(){
        eventHappened?.Invoke();
    }
}
