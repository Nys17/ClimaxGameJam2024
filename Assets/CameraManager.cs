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
    private Coroutine panCameraCoroutine;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private float normYPanAmount;

    private Vector2 startingTrackedObjectOffset;

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

        //set the starting position of the tracked object offset
        startingTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;
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

    #region Pan Camera
    public void PanCameraOnContact( float panDistance, float panTime, PanDirection panDirection , bool panToStartingPos)
    {
        panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection,panToStartingPos));
    }
    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos = Vector2.zero;

        //handle pan from trigger
        if (!panToStartingPos)
        {
            //set direction and distance
            switch (panDirection)
            {
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                case PanDirection.Left:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.left;
                    break;
                default:
                    break;
            }
            endPos *= panDistance;

            startingPos = startingTrackedObjectOffset;
            endPos += startingPos;

        }
        // handle pan back to starting pos
        else
        {
            startingPos = framingTransposer.m_TrackedObjectOffset;
            endPos = startingTrackedObjectOffset;
        }
        //handle the actual panning of the camera 
        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            framingTransposer.m_TrackedObjectOffset = panLerp;
            yield return null;
        }
    }
    #endregion
}
