using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Mirror;
using System.Linq;

public class KnightSmoke : NetworkBehaviour
{
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedSmoke;
    [SerializeField] private GameObject[] smokeObjs;
    private int whichBall;
    private void Start()
    {
        Transform SpawnSpot = PlayerThatSpawnedSmoke.GetComponent<Knight>().FireBallSpawnLocation.transform;
        float smokeSpeed = PlayerThatSpawnedSmoke.GetComponent<Knight>().smokeSpeed;
        foreach(GameObject g in smokeObjs)
        {
            g.gameObject.transform.position = gameObject.transform.position + (SpawnSpot.forward*3);
            g.gameObject.GetComponent<Rigidbody>().velocity = SpawnSpot.forward * smokeSpeed;
            g.GetComponent<KnightSmokeObj>().playerThatSpawnedSmoke = PlayerThatSpawnedSmoke;
        }
        Invoke("StopBall", .3f);
    }

    private void StopBall()
    {
        if(whichBall > smokeObjs.Count() -1) 
        {
            Destroy(gameObject, PlayerThatSpawnedSmoke.GetComponent<Knight>().smokeDurration);
            return;
        }
        smokeObjs[whichBall].GetComponent<Rigidbody>().velocity = Vector2.zero;
        whichBall ++;
        Invoke("StopBall", .3f);
    }

    
}
