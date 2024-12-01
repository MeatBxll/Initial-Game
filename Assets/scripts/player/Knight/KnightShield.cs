using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightShield : MonoBehaviour
{
    private bool IsRedTeam;
    [SerializeField] GameObject CharacterBody;

    private void Start()
    {
        IsRedTeam = CharacterBody.GetComponent<Health>().IsRedTeam;
    }

    private void TakeDamage(float dmg)
    {
        CharacterBody.GetComponent<KnightCastShield>().TakeDamage(dmg);
    }
}
