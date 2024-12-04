using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public float playerHealth;
    [HideInInspector] public GameObject myLobbyPlayer;
    [HideInInspector] public bool IsRedTeam;

    //taking dmg over time
    private List<float> OverTimeDmgs;
    private List<int> OverTimeDurrations;
    private bool IsTakingDmgOverTime;
    
    public float maxHealth;
    void Start()
    {
        playerHealth = maxHealth;
        GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().PlayerHealthVisuals(maxHealth, maxHealth);
        myLobbyPlayer.GetComponent<LobbyPlayer>().PlayerHealthBar(maxHealth, maxHealth);
    }

    public void TakeDmg(float dmg)
    {
        playerHealth -= dmg;
        
        if(playerHealth <= 0) myLobbyPlayer.GetComponent<LobbyPlayer>().KillPlayer();
        else 
        {
            GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().PlayerHealthVisuals(playerHealth, maxHealth);
            myLobbyPlayer.GetComponent<LobbyPlayer>().PlayerHealthBar(playerHealth, maxHealth);
            CancelInvoke("DealDmgOverTime");

        }
    }

    public void TakeDmgOverTime(float dmgPerTick, float numberOfTicks)
    {
        //need to find a way to burn the player over a certain amount of time while passing in these variables
        numberOfTicks = Mathf.Round(numberOfTicks * 10.0f) * 0.1f;
        

        
    }

    private void DealDmgOverTime()
    {


        if(playerHealth <= 0) myLobbyPlayer.GetComponent<LobbyPlayer>().KillPlayer();
        else 
        {
            GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().PlayerHealthVisuals(playerHealth, maxHealth);
            myLobbyPlayer.GetComponent<LobbyPlayer>().PlayerHealthBar(playerHealth, maxHealth);
            CancelInvoke("DealDmgOverTime");

        }
    }

    public void Respawn(Transform g)
    {
        transform.position = new Vector3(g.position.x, g.position.y + 3f, g.position.z);
        playerHealth = maxHealth;
        GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().PlayerHealthVisuals(maxHealth, maxHealth);
        myLobbyPlayer.GetComponent<LobbyPlayer>().PlayerHealthBar(maxHealth, maxHealth);
    }

}