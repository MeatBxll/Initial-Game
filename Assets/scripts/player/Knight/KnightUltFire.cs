using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightUltFire : MonoBehaviour
{
    [HideInInspector] public GameObject playerThatSpawnedUltFire;
    private float[] passiveDmg;
    private float baseDmg;
    void Start()
    {
        Knight g = playerThatSpawnedUltFire.GetComponent<Knight>();
        passiveDmg = new float[2];
        passiveDmg[0] = g.FireBallElements[1];
        passiveDmg[1] = g.FireBallElements[2];

        baseDmg = g.KnightUltFireLeftBehindBaseDmg;
    }
}
