using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("camStuff")]
    [SerializeField] private GameObject cameraFollowGo;
    public float x;
    public float speed = 1f;
    public float jumpHeight;

    public Vector3 boxSize;
    public float castDistance;
    public LayerMask groundLayer;

    public bool isGrounded;
    
    public bool IsFacingRight;
    
    Vector3 Move;
    
    public Rigidbody rb;

    public Animator playerAnim;

    private CamFollowObject camFollowObject;
    private float fallSpeedYDampingChangeThreshold;

    //audio
    private EventInstance footstepInstance;

    // Start is called before the first frame update
    void Start()
    {
        camFollowObject = cameraFollowGo.GetComponent<CamFollowObject>();
        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
        footstepInstance = AudioManager.instance.createEventInstance(FMODEvents.instance.footstepSound);
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        Move = new Vector3(x, 0, 0);
        Move = Move * speed * Time.deltaTime;
        transform.position += Move;
        playerAnim.SetFloat("MovementSpeed", x); ///anim
        // Play footsteps
        UpdateSound();

        if (Input.GetButtonDown("Jump")&& isGrounded)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode.Impulse);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.jumpSound, this.gameObject.transform.position); // Play jump sound
            playerAnim.SetBool("isJumping", true);
        }
        else
        {
            playerAnim.SetBool("isJumping", false);
        }
        //if we are falling past a certain speed threshold
        if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping&& !CameraManager.instance.lerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        // if we are standing still or moing up 
        if(rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping&& CameraManager.instance.lerpedFromPlayerFalling)
        {
            // reset so it can be called again
            CameraManager.instance.lerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }
  

    private void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            TurnCheck();
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up *castDistance, boxSize);

        
    }

    private void TurnCheck()
    {
        if (Input.GetAxisRaw("Horizontal") >0 &&!IsFacingRight)
        {
            Turn();
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && IsFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
            // turn the camera follow object
            camFollowObject.CallTurn();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
            // turn the camera follow object
            camFollowObject.CallTurn();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateSound(){
        if (Move.x != 0 && isGrounded){
            PLAYBACK_STATE playbackState;
            footstepInstance.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED)){
                footstepInstance.start();
            }
        }
        else{
            footstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

}
