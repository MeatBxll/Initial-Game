using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Knight : NetworkBehaviour
{
    //slash melee stuff
    public float SwingSpeed;
    //shield stuff
    public float ShieldMaxHealth;
    [HideInInspector] public float ShieldCurrentHealth;
    [SerializeField] private float RegenerationCooldown;
    [SerializeField] private float BrokenShieldReginCooldown;
    [SerializeField] private float ReginAmount;
    [SerializeField] private float ReginSpeed;
    [HideInInspector] public bool ShieldUp;
    private bool ShieldBroken;
    [SerializeField] private GameObject ShieldObject;

    // fireball stuff
    public Transform FireBallSpawnLocation;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private float FireBallSpeed;
    [SerializeField] private float FireBallBaseDmg;
    private bool fireballOnCooldown;
    [HideInInspector] public float[] FireBallElements;
    //fire smoke stuff
    [SerializeField] private GameObject smokeSpawnObject;
    [SerializeField] private float smokeCooldown;
    public float smokeSpeed;
    public float smokeDurration;
    private bool smokeOnCooldown;

    //Passive stuff
    [SerializeField] private int MaxPassiveStacks;
    [SerializeField] private int PassiveBurnDmg;
    [SerializeField] private int PassiveBurnDurration;
    private int CurrentPassiveCount;

    [HideInInspector] public Animator animator;
    

    void Start()
    {
        //packages all the important knightElements for Adjustments as the player gets more ap or ad
        FireBallElements = new float[3];
        FireBallElements[0] = FireBallBaseDmg;
        FireBallElements[1] = PassiveBurnDmg;
        FireBallElements[2] = PassiveBurnDurration;

        ShieldCurrentHealth = ShieldMaxHealth;
        fireballOnCooldown = false;
        smokeOnCooldown = false;
        CurrentPassiveCount = 0;

        animator = gameObject.GetComponent<player>().animator;
        animator.gameObject.GetComponent<KnightAnimator>().knight = gameObject.GetComponent<Knight>();
        animator.SetBool("Blocking", false);
        ShieldObject.GetComponent<KnightShield>().CharacterBody = gameObject;
        CmdSetShield(false);

        animator.SetFloat("SwingSpeed", SwingSpeed);
    }

    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused == true) return;
        if(!ShieldBroken)ShieldFunctionality(); // cast shield stuff
        if(!fireballOnCooldown && Input.GetKeyDown(KeyCode.Q)) CastFireBall();
        if(!smokeOnCooldown && Input.GetKeyDown(KeyCode.E)) CastSmoke();
        if (Input.GetMouseButton(0) && !ShieldUp ) SlashMelee();
        if(ShieldUp) animator.SetBool("IsSwinging", false);
    }
    private void SlashMelee()
    {
        if(animator.GetBool("IsSwinging") == true) return;
        animator.SetBool("IsSwinging", true);
    }

    public void TakeDamage(float dmg) //shield Take dmg
    {
        ShieldCurrentHealth -= dmg;
        if(ShieldCurrentHealth <= 0) 
        {
            ShieldBroken = true;
            ShieldFunctionality();
        }
    }

    private void ShieldFunctionality()
    {
        Debug.Log(ShieldUp);
        if(Input.GetMouseButton(1))
        {
            if(!ShieldUp)
            {
                ShieldUp = true;
                CmdSetShield(true);
                CancelInvoke("RegenerateShieldHealth"); //cancels shield health regin when shield is up
                animator.SetBool("Blocking", true);
            }
        }
        else
        {
            if(ShieldUp) 
            {
                ShieldUp = false;
                CmdSetShield(false);
                Invoke("RegenerateShieldHealth", RegenerationCooldown); //starts the shield health recharge when shield isnt up 
                animator.SetBool("Blocking", false);
            }
        }
        if(ShieldBroken) 
        {
            ShieldUp = false;
            ShieldObject.SetActive(false);
            Invoke("RegenerateShieldHealth", BrokenShieldReginCooldown); //starts the shield health recharge when shield is broken 
        }
    }
    
    [Command]
    void CmdSetShield(bool a)
    {
        ShieldObject.SetActive(a);
    }

    private void RegenerateShieldHealth()
    {
        if(!ShieldUp)
        {
            if(ShieldCurrentHealth == ShieldMaxHealth) return;
            Invoke("RegenerateShieldHealth", ReginSpeed);
            if(ShieldCurrentHealth >= ShieldMaxHealth) ShieldCurrentHealth = ShieldMaxHealth;
            else ShieldCurrentHealth += ReginAmount;
        }
    }

    //fireball stuff
    private void CastFireBall()
    {
        if(!fireballOnCooldown)
        {
            fireballOnCooldown = true;
            Invoke("CastFireBall", fireballCooldown);
            animator.SetBool("Fireball", true);
        }
        else fireballOnCooldown = false;
    }

    

    [Command]
    public void CmdCastFireBall(bool team)
    {
        // used to fix the rotation if it is off
        Quaternion FixSpawnLocation = Quaternion.Euler(FireBallSpawnLocation.rotation.x, FireBallSpawnLocation.rotation.y, FireBallSpawnLocation.rotation.z + 90);
        GameObject fireBallClone = Instantiate(FireBall, FireBallSpawnLocation.position, FixSpawnLocation);
        fireBallClone.GetComponent<Rigidbody>().velocity = FireBallSpawnLocation.transform.forward * FireBallSpeed; 
        fireBallClone.GetComponent<KnightFireBall>().IsRedTeam = team;
        fireBallClone.GetComponent<KnightFireBall>().PlayerThatSpawnedFireBall = gameObject;
        fireBallClone.GetComponent<KnightFireBall>().FireBallElements = FireBallElements;

        NetworkServer.Spawn(fireBallClone);
        Destroy(fireBallClone, 10);
    }

    //Passive Stuff
    public void IncreasePassiveCount()
    {
        if(!isOwned) return;
        if(CurrentPassiveCount == MaxPassiveStacks) return;
        if(CurrentPassiveCount > MaxPassiveStacks) CurrentPassiveCount = MaxPassiveStacks;
        CurrentPassiveCount++;
    }

    private void DepreciatePassive()
    {

    }

    public void UpdateStats()
    {
        
    }
    
    //fire smoke stuff
    private void CastSmoke()
    {
        if(!smokeOnCooldown)
        {
            smokeOnCooldown = true;
            Invoke("CastSmoke", smokeCooldown);
            animator.SetBool("Smoke", true);
        }
        else smokeOnCooldown = false;
    }
    [Command]
    public void CmdCastSmoke()
    {
        // used to fix the rotation if it is off
        Quaternion FixSpawnLocation = Quaternion.Euler(FireBallSpawnLocation.rotation.x, FireBallSpawnLocation.rotation.y, FireBallSpawnLocation.rotation.z);
        GameObject smokeSpawnerClone = Instantiate(smokeSpawnObject, FireBallSpawnLocation.position, FixSpawnLocation);
        smokeSpawnerClone.GetComponent<KnightSmoke>().PlayerThatSpawnedSmoke = gameObject;
        NetworkServer.Spawn(smokeSpawnerClone);
    }
}
