using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance {get; private set;}

    void Awake()
    {
        if (instance != null){
            Debug.LogError("More then 1 Audio Manager in Scene.");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition){
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }
}
