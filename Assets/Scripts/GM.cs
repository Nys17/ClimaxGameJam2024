using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  !!!!   ADD GM TAG TO THE OBJECT THAT HAS THIS SCRIPT
/// </summary>
public class GM : MonoBehaviour
{
    public GameObject PastLevel;
    public GameObject PresentLevel;
    PauseMenu pauseRef;
   public List<KeyData> CollectedKeys = new List<KeyData>();
    private void Start()    
    {
        PastLevel.SetActive(false);
        pauseRef = GetComponent<PauseMenu>();
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
    }
    public void GoToPast()
    {
        PastLevel.SetActive(true);
        PresentLevel.SetActive(false);

    }

    public void GoToPresent()
    {
        PastLevel.SetActive(false);
        PresentLevel.SetActive(true);

    }

    public void AllKeysAreCollected()
    {
        //// do something when you collect all keys
    }
}
