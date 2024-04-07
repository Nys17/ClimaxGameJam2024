using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    #region Variables

    [field: Header("Music")]
    public EventReference menuMusic;
    public EventReference pastMusic;
    public EventReference presentMusic;
    public EventReference endingMusic;
    public EventReference powerUnlockedMusic;

    [field: Header ("Dialogue Effects")]
    public EventReference dialogueSound;
    public EventReference dialogueEndSound;

    [field: Header ("Sound Effects")]
    public EventReference jumpSound;
    public EventReference footstepSound;

    #endregion

    #region Initialisation & Singleton
    public static FMODEvents instance {get; private set;}

    void Awake()
    {
        if (instance != null){
            Debug.Log("More then 1 FMOD Events in Scene.");
            if (instance != this){
                Destroy(gameObject);
                Debug.Log("wwwwwwww");
            }
        }
        else{
            instance = this;
        }
    }
    #endregion
}
