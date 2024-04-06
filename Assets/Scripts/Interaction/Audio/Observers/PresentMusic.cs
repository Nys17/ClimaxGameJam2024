using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentMusicObserver : Observer
{
    protected override void OnEventFired()
    {
        if(!ChangePresentPastMusic.instance.isPresent){
            AudioManager.instance.StopMusic();
            AudioManager.instance.InitaliseMusicEvent(FMODEvents.instance.presentMusic);
        }
    }
}
