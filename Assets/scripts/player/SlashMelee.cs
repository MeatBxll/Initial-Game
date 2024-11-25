using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class SlashMelee : NetworkBehaviour
{
    public Animator animator;
    [SerializeField] private float SwingSpeed;
    private AnimationClip SwingClip;

    private void Start() 
    {
        animator.SetFloat("SwingSpeed", SwingSpeed);
        AnimationClip[] AnimationClips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip c in AnimationClips) if(c.name == "KnightSwing") SwingClip = c;
    }
    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
        if (Input.GetMouseButton(0)) 
        {
            if(animator.GetBool("IsSwinging") == true) return;
            animator.SetBool("IsSwinging", true);
            Invoke("EndSwing", SwingClip.length / SwingSpeed);
        }
    }

    public void EndSwing()
    {
        animator.SetBool("IsSwinging", false);
    }
}
