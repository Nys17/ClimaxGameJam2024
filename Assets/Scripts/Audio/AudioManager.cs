using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Video;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    #region Variables

    [field: Header("Volumes")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float dialogueVolume = 1f;

    [field: Header("Buses")]
    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;
    private Bus dialogueBus;
    
    [field: Header("Events & Instances")]
    private List<EventInstance> eventInstances;
    private List<EventInstance> musicEventInstances;
    private EventInstance musicEventInstance;

    #endregion

    #region Initialisation & Singleton
    public static AudioManager instance {get; private set;}

    void Awake()
    {
        if (instance != null){
            Debug.Log("More then 1 Audio Manager in Scene.");
            if (instance != this){
                Destroy(gameObject);
            }
        }
        else{
            instance = this;
           
            musicEventInstances = new List<EventInstance>();

            #region Bus Init
            masterBus = RuntimeManager.GetBus("bus:/");
            musicBus = RuntimeManager.GetBus("bus:/Music");
            sfxBus = RuntimeManager.GetBus("bus:/SFX");
            dialogueBus = RuntimeManager.GetBus("bus:/Dialogue");
            #endregion
        }
        eventInstances = new List<EventInstance>();
    }
    #endregion

    void Start()
    {
        StopMusic();
        InitaliseMusicEvent(FMODEvents.instance.menuMusic);
    }
    
    void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
        dialogueBus.setVolume(dialogueVolume);
    }

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
