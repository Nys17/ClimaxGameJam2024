using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    public  void OnClickStart()  {
        AudioManager.instance.StopMusic();
        AudioManager.instance.InitaliseMusicEvent(FMODEvents.instance.presentMusic);
        SceneManager.LoadScene(1);
    }

    public void OnClickQuit() { Application.Quit(); }

    public void OnClickBackToStartMenu() { 
        AudioManager.instance.StopMusic();
        AudioManager.instance.InitaliseMusicEvent(FMODEvents.instance.menuMusic);
        SceneManager.LoadScene(0);
    }

    public void OnClickOptions() { optionsMenu.SetActive(true); }

   
}
