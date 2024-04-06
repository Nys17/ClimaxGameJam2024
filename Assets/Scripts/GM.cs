using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
///  !!!!   ADD GM TAG TO THE OBJECT THAT HAS THIS SCRIPT
/// </summary>
/// 

public class GM : MonoBehaviour
{
   public enum State { Past, Present};

    public State currentState;

    public GameObject PastLevel;
    public GameObject PresentLevel;
    PauseMenu pauseRef;
   public List<int> CollectedKeys = new List<int>();

    bool allKeysCollected = false;

    public TMP_Text numberOfKeys;
    private void Start()    
    {
        PastLevel.SetActive(false);
        pauseRef = GetComponent<PauseMenu>();
        currentState = State.Present;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            if (PastLevel.activeInHierarchy == false)
            {
              Invoke("GoToPast",0.5f);
            }
            else
            {   
                Invoke("GoToPresent", 0.5f);
            }

        }

        if (Input.GetKeyDown(KeyCode.Escape)) { pauseRef.PauseBehaviour(); }// pause menu

        if(numberOfKeys.text != CollectedKeys.Count.ToString()) { ChangeKeyNumberText(); }
        ChangeKeyNumberText();
    }
    public void GoToPast()
    {
        PastLevel.SetActive(true);
        PresentLevel.SetActive(false);
        currentState = State.Past;

    }

    public void GoToPresent()
    {
        PastLevel.SetActive(false);
        PresentLevel.SetActive(true);
        currentState = State.Present;
    }

     void AllKeysAreCollected()
    {
            if(CollectedKeys.Count == 4)
             {
            allKeysCollected = true;
        }
    }

    public bool GetKeysCollectedBool() { return  allKeysCollected; } 

    public void ChangeKeyNumberText()
    {
        numberOfKeys.text = CollectedKeys.Count.ToString();
    }
}
