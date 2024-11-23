using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class slashMeleeAnimator : NetworkBehaviour
{
    public void EndSwing()
    {
        gameObject.GetComponent<Animator>().SetBool("IsSwinging", false);
    }
}
