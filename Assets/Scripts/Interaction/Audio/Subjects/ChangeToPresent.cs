using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePresentPastMusic : Subject
{
    #region Initialisation & Singleton

    public static ChangePresentPastMusic instance {get; private set;}

    void Awake()
    {
        if (instance != null){
            Debug.LogError("More then 1 ChangeToPresent in Scene.");
        }
        instance = this;
    }
    #endregion

    public bool isPresent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isPresent){
            FireEvent();
            isPresent = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isPresent){
            FireEvent();
            isPresent = false;
        }
    }
}
