using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimSwitchingYime : MonoBehaviour
{
    public Animator textAnim;

    public UIAnimation uiAnimRef;

    public Image img;

    public Sprite one;
    public Sprite two;
    GM gmRef;
    void Start()
    {
        gmRef = GameObject.FindGameObjectWithTag("GM").GetComponent<GM>();
       // uiAnimRef = GetComponent<UIAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) {

            Anim();
            //uiAnimRef.shouldAnim = true;
            //uiAnimRef.StartAnim();


        }

        //if (uiAnimRef.spriteIndex == uiAnimRef.spritesForAnim.Length - 1)
        //{
        //    uiAnimRef.StopAnim();
        //}
    }

    void Anim()
    {
        if(gmRef.currentState == 0 /*Past*/) { textAnim.SetBool("Past", false); textAnim.SetBool("Present", true); img.sprite = one; /*uiAnimRef.spriteImage.sprite = uiAnimRef.spritesForAnim[0];*/ }

        else { textAnim.SetBool("Past", true); textAnim.SetBool("Present", false); img.sprite = two; /*uiAnimRef.spriteImage.sprite = uiAnimRef.spritesForAnim[11];*/ }



            }
}
