using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class SlashMelee : NetworkBehaviour
{
    public Animator animator;
    [SerializeField] private float SwingSpeed;

    private void Start() 
    {
        animator.SetFloat("SwingSpeed", SwingSpeed);
    }
    void Update()
    {
        if(!isOwned) return;
        if(GameObject.FindGameObjectWithTag("playerUI").GetComponent<PlayerUI>().GamePaused) return;
        if (Input.GetMouseButton(0)) 
        {
            if(animator.GetBool("IsSwinging") == true) return;
            animator.SetBool("IsSwinging", true);
        }
    }
}
