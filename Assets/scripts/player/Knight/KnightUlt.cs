using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightUlt : MonoBehaviour
{
    [SerializeField] private GameObject[] ultPathObjs;
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedSmoke;
    private int whichBall;
    private void Start()
    {
        //make ult objs destory faster and get deployed out of the knight with the dash
        Transform SpawnSpot = PlayerThatSpawnedSmoke.GetComponent<Knight>().FireBallSpawnLocation.transform;
        float smokeSpeed = PlayerThatSpawnedSmoke.GetComponent<Knight>().smokeSpeed;
        foreach(GameObject g in ultPathObjs)
        {
            g.gameObject.transform.position = gameObject.transform.position + (SpawnSpot.forward*3);
            g.gameObject.GetComponent<Rigidbody>().velocity = SpawnSpot.forward * smokeSpeed;
            g.GetComponent<KnightSmokeObj>().playerThatSpawnedSmoke = PlayerThatSpawnedSmoke;
        }
        Invoke("StopBall", .3f);
    }

    private void StopBall()
    {
        if(whichBall > ultPathObjs.Count() -1) 
        {
            Destroy(gameObject, PlayerThatSpawnedSmoke.GetComponent<Knight>().smokeDurration);
            return;
        }
        ultPathObjs[whichBall].GetComponent<Rigidbody>().velocity = Vector2.zero;
        whichBall ++;
        Invoke("StopBall", .3f); 
    }
}
