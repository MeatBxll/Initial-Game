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
    private bool gunLive = true;
    private bool reloading = false;
    private int numbOfShots;

    [SerializeField] float delayBetweenShots;
    [SerializeField] int maxMagSize;
    [SerializeField] float reloadTime;



    [SerializeField] GameObject gun1Projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] Transform endOfBarrel;
    [SerializeField] Animator animator;
    
    private void Start()
    {
        if(isOwned != true) return;
        numbOfShots = maxMagSize;
        GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().BulletAmountVisuals(maxMagSize , maxMagSize);
        
    }
    private void FixedUpdate() 
    {
        if(isOwned != true) return;

        //disables gun if game is paused
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
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
                gameObject.GetComponent<player>().animator.SetBool("isShooting", true);
                CmdSpawnBullet(gameObject.GetComponent<Health>().IsRedTeam);
                numbOfShots--;
                Invoke("countAmmo", delayBetweenShots);
                gunLive = false;
                GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().BulletAmountVisuals(numbOfShots, maxMagSize);

            }
        }
    }

    [Command]
    public void CmdSpawnBullet(bool g)
    {
        GameObject bulletClone = Instantiate(gun1Projectile, endOfBarrel.position, endOfBarrel.rotation);
        bulletClone.GetComponent<Rigidbody>().velocity = endOfBarrel.transform.forward * projectileSpeed;
        bulletClone.GetComponent<projectile>().redTeamBullet = g;
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
        gameObject.GetComponent<player>().animator.SetBool("isReloading", true);
    }
    private void reload()
    {
        gameObject.GetComponent<player>().animator.SetBool("isReloading", false);
        numbOfShots = maxMagSize;
        gunLive = true;
        reloading = false;
        GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().BulletAmountVisuals(numbOfShots, maxMagSize);
    }
}
