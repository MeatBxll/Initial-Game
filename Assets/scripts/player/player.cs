using UnityEngine;
using Mirror;
using UnityEditor;
using Unity.Cinemachine;
using System.Runtime.InteropServices.WindowsRuntime;

public class player : NetworkBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sprintMultiplier = 1;
    [SerializeField] float airMultiplier;
    [SerializeField] float jumpPower;
    private bool readyToJump = true;
    private float sprintSpeed;

    [SerializeField] float playerHeight;
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask whatIsGround;
    private bool grounded;
    private GameObject _pauseMenu;
    private bool gameIsPaused;

    private Rigidbody rb;
    [SerializeField] private GameObject cam;

    public Animator animator;
    private bool stopMove = false;

    void Start()
    {
        if(!isOwned) return;
        rb = GetComponent<Rigidbody>();

        _pauseMenu = GameObject.FindGameObjectWithTag("playerUI");
        _pauseMenu.GetComponent<PlayerUI>().OnPlayerStart();
        stopMove = false;

    }

    private void Update()
    {
        if(!isOwned) return;
        if(stopMove == true) return;
        //sets FOV
        if(PlayerPrefs.GetFloat("FOV") > 50f )
            cam.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetFloat("FOV");

        //disables camera and jump while game is paused
        gameIsPaused = _pauseMenu.GetComponent<PlayerUI>().GamePaused;
        if(gameIsPaused) cam.GetComponent<CinemachineInputAxisController>().enabled = false;
        else if(!gameIsPaused) cam.GetComponent<CinemachineInputAxisController>().enabled = true;

        
        //sets the sensitivity of the player
        foreach (var c in cam.GetComponent<CinemachineInputAxisController>().Controllers)
        {
            if (c.Name == "Look X (Pan)")
            {
                c.Input.LegacyGain = PlayerPrefs.GetFloat("Sensitivity") * 200f;

            }
            if (c.Name == "Look Y (Tilt)")
            {
                c.Input.LegacyGain = PlayerPrefs.GetFloat("Sensitivity") * -200f;
            }
        }

        if (Input.GetKey("space") && readyToJump && grounded && !gameIsPaused) Jump();
        
        SpeedControl(); //sets a speed limit but may prevent boops in the future (may need changing) 
        
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.transform.rotation.eulerAngles.y, transform.rotation.z));
        //horizontally rotate the player to match camera


    }

    void FixedUpdate()
    {
        if(!gameIsPaused && isOwned) Move(); //movement in fixed update to ensure differing frame rates dont interfere with player speed
    }

    public void Move()
    {
        //checks to see if player is on the floor and adds drag
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight* 0.5f +.2f, whatIsGround);
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;            
        
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

        //animates walking and idle
        if(new Vector3(xMove, 0, zMove) == Vector3.zero) animator.SetBool("isWalking", false);
        else animator.SetBool("isWalking", true);


        if (Input.GetKey(KeyCode.LeftShift)) sprintSpeed = speed * sprintMultiplier;
        else sprintSpeed = 0f;

        if (grounded)
            rb.AddRelativeForce(xMove * (speed + sprintSpeed) * 10, 0, zMove * (speed + sprintSpeed) * 10, ForceMode.Force); //moves player normally when theyre on the ground

        else if (!grounded)
            rb.AddRelativeForce(xMove * speed * 10, 0, zMove * speed * airMultiplier * 10, ForceMode.Force); //moves them slower if theyre in the air
    }

    private void SpeedControl()
    {

        Vector3 flatVelocity = new Vector3(rb.velocity.x,0,rb.velocity.z);

        //limit velocity
        if(flatVelocity.magnitude > speed + sprintSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    
    }
    private void Jump()
    {
        {
            rb.AddForce(transform.up * jumpPower * 10);
            readyToJump = false;
            Invoke(nameof(ResetJump), 0.1f);//sets a time limit before you can jump again to prevent spam for higher jump
        }
        //allows player to jump if theyre on the floor
    
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
