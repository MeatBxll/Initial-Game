using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public bool redTeamBullet;
    [SerializeField] private float headshotMultiplier;
    [SerializeField] private float damage;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet") return;
        if (collision.gameObject.tag == "Player")
        {
            if(redTeamBullet == collision.gameObject.GetComponent<Health>().IsRedTeam) return;
            if(collision.gameObject.tag == "PlayerHead") collision.gameObject.GetComponent<Health>().TakeDmg(damage * headshotMultiplier);
        } 
        Destroy(gameObject,0f);
    }
}
