using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Mirror;

public class KnightSmoke : NetworkBehaviour
{
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedFireBall;
    [HideInInspector] public float[] smokeElements;
    [SerializeField] private GameObject smokeObj;
    private void OnCollisionEnter(Collision obj) 
    {
        if(obj.gameObject.tag != "Player")
        {
            CmdSpawnSmoke();
        }
    }
    [Command]
    void CmdSpawnSmoke()
    {
        GameObject smokeSpawnerClone = Instantiate(smokeObj, gameObject.transform.position, gameObject.transform.rotation);
        smokeSpawnerClone.GetComponent<KnightSmoke>().IsRedTeam = IsRedTeam;
        smokeSpawnerClone.GetComponent<KnightSmoke>().PlayerThatSpawnedFireBall = PlayerThatSpawnedFireBall;
        smokeSpawnerClone.GetComponent<KnightSmoke>().smokeElements = smokeElements;

        NetworkServer.Spawn(smokeSpawnerClone);
        Destroy(smokeSpawnerClone, smokeElements[0]);
    }

    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Player")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
            obj.GetComponent<Health>().TakeDmgOverTime(smokeElements[1], smokeElements[2], PlayerThatSpawnedFireBall);
        }
    }
}
