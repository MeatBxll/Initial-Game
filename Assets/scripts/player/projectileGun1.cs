using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SocialPlatforms;
using Mirror;

public class projectileGun1 : NetworkBehaviour
{
    bool gunLive = true;
    bool reloading = false;

    int numbOfShots;

    [SerializeField] float delayBetweenShots;
    [SerializeField] int maxMagSize;
    [SerializeField] float reloadTime;



    [SerializeField] GameObject gun1Projectile;
    [SerializeField] private float projectileSpeed;

    [SerializeField] Transform endOfBarrel;

    private void Start()
    {
        if(isLocalPlayer != true) return;
        numbOfShots = maxMagSize;
        
    }
    private void FixedUpdate() 
    {
        if(isLocalPlayer != true) return;
        gunMechanics();
    }

    private void gunMechanics()
    {
        //reload with r
        if(Input.GetKeyDown(KeyCode.R) && numbOfShots != 6 && reloading == false)
        {    
                Invoke("reloadAnimation", delayBetweenShots);
                gunLive = false;
        }
        if(gunLive == true)
        {
            //fire with left mouse key
            if(Input.GetKey(KeyCode.Mouse0))
            {
                // GameObject bulletClone = Instantiate(gun1Projectile, endOfBarrel.position, endOfBarrel.rotation);
                // bulletClone.GetComponent<Rigidbody>().velocity = endOfBarrel.transform.forward * projectileSpeed;
                CmdSpawnBullet();
                numbOfShots--;
                Invoke("countAmmo", delayBetweenShots);
                gunLive = false;
                Debug.Log(numbOfShots);
            }
        }
    }

    [Command]
    public void CmdSpawnBullet()
    {
        GameObject bulletClone = Instantiate(gun1Projectile, endOfBarrel.position, endOfBarrel.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = endOfBarrel.transform.forward * projectileSpeed;
        NetworkServer.Spawn(bulletClone);
        Destroy(bulletClone, 10);
    }

    private void countAmmo()
    {
        if(numbOfShots <= 0) reloadAnimation();
        else gunLive = true;
    }

    private void reloadAnimation()
    {
        reloading = true;
        Invoke("reload",reloadTime);
        Debug.Log("reloading");
    }
    private void reload()
    {
        numbOfShots = maxMagSize;
        Debug.Log(numbOfShots);
        gunLive = true;
        reloading = false;
    }
}
