using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowObject : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("References")]
    [SerializeField] private Transform playerTransform;
    [Header("Flip Rotation Stats")]
    [SerializeField] private float flipYRotationTime = 0.5f;

     
    private PlayerController player;

    private bool IsFacingRight;

    private Coroutine turnCoroutine;



    private void Awake()
    {
        player = playerTransform.gameObject.GetComponent<PlayerController>();

        IsFacingRight = player.IsFacingRight;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }
    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        IsFacingRight = !IsFacingRight;

        if (IsFacingRight)
        {
            return 180f;

        }
        else
        {
            return 0f;
        }
    }

}
