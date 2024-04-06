using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Video;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    #region Variables
    private List<EventInstance> eventInstances;
    private List<EventInstance> musicEventInstances;
    private EventInstance musicEventInstance;

    #endregion

    #region Initialisation & Singleton
    public static AudioManager instance {get; private set;}

    void Awake()
    {
        if (instance != null){
            Debug.LogError("More then 1 Audio Manager in Scene.");
        }
        instance = this;
        eventInstances = new List<EventInstance>();
        musicEventInstances = new List<EventInstance>();
    }
    #endregion

    #region Handle Sounds
    public void InitaliseMusicEvent(EventReference musicEventReference){
        musicEventInstance = createMusicEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition){
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance createEventInstance(EventReference eventReference){
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public EventInstance createMusicEventInstance(EventReference eventReference){
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        musicEventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp(){
        foreach (EventInstance eventInstance in eventInstances){
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    public void StopMusic(){
        foreach (EventInstance eventInstance in musicEventInstances){
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }
    }

    void OnDestroy()
    {
        CleanUp();
    }
    #endregion

}
