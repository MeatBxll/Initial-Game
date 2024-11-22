using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashMeleeAnimator : MonoBehaviour
{
    public void EndSwing()
    {
        gameObject.GetComponent<Animator>().SetBool("IsSwinging", false);
    }
}
