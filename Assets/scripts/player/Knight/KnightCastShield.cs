using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class KnightCastShield : NetworkBehaviour
{
    public float ShieldMaxHealth;
    public float ShiledCurrentHealth;

    [SerializeField] private float RegenerationCooldown;
    [SerializeField] private float RegenAmount;
    [SerializeField] private float ReginSpeed;
    private bool ShieldUp;
    private bool CoolDownStarted;

    void Start()
    {
        ShiledCurrentHealth = ShieldMaxHealth;
    }

    void Update()
    {
        //when button is held shield is up when its not it goes down


        //when shield is down shield health regenerates 
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused == true) return;
        if(Input.GetMouseButton(0))
        {
            float g = RegenerationCooldown;
        }
        else
        {
            
        }
    }

    public void TakeDamage(float dmg)
    {
        ShiledCurrentHealth -= dmg;
    }

    private void PutUpShield()
    {
        
    }

    private void RegenerateShieldHealth()
    {
        if(!ShieldUp)
        {
            Invoke("RegenerateShieldHealth", ReginSpeed);
        }
    }
}
