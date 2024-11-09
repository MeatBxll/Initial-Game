using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float playerHealth;
    public GameObject myLobbyPlayer;
    public bool IsRedTeam;
    
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