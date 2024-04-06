using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;

public class DialogueObserver : Observer
{
#region Variables
    [Header("Audio")]
    [SerializeField] private EventReference dialogueSound;
    [SerializeField] private EventReference dialogueEndSound;

    [Header("Common")] public TextMeshProUGUI text;
    [Range(0.01f, 0.5f)] public float textSpeed;
    private int index;
    private bool textStarted = false;

    [Header("Conditional")]
    public bool isConditionMet = true;
    public string[] conditionFailedLines;

    [Header("Non-Conditional")]
    public string[] conditionPassedLines;

#endregion

    void Start()
    {
        text.text = string.Empty;
        text.gameObject.SetActive(false);
    }

    protected override void OnEventFired()
    {
        if (textStarted == false){
            text.text = string.Empty;
            StartDialogue();
        }
        else{
            if (isConditionMet != true){
                if(text.text == conditionFailedLines[index]){
                    ContinueDialogue();
                }
                else{
                    StopAllCoroutines();
                    text.text = conditionFailedLines[index];
                    AudioManager.instance.PlayOneShot(dialogueEndSound, this.transform.position);
                }
            }
            else{
                if(text.text == conditionPassedLines[index]){
                    ContinueDialogue();
                }
                else{
                    StopAllCoroutines();
                    text.text = conditionPassedLines[index];
                    AudioManager.instance.PlayOneShot(dialogueEndSound, this.transform.position);
                }
            }
        }
    }

#region Start/Continue Dialogue
    private void StartDialogue(){
        index = 0;
        text.gameObject.SetActive(true);
        if (isConditionMet != true){
            StartCoroutine(TypeLine(conditionFailedLines));
        }
        else{
            StartCoroutine(TypeLine(conditionPassedLines));
        }
        textStarted = true;
    }

    private void ContinueDialogue(){
        if (isConditionMet != true){
            if (index < conditionFailedLines.Length - 1){
                index++;
                text.text = string.Empty;
                StartCoroutine(TypeLine(conditionFailedLines));
            }
            else{
                textStarted = false;
                text.gameObject.SetActive(false);
            }
        }
        else{
            if (index < conditionPassedLines.Length - 1){
                index++;
                text.text = string.Empty;
                StartCoroutine(TypeLine(conditionPassedLines));
            }
            else{
                textStarted = false;
                text.gameObject.SetActive(false);

            }
        }
    }

#endregion

#region Coroutines

    IEnumerator TypeLine(string[] passedLines){
        foreach (char c in passedLines[index].ToCharArray()){
            text.text += c;
            AudioManager.instance.PlayOneShot(dialogueSound, this.transform.position);
            yield return new WaitForSeconds(textSpeed);
        }
        AudioManager.instance.PlayOneShot(dialogueEndSound, this.transform.position);
    }

#endregion
}
