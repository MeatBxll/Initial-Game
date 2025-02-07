using Mirror;
using UnityEngine;

public class KnightUlt : NetworkBehaviour
{
    private GameObject ultPathObjs;
    private float ultBallWidth;
    [HideInInspector] public GameObject PlayerThatSpawnedUlt;
    private Vector3 lastSpawnedBallLocation;
    private void Start()
    {
        lastSpawnedBallLocation = gameObject.transform.position;
        ultPathObjs = PlayerThatSpawnedUlt.GetComponent<Knight>().ultFire;
        ultBallWidth = PlayerThatSpawnedUlt.GetComponent<Knight>().ultBallWidth;
    }

    private void FixedUpdate()
    {
        Vector3 g = PlayerThatSpawnedUlt.transform.position;
        if(g.x >= lastSpawnedBallLocation.x + ultBallWidth || g.y >= lastSpawnedBallLocation.y + ultBallWidth)
        {
            CmdSpawnKnightUltFire();
        }
    }
    [Command]
    void CmdSpawnKnightUltFire()
    {
        GameObject UltBallClone = Instantiate(ultPathObjs, PlayerThatSpawnedUlt.transform.position, PlayerThatSpawnedUlt.transform.rotation);
        lastSpawnedBallLocation = UltBallClone.transform.position;
        UltBallClone.GetComponent<KnightUltFire>().playerThatSpawnedUltFire = PlayerThatSpawnedUlt;
        UltBallClone.transform.SetParent(gameObject.transform);
        NetworkServer.Spawn(UltBallClone);
    }
}
