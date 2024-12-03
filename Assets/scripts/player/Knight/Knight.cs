using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Knight : NetworkBehaviour
{
    //shield stuff
    public float ShieldMaxHealth;
    public float ShieldCurrentHealth;
    [SerializeField] private float RegenerationCooldown;
    [SerializeField] private float BrokenShieldReginCooldown;
    [SerializeField] private float ReginAmount;
    [SerializeField] private float ReginSpeed;
    private bool ShieldUp;
    private bool ShieldBroken;
    [SerializeField] GameObject ShieldObject;

    // fireball stuff
    [SerializeField] private Transform FireBallSpawnLocation;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private float FireBallSpeed;
    [SerializeField] private float FireBallBaseDmg;
    private bool fireballOnCooldown;
    private float[] FireBallElements; //packages all the important fireball elements to send to the fireball

    //Passive stuff
    [SerializeField] private int MaxPassiveStacks;
    [SerializeField] private int PassiveBurnDmg;
    [SerializeField] private int PassiveBurnDurration;
    private int CurrentPassiveCount;
    

    void Start()
    {
        //packages all the important fireball elements to send to the fireball
        FireBallElements = new float[3];
        FireBallElements[0] = FireBallBaseDmg;
        FireBallElements[1] = PassiveBurnDmg;
        FireBallElements[2] = PassiveBurnDurration;

        ShieldCurrentHealth = ShieldMaxHealth;
        fireballOnCooldown = false;
        CurrentPassiveCount = 0;
    }

    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused == true) return;
        if(!ShieldBroken)ShieldFunctionality(); // cast shield stuff
        if(!fireballOnCooldown && Input.GetKeyDown(KeyCode.Q)) CastFireBall();
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
        if(Input.GetMouseButton(0))
        {
            if(!ShieldUp)
            {
                ShieldUp = true;
                ShieldObject.SetActive(false);
                CancelInvoke("RegenerateShieldHealth"); //cancels shield health regin when shield is up
            }
        }
        else
        {
            if(ShieldUp) 
            {
                ShieldUp = false;
                ShieldObject.SetActive(false);
                Invoke("RegenerateShieldHealth", RegenerationCooldown); //starts the shield health recharge when shield isnt up 
            }
        }
        if(ShieldBroken) 
        {
            ShieldUp = false;
            ShieldObject.SetActive(false);
            Invoke("RegenerateShieldHealth", BrokenShieldReginCooldown); //starts the shield health recharge when shield is broken 
        }
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
            CmdCastFireBall(gameObject.GetComponent<Health>().IsRedTeam);
        }
        else fireballOnCooldown = false;
    }

    [Command]
    void CmdCastFireBall(bool team)
    {
        GameObject fireBallClone = Instantiate(FireBall, FireBallSpawnLocation.position, FireBallSpawnLocation.rotation);
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
        if(CurrentPassiveCount == MaxPassiveStacks) return;
        if(CurrentPassiveCount > MaxPassiveStacks) CurrentPassiveCount = MaxPassiveStacks;
        CurrentPassiveCount++;
    }
    
}
