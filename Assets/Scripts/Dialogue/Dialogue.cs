using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
#region Variables

    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;
    private int index;

#endregion

#region Start/Update

    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            if(text.text == lines[index]){
                ContinueDialogue();
            }
            else{
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }
#endregion

#region Start/Continue Dialogue

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    void ContinueDialogue(){
        if (index < lines.Length - 1){
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            gameObject.SetActive(false);
        }
    }

#endregion

#region Coroutines

    IEnumerator TypeLine(){
        foreach (char c in lines[index].ToCharArray()){
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

#endregion
}
