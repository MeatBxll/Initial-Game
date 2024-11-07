using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool redTeamBullet;
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet") return;
        if (collision.gameObject.tag == "Player")
        {
            if(redTeamBullet && collision.gameObject.GetComponent<Health>().IsRedTeam) return;
            collision.gameObject.GetComponent<Health>().TakeDmg(damage);
        } 
        Destroy(gameObject,0f);
    }
}
