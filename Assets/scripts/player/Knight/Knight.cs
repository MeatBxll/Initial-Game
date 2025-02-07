using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.UI;

public class Knight : NetworkBehaviour
{
    //slash melee stuff
    [Header("Knight Sword")]
    public float SwingSpeed;
    public GameObject SwordObj;
    public float SwordBaseDmg;


    [Header("Knight Shield")]
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


    [Header("Knight FireBall")]
    // fireball stuff
    public Transform FireBallSpawnLocation;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private float fireballCooldown;
    [SerializeField] private float FireBallSpeed;
    [SerializeField] private float FireBallBaseDmg;
    private bool fireballOnCooldown;
    [HideInInspector] public float[] FireBallElements;

    [Header("Knight Smoke")]
    //fire smoke stuff
    [SerializeField] private GameObject smokeSpawnObject;
    [SerializeField] private float smokeCooldown;
    public float smokeSpeed;
    public float smokeDurration;
    private bool smokeOnCooldown;

    [Header("Knight Ult")]
    //ult stuff
    [SerializeField] private GameObject ultFireController;
    public GameObject ultFire;
    public float ultBallWidth;
    [SerializeField] private float ultCooldown;
    [SerializeField] private float dashStrength;
    [SerializeField] private float KnightUltFireLeftBehindDurration;
    public float KnightUltFireLeftBehindBaseDmg;
    [SerializeField] float swordUltDmgIncrease;
    private bool ultOnCooldown;


    [Header("Knight Passive")]
    //Passive stuff
    [SerializeField] private int MaxPassiveStacks;
    [SerializeField] private int PassiveBurnDmg;
    [SerializeField] private int PassiveBurnDurration;
    [SerializeField] private int PassiveDepreciateRate;
    private int CurrentPassiveCount;

    [HideInInspector] public Animator animator;
    private PlayerUI playerUI;
    

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

        playerUI = GameObject.FindWithTag("playerUI").GetComponent<PlayerUI>();
        foreach(GameObject i in playerUI.playerUI) if(i.name == "BulletCounter") i.transform.parent.gameObject.SetActive(false);

        SwordObj.GetComponent<KnightSword>().damage = SwordBaseDmg;
    }

    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused == true) return;

        if(!ShieldBroken)ShieldFunctionality(); // cast shield stuff

        if(!fireballOnCooldown && Input.GetKeyDown(KeyCode.Q)) CastFireBall();

        if(!smokeOnCooldown && Input.GetKeyDown(KeyCode.E)) CastSmoke();
        if(!ultOnCooldown && Input.GetKeyDown(KeyCode.R)) CastUlt();

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
            playerUI.qAbilityCoolDown = fireballCooldown;
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
        if(CurrentPassiveCount == 0) Invoke("DepreciatePassive", PassiveDepreciateRate);
        if(CurrentPassiveCount == MaxPassiveStacks) return;
        if(CurrentPassiveCount > MaxPassiveStacks) CurrentPassiveCount = MaxPassiveStacks;
        CurrentPassiveCount++;
    }

    private void DepreciatePassive()
    {
        if(CurrentPassiveCount <= 0) 
        {
            CancelInvoke("DepreciatePassive");
            if(CurrentPassiveCount < 0) CurrentPassiveCount = 0;
            return;
        }
        else
        {
            CurrentPassiveCount--;
            Invoke("DepreciatePassive", PassiveDepreciateRate);
        }
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
            playerUI.eAbilityCoolDown = smokeCooldown;
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

    private void CastUlt()
    {
        CmdCastKnightUlt();
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * dashStrength, ForceMode.Impulse);
    }
    
    [Command] 
    public void CmdCastKnightUlt()
    {
        GameObject ultFireClone = Instantiate(ultFireController, gameObject.transform.position, gameObject.transform.rotation);
        ultFireClone.GetComponent<KnightUlt>().PlayerThatSpawnedUlt = gameObject;        
        NetworkServer.Spawn(ultFireClone);
        Destroy(ultFireClone, KnightUltFireLeftBehindDurration);
        SwordObj.GetComponent<KnightSword>().damage = SwordBaseDmg + swordUltDmgIncrease;
    }
}
