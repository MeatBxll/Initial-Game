using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSmoke : MonoBehaviour
{
    public bool IsRedTeam;
    [SerializeField] float SmokeDurration;
    [SerializeField] float BaseDmg;
    private void Start() 
    {
        Destroy(gameObject, SmokeDurration);
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
