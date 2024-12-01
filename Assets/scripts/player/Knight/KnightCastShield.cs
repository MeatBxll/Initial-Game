using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class KnightCastShield : NetworkBehaviour
{
    public float ShieldMaxHealth;
    public float ShieldCurrentHealth;

    [SerializeField] private float RegenerationCooldown;
    [SerializeField] private float BrokenShieldReginCooldown;
    [SerializeField] private float ReginAmount;
    [SerializeField] private float ReginSpeed;
    private bool ShieldUp;
    private bool ShieldBroken;
    [SerializeField] GameObject ShieldObject;

    void Start()
    {
        ShieldCurrentHealth = ShieldMaxHealth;
    }

    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused == true) return;
        if(!ShieldBroken)ShieldFunctionality();
    }

    public void TakeDamage(float dmg)
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
}
