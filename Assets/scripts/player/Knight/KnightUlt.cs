using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class KnightUlt : NetworkBehaviour
{
    [HideInInspector] public GameObject ultPathObjs;
    [HideInInspector] public float ultBallWidth;
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedUlt;
    private Vector3 lastSpawnedBallLocation;
    private void Start()
    {
        lastSpawnedBallLocation = gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 g = PlayerThatSpawnedUlt.transform.position;
        if(g.x >= lastSpawnedBallLocation.x + ultBallWidth || g.y >= lastSpawnedBallLocation.y + ultBallWidth)
        {
            CmdSpawnKnightUltBall();
        }
    }
    [Command]
    void CmdSpawnKnightUltBall()
    {
        GameObject UltBallClone = Instantiate(ultPathObjs, PlayerThatSpawnedUlt.transform.position, PlayerThatSpawnedUlt.transform.rotation);
        lastSpawnedBallLocation = UltBallClone.transform.position;
        UltBallClone.GetComponent<KnightUltFire>().playerThatSpawnedUltFire = PlayerThatSpawnedUlt;
        UltBallClone.transform.SetParent(gameObject.transform);
        NetworkServer.Spawn(UltBallClone);
    }
}
