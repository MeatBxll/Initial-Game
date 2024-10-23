using UnityEngine;
using UnityEngine.UIElements;
using Mirror;

public class player : NetworkBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sprintMultiplier = 1;
    [SerializeField] float airMultiplier;
    [SerializeField] float jumpPower;
    bool readyToJump = true;
    float sprintSpeed;

    [SerializeField] float playerHeight;
    [SerializeField] float groundDrag;
    [SerializeField] LayerMask whatIsGround;
    bool grounded;

    Rigidbody rb;
    [SerializeField] Transform cam;




    void Start()
    {
        if(!isLocalPlayer) return;
        rb = GetComponent<Rigidbody>();
    }

    

    private void Update()
    {
        if(!isLocalPlayer) return;
        Jump(); //allows the player to jump

        SpeedControl(); //sets a speed limit but may prevent boops in the future (may need changing) 
        
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.rotation.eulerAngles.y, transform.rotation.z));
        //horizontally rotate the player to match camera

    }

    void FixedUpdate()
    {
        if(!isLocalPlayer) return;
        Move(); //movement in fixed update to ensure differing frame rates dont interfere with player speed
    }

    public void Move()
    {
        //checks to see if player is on the floor and adds drag
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight* 0.5f +.2f, whatIsGround);
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;            
        
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1

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
        if (Input.GetKey("space") && readyToJump == true && grounded == true)
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
