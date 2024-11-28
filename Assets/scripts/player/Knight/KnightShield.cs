using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightShield : MonoBehaviour
{
    public bool IsRedTeam;
    private void OnTriggerEnter(Collider obj) 
    {
        if(obj.tag == "Bullet")
        {
            if(obj.GetComponent<Health>().IsRedTeam == IsRedTeam) return;
        }
        Destroy(gameObject);
    }
}
