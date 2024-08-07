using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float sprintMultiplier = 1;
    public float airMultiplier;
    public float jumpPower;
    private bool readyToJump = false;
    private float sprintSpeed;

    public float playerHeight;
    public float groundDrag;
    public LayerMask whatIsGround;
    private bool grounded;

    private Rigidbody rb;
    public Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        Invoke(nameof(ResetJump), 0.1f);
    }
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight* 0.5f +.2f, whatIsGround);
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
        //checks to see if player is on the floor and adds drag


        Jump(); //allows the player to jump

        SpeedControl(); //sets a speed limit but may prevent boops in the future (may need changing) 

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.rotation.eulerAngles.y, transform.rotation.z));
        //horizontally rotate the player to match camera

    }
    void FixedUpdate()
    {
        Movement(); //movement in fixed update to ensure differing frame rates dont interfere with player speed
    }
    
    private void Movement()
    {
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
