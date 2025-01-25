using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSmokeObj : MonoBehaviour
{
    [HideInInspector] public GameObject playerThatSpawnedSmoke;
    private float[] smokeElements;
    private bool IsRedTeam;

    private void Start()
    {
        IsRedTeam = playerThatSpawnedSmoke.GetComponent<Health>().IsRedTeam;
        smokeElements = playerThatSpawnedSmoke.GetComponent<Knight>().FireBallElements;
    }
    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Player")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
            obj.GetComponent<Health>().TakeDmgOverTime(smokeElements[1], smokeElements[2], playerThatSpawnedSmoke);
            playerThatSpawnedSmoke.GetComponent<Knight>().IncreasePassiveCount();
        }
    }
}
