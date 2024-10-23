using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms;

public class projectileGun1 : MonoBehaviour
{
    bool gunLive = true;

    int numbOfShots;

    [SerializeField] float delayBetweenShots;
    [SerializeField] int maxMagSize;
    [SerializeField] float reloadTime;


    [SerializeField] GameObject gun1Projectile;
    [SerializeField] GameObject endOfBarrel;

    private void Start()
    {
        // if(isLocalPlayer != true) return;
    
        numbOfShots = maxMagSize;
        
    }
    private void FixedUpdate() 
    {
        // if(isLocalPlayer != true) return;
        gunMechanics();
    }

    private void gunMechanics()
    {
        if(gunLive == true)
        {
            //reload with r
            if(Input.GetKeyDown(KeyCode.R) && numbOfShots != 6)
            {
                Invoke("reloadAnimation", delayBetweenShots);
                gunLive = false;
            }

            //fire with left mouse key
            if(Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(gun1Projectile, endOfBarrel.transform.position, endOfBarrel.transform.rotation);
                numbOfShots--;
                Invoke("countAmmo", delayBetweenShots);
                gunLive = false;
                Debug.Log(numbOfShots);
            }
        }
    }

    private void countAmmo()
    {
        if(numbOfShots <= 0) reloadAnimation();
        else gunLive = true;
    }

    private void reloadAnimation()
    {
        Invoke("reload",reloadTime);
        Debug.Log("reload");
    }
    private void reload()
    {
        numbOfShots = maxMagSize;
        Debug.Log(numbOfShots);
        gunLive = true;
    }
}
