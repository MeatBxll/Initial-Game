using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUltFire : MonoBehaviour
{
    [HideInInspector] public GameObject playerThatSpawnedUltFire;
    private float[] passiveDmg;
    private float baseDmg;
    private bool IsRedTeam;
    void Start()
    {
        Knight g = playerThatSpawnedUltFire.GetComponent<Knight>();
        IsRedTeam = playerThatSpawnedUltFire.GetComponent<Health>().IsRedTeam;

        passiveDmg = new float[2];
        passiveDmg[0] = g.FireBallElements[1];
        passiveDmg[1] = g.FireBallElements[2];

        baseDmg = g.KnightUltFireLeftBehindBaseDmg;
    }

    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Player")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
            obj.GetComponent<Health>().TakeDmg(baseDmg);
            obj.GetComponent<Health>().TakeDmgOverTime(passiveDmg[0], passiveDmg[1], playerThatSpawnedUltFire);
            playerThatSpawnedUltFire.GetComponent<Knight>().IncreasePassiveCount();
        }
    }
}
