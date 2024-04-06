using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    public bool shouldAnim = false;

    public Sprite[] spritesForAnim;

    public int spriteIndex;

   public Image spriteImage;

    public float animSpeed;
    private void Start()
    {
        spriteImage = GetComponent<Image>();
        spriteIndex = 0;
    }

    IEnumerator playToyAnimation()
    {
        while (shouldAnim)
        {


            yield return new WaitForSeconds(animSpeed);

            if (spriteIndex >= spritesForAnim.Length)
            {
                spriteIndex = 0;
            }


            spriteImage.sprite = spritesForAnim[spriteIndex];

            spriteIndex += 1;

        }




    }

    public void StartAnim()
    {
        StartCoroutine(playToyAnimation());
    }

    public void StopAnim()
    {
        spriteImage.sprite = spritesForAnim[0];
        StopCoroutine(playToyAnimation());

    }

    protected void SpriteIndex()
    {
        spriteIndex++;
    }

 

    void AnimToy()
    {
        if (spriteIndex >= spritesForAnim.Length)
        {
            spriteIndex = 0;
        }

        for (int i = 0; i < spritesForAnim.Length; i++)
        {
            spriteImage.sprite = spritesForAnim[i];
        }
    }
}
