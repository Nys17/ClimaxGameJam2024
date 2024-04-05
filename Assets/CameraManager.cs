using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] private CinemachineVirtualCamera[] allVirtualCameras;
    [SerializeField] private float fallPanAmout = 0.25f;
    [SerializeField] private float fallYPanTime = 0.35f;

    public float fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool lerpedFromPlayerFalling{ get;  set; }

    private Coroutine lerpYPanCoroutine;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normYPanAmount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < allVirtualCameras.Length; i++)
        {
            if (allVirtualCameras[i].enabled)
            {
                //set the current active camera
                currentCamera = allVirtualCameras[i];
                //set tyhe framing transposer
                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }

        // set y damping amount to inspector value
        normYPanAmount = framingTransposer.m_YDamping;

    }
    #region Lerp YDamping
    public void LerpYDamping(bool isPlayerFalling)
    {
        lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;
        // grab the starting damingamount 
        float startDampAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;
        // determine the end damping amount
        if (isPlayerFalling)
        {
            endDampAmount = fallPanAmout;
            lerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normYPanAmount;
        }
        float elapsedTIme = 0f;
        while (elapsedTIme < fallYPanTime)
        {
            elapsedTIme += Time.deltaTime;
            float LerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTIme / fallYPanTime));
            framingTransposer.m_YDamping = LerpedPanAmount;
            yield return null;
        }
        IsLerpingYDamping = false;
    }
    #endregion

}
