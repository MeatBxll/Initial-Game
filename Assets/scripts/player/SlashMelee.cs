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
    private bool NormalSwinging;

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
            NormalSwinging = true;
        }
    }

    public void EndSwing()
    {
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) 
        {
            animator.SetBool("IsSwinging", false);
            return;
        }
        
        if (Input.GetMouseButton(0)) 
        {
            if(NormalSwinging)
            {
                animator.SetFloat("SwingSpeed", -SwingSpeed);
                NormalSwinging = false;
                Invoke("EndSwing", SwingClip.length / SwingSpeed);
            }
            else
            {
                animator.SetFloat("SwingSpeed", SwingSpeed);
                NormalSwinging = true;
                Invoke("EndSwing", SwingClip.length / SwingSpeed);
            }
        }
        
    }
}
