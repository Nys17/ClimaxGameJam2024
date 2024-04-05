using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    public GameObject PastLevel;
    public GameObject PresentLevel;

    private void Start()
    {
        PastLevel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab)) {
            if (PastLevel.activeInHierarchy == false)
            {
              Invoke("GoToPast",0.5f);
            }
            else
            {
                Invoke("GoToPresent", 0.5f);
            }

        }
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
}
