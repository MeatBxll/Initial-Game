using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightShield : MonoBehaviour
{
    private bool IsRedTeam;
    public GameObject CharacterBody;

    private void Start()
    {
        IsRedTeam = CharacterBody.GetComponent<Health>().IsRedTeam;
    }

    private void TakeDamage(float dmg)
    {
        CharacterBody.GetComponent<Knight>().TakeDamage(dmg);
    }
}
