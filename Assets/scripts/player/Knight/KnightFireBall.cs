using System.Collections;
using UnityEngine;

public class KnightFireBall : MonoBehaviour
{
    public bool IsRedTeam;
    [SerializeField] float TravelTimeBeforeDespawn;
    [SerializeField] float BaseDmg;
    private void Start() 
    {
        Destroy(gameObject, TravelTimeBeforeDespawn);
    }
    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Player")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
            obj.GetComponent<Health>().TakeDmg(BaseDmg);
        }
        Destroy(gameObject);
    }


}
