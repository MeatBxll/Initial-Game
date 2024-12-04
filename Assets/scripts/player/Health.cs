using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Steamworks;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [HideInInspector] public float playerHealth;
    [HideInInspector] public GameObject myLobbyPlayer;
    [HideInInspector] public bool IsRedTeam;
    public float maxHealth;


     //taking dmg over time
    private List<float> OverTimeDmgs;
    private List<int> OverTimeDurrations;
    private List<GameObject> DmgSources;
    private bool IsTakingDmgOverTime;
    [SerializeField] private float TimeBetweenDmgTicks;
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

    public void TakeDmgOverTime(float dmgPerTick, float numberOfTicks, GameObject dmgSource)
    {
        //converts number of dmg ticks to a int
        numberOfTicks = Mathf.Round(numberOfTicks * 10.0f) * 0.1f;
        int tickNumb = (int) numberOfTicks;

        //checks to see if youre already taking dmg over time from that source and if so just replaces the durration and dmg of that
        for (int i = 0; i < DmgSources.Count; i++)
        {
            if(DmgSources[i] == dmgSource)
            {
                OverTimeDmgs[i] = dmgPerTick;
                OverTimeDurrations[i] = tickNumb;
                return;
            }
        }
        DmgSources.Add(dmgSource);
        OverTimeDmgs.Add(dmgPerTick);
        OverTimeDurrations.Add(tickNumb);
        if(!IsTakingDmgOverTime)
        {
            IsTakingDmgOverTime = true;
            DealDmgOverTime();
        }
        


    }

    private void DealDmgOverTime()
    {
        if(IsTakingDmgOverTime == false) return;
        if(DmgSources.Count < OverTimeDurrations.Count) Debug.Log("something went wrong");
        for(int i = 0; i < DmgSources.Count; i++)
        {
            if(OverTimeDurrations[i] !<= 0)
            {
                OverTimeDurrations[i]--;
                TakeDmg(OverTimeDmgs[i]);
            }
            else
            {
                DmgSources.Remove(DmgSources[i]);
                OverTimeDmgs.Remove(OverTimeDmgs[i]);
                OverTimeDurrations.Remove(OverTimeDurrations[i]);
            }

        }

        if(DmgSources.Count <= 0)
        {
            IsTakingDmgOverTime = false;
            OverTimeDmgs.Clear();
            OverTimeDurrations.Clear();
        }

        Invoke("DealDmgOverTime", TimeBetweenDmgTicks);
        
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