using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType{
        MASTER,
        MUSIC,
        SFX,
        DIALOGUE
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;
    void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    void Update()
    {
        switch (volumeType){
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.sfxVolume;
                break;
            case VolumeType.DIALOGUE:
                volumeSlider.value = AudioManager.instance.dialogueVolume;
                break;
            default:
                Debug.LogWarning("VolumeType not supported: " + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged(){
        switch (volumeType){
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.sfxVolume = volumeSlider.value;
                break;
            case VolumeType.DIALOGUE:
                AudioManager.instance.dialogueVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VolumeType not supported: " + volumeType);
                break;
        }
    }
}
