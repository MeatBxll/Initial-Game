using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Mirror;
using System.Linq;

public class KnightSmoke : NetworkBehaviour
{
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedFireBall;
    [HideInInspector] public float[] smokeElements;
    [HideInInspector] public float smokeSpeed;
    [SerializeField] private GameObject[] smokeObjs;
    private int whichBall;
    private void Start()
    {
        foreach(GameObject g in smokeObjs)
        {
            g.gameObject.GetComponent<Rigidbody>().velocity = PlayerThatSpawnedFireBall.GetComponent<Knight>().FireBallSpawnLocation.transform.forward * smokeSpeed;
        }
        Invoke("StopBall", .3f);
    }

    private void StopBall()
    {
        if(whichBall > smokeObjs.Count() -1) return;
        smokeObjs[whichBall].GetComponent<Rigidbody>().velocity = Vector2.zero;
        whichBall ++;
        Invoke("StopBall", .3f);
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
