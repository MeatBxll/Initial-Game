using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.Basic;
public class KnightSword : NetworkBehaviour
{
    [HideInInspector] public float damage;
    
    private void OnTriggerEnter(Collider g) 
    {
        if(gameObject.GetComponentInParent<Knight>().animator.GetBool("IsSwinging") == false) return;
        if (g.gameObject.tag == "Player")
        {
            if(gameObject.GetComponentInParent<Health>().IsRedTeam = g.GetComponent<Health>().IsRedTeam) return;
            g.gameObject.GetComponent<Health>().TakeDmg(damage);
        } 
    }
}
