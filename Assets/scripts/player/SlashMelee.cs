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
    private int SwingState;

    private void Start() 
    {
        animator.SetFloat("SwingSpeed", SwingSpeed);
        AnimationClip[] AnimationClips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip c in AnimationClips) if(c.name == "KnightSwing") SwingClip = c;
        SwingState = 0;
    }
    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
        if (Input.GetMouseButton(0)) 
        {
            if(SwingState != 0) return;
            animator.SetFloat("Swing", 1);
            animator.SetFloat("Swing", 0);
            Invoke("EndSwing", SwingClip.length / SwingSpeed);
            SwingState = 2;
        }
    }

    //SwingState 0 means done swinging/not swinging
    //SwingState 1 means is currently normalswinging
    //SwingState 2 means transfering from not swinging to swinging or is backwardswinging
    public void EndSwing()
    {
        if (Input.GetMouseButton(0))
        {
            if(SwingState == 2)
            {
                animator.SetFloat("SwingSpeed", -SwingSpeed);
                animator.SetFloat("Swing", 1);
                animator.SetFloat("Swing", 0);
                Invoke("EndSwing", SwingClip.length / SwingSpeed);
                SwingState = 1;
            }
            else
            {
                animator.SetFloat("SwingSpeed", SwingSpeed);
                animator.SetFloat("Swing", 1);
                animator.SetFloat("Swing", 0);
                Invoke("EndSwing", SwingClip.length / SwingSpeed);
                SwingState = 2;
            }
        }
        else 
        {
            animator.SetFloat("Swing", 2);
            SwingState = 0;
        }
        
    }
}
