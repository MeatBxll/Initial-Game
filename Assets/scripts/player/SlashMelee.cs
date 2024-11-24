using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class SlashMelee : NetworkBehaviour
{
    public Animator animator;
    [SerializeField] private float SwingSpeed;

    private AnimationClip SwingAnimationClip;

    private void Start() 
    {
        animator.SetFloat("SwingSpeed", SwingSpeed);
        SwingAnimationClip = animator.runtimeAnimatorController.animationClips[1];
    }
    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
        if (Input.GetMouseButton(0)) 
        {
            if(animator.GetBool("IsSwinging") == true) return;
            animator.SetBool("IsSwinging", true);
            Invoke("EndSwing", SwingAnimationClip.length * SwingSpeed);
        }
    }

    public void EndSwing()
    {
        animator.SetBool("IsSwinging", false);
    }
}
