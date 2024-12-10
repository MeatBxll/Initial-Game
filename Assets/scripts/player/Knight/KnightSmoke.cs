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
    private bool onlyOnce;
    private void Start()
    {
        
        onlyOnce = false;
    }
    private void OnCollisionEnter(Collision obj) 
    {
        if(onlyOnce) return;
        if(obj.gameObject.tag != "Player")
        {
            onlyOnce = true;
            CmdSpawnSmoke();
        }
    }
    [Command]
    void CmdSpawnSmoke()
    {
        GameObject smokeClone = Instantiate(smokeObj, gameObject.transform.position, gameObject.transform.rotation);
        smokeClone.GetComponent<KnightSmoke>().IsRedTeam = IsRedTeam;
        smokeClone.GetComponent<KnightSmoke>().PlayerThatSpawnedFireBall = PlayerThatSpawnedFireBall;
        smokeClone.GetComponent<KnightSmoke>().smokeElements = smokeElements;

        NetworkServer.Spawn(smokeClone);
        Destroy(smokeClone, smokeElements[0]);
        Destroy(gameObject, smokeElements[0]+ 0.1f);
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
