using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    private float runningSpeed = 15f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;
    public float blinkSpeed = 10f;
    
    public bool canBlink = true;
    public bool canWallJump = true;

    public float fallHeight = -10f;

    private float blinkResetTime = 0.8f;
    private float wallJumpResetTime = 0.7f;
    private Vector3 startPosition;
    
    // Get audio source and clips to play
    private AudioSource Audio;
    public AudioClip jumpSound;
    public AudioClip blinkSound;
    public AudioClip wallJumpSound;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    // n - 1 gives player jumps due to collision
    [HideInInspector]
    public int jumplimit = 1;
    public int jumps = 1;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        startPosition = transform.position;

        // Source used for playing Sound Effect
        Audio = GetComponent<AudioSource>();

    }

    // Function that is called after some time to reset blink bool
    void ResetBlink()
    {
        canBlink = true;
    }

    // Resets bool when invoked
    void ResetWallJump()
    {
        canWallJump = true;
    }

    // Parent PLayer to platform so that it moves along with it
    void OnControllerColliderHit(ControllerColliderHit other){

        // Set Respawn Point, Offset is needed for some reason
        if (other.gameObject.tag == "Respawn") {
            startPosition = new Vector3(other.transform.position.x - 1, other.transform.position.y + 1, other.transform.position.z - 1);

        } else if (other.gameObject.tag == "Wall") {

            // Give the player lateral speed and reduce their jumps
            if (Input.GetButtonDown("Jump") && canWallJump) {
                moveDirection.y = jumpSpeed;
                canWallJump = false;
                Audio.PlayOneShot(wallJumpSound, 0.9f);
                jumps--;
                Invoke("ResetWallJump", wallJumpResetTime);

            }
        }
    }
    

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonDown("Jump") && canMove && jumps > 0)
        {
            moveDirection.y = jumpSpeed;
            jumps--;
            Audio.PlayOneShot(jumpSound, 0.9f);
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        } else {
            jumps = jumplimit;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        // Code for short range dash / teleport
        if (Input.GetButtonDown("Blink") && canBlink)
        {   
            characterController.Move(playerCamera.transform.forward * blinkSpeed);
            canBlink = false;
            Audio.PlayOneShot(blinkSound, 0.9f);
            Invoke("ResetBlink", blinkResetTime);
        }

        if (Input.GetKeyDown("r") || characterController.transform.position.y < fallHeight){
            characterController.enabled = false;
            characterController.transform.position = startPosition;
            characterController.enabled = true;

            Debug.Log("Player Respawned");
        }
    }
}