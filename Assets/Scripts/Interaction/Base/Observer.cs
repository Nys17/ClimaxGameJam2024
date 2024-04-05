using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Observer : MonoBehaviour
{
#region Variables
    [SerializeField] protected Subject subjectToObserve;

#endregion

#region Boilerplate

    protected virtual void OnEventFired(){
        Debug.Log("Observer responds");
    }

    void Awake()
    {
        if (subjectToObserve != null){
            subjectToObserve.eventHappened += OnEventFired;
        }
    }

    void OnDestroy()
    {
        if (subjectToObserve != null){
            subjectToObserve.eventHappened -= OnEventFired;
        }
    }
#endregion

}
