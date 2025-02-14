using UnityEngine;

public class KnightUlt : MonoBehaviour
{
    private float ultBallWidth;
    [HideInInspector] public GameObject PlayerThatSpawnedUlt;
    private Vector3 lastSpawnedBallLocation;
    private void Start()
    {
        lastSpawnedBallLocation = gameObject.transform.position;
        ultBallWidth = PlayerThatSpawnedUlt.GetComponent<Knight>().ultBallWidth;
        Destroy(gameObject, PlayerThatSpawnedUlt.GetComponent<Knight>().dashDurration);
    }

    private void FixedUpdate()
    {
        Vector3 g = PlayerThatSpawnedUlt.transform.position;
        if(g.x >= lastSpawnedBallLocation.x + ultBallWidth || g.z >= lastSpawnedBallLocation.z + ultBallWidth)
        {
            PlayerThatSpawnedUlt.GetComponent<Knight>().CmdSpawnKnightUltFire();
            lastSpawnedBallLocation = PlayerThatSpawnedUlt.GetComponent<Knight>().knightFeet.transform.position;
        }
    }
    
}
