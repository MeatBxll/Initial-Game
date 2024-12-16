using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using Mirror.Examples.Basic;

public class SlashMelee : NetworkBehaviour
{
    [HideInInspector] public Animator animator;
    [SerializeField] private float SwingSpeed;
    private AnimationClip SwingClip;
    private bool NormalSwinging;
    [HideInInspector] public bool IsShielding;

    private void Start() 
    {
        animator = gameObject.GetComponent<player>().animator;
        animator.SetFloat("SwingSpeed", SwingSpeed);
    }
    void FixedUpdate()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
        if (Input.GetMouseButton(0) && !IsShielding) 
        {
            if(animator.GetBool("IsSwinging") == true) return;
            animator.SetBool("IsSwinging", true);
        }
        
        if(IsShielding) animator.SetBool("IsSwinging", false);
    }
}
