using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private Rigidbody rb;
    private bool detectCollisionDelay = false;
    public float collisionDelay;
    public float projectileSpeed;

    void Start()
    {
        Invoke("detectCollision", collisionDelay);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(detectCollisionDelay == true)
        {
            detectCollision();
        }

        rb.AddRelativeForce(0, 0 ,projectileSpeed/10000, ForceMode.Force);
    }

    private void detectCollision()
    {
        detectCollisionDelay = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(detectCollisionDelay == true)
        {
            //Check for a match with the specified name on any GameObject that collides with your GameObject
            if (collision.gameObject.name == "MyGameObjectName")
            {
                //If the GameObject's name matches the one you suggest, output this message in the console
                Debug.Log("Do something here");
            }

            //Check for a match with the specific tag on any GameObject that collides with your GameObject
            if (collision.gameObject.tag == "MyGameObjectTag")
            {
                //If the GameObject has the same tag as specified, output this message in the console
                Debug.Log("Do something else here");
            }
            Debug.Log("destroy");
            Destroy(gameObject,0f);
        }
    }


}
