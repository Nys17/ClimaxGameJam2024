using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonsBehaviour : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    public  void OnClickStart()  {SceneManager.LoadScene(1); }

    public void OnClickQuit() { Application.Quit(); }

    public void OnClickBackToStartMenu() { SceneManager.LoadScene(0); }

    public void OnClickOptions() { optionsMenu.SetActive(true); }

   
}
