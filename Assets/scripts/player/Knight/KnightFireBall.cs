using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class KnightFireBall : MonoBehaviour
{
    [HideInInspector] public bool IsRedTeam;
    [HideInInspector] public GameObject PlayerThatSpawnedFireBall;

    [HideInInspector] public float[] FireBallElements;// [0] is base dmg [1] is burndmgPerTick [2] is burnDurration

    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Player")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
            obj.GetComponent<Health>().TakeDmg(FireBallElements[0]);
            obj.GetComponent<Health>().TakeDmgOverTime(FireBallElements[1], FireBallElements[2]);
            PlayerThatSpawnedFireBall.GetComponent<Knight>().IncreasePassiveCount();
        }
        Destroy(gameObject);
    }
}
