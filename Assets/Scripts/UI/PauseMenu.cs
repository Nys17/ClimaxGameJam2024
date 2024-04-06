using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pMenu;

    bool isPaused = false;
    void Start()
    {
        pMenu = GameObject.FindWithTag("PauseMenu");
        pMenu.SetActive(false);
    }

     public void PauseBehaviour()
    {

        if (isPaused)
        {
            Unpause();
        }

        else
        {
            Pause();
        }

    }
    void Pause()
    {
        pMenu.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
