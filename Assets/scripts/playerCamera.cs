using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    public float sensitivity;
    public Rigidbody player;

    public GameObject vertBoundUp;
    public GameObject vertBoundDn;
    private bool vBF = false;

    private Rigidbody rb;
    private RaycastHit rayHit;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalRotate = Input.GetAxis("Mouse X");
        float verticalRotate = Input.GetAxis("Mouse Y");


        //Debug.Log(transform.eulerAngles.x);
        //horizontal rotation rotates the player not the camera to prevent rotation on the z axis
        if(horizontalRotate != 0)
        {
            player.transform.Rotate(0, horizontalRotate * sensitivity, 0);
        }

        //vertical rotation uses a raycast to detect vertBound to prevent the player from being able to look too far in the vertical direction
        if (verticalRotate != 0)
        {

            if (Physics.Raycast(transform.position, transform.forward, out rayHit, 10f))
            {
                if (rayHit.transform.gameObject == vertBoundUp || rayHit.transform.gameObject == vertBoundDn)
                {
                    Debug.Log(rayHit.transform.gameObject.name);
                    vBF = true;
                }
            }
            if (vBF == false)
            {
                rb.transform.Rotate(-verticalRotate * sensitivity, 0, 0);
            }
        }
    }
}
//-83 83