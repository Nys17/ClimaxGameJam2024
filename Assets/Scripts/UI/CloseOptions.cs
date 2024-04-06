using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseOptions : MonoBehaviour
{
    [SerializeField] private GameObject OptionsPanel;
    public void CloseOptionsMenu(){
        OptionsPanel.SetActive(false);
    }
}
