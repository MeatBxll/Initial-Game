using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Mirror;

public class KnightAnimator : NetworkBehaviour
{
    [HideInInspector] public Knight knight;

    // swing stuff
    public void StopSwingAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("IsSwinging" , false);
        gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", knight.SwingSpeed);
    }

    public void ReverseSwingAnimation()
    {
        if(Input.GetMouseButton(0)) gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", -knight.SwingSpeed);
    }

    public void ResetSwing()
    {
        if(!Input.GetMouseButton(0)) gameObject.GetComponent<Animator>().SetBool("IsSwinging" , false);
        gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", knight.SwingSpeed);
    }

    //fireball stuff
    public void SpawnFireball()
    {
        knight.CmdCastFireBall(knight.gameObject.GetComponent<Health>().IsRedTeam);
    }
    public void StopFireBallAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Fireball" , false);
        
    }
    //smoke stuff
    public void StopSmokeAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Smoke" , false);

    }
    public void SpawnSmoke()
    {
        knight.CmdCastSmoke(knight.gameObject.GetComponent<Health>().IsRedTeam);
    }

    //ult stuff
    public void StopKnightUltAnim()
    {

    }
    
}
