using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightFirstPersonAnim : MonoBehaviour
{
[HideInInspector] public Knight knight;

    // swing stuff
    public void StopSwingAnimPov()
    {
        gameObject.GetComponent<Animator>().SetBool("IsSwinging" , false);
        gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", knight.SwingSpeed);
    }

    public void ReverseSwingAnimationPov()
    {
        if(Input.GetMouseButton(0)) gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", -knight.SwingSpeed);
    }

    public void ResetSwingPov()
    {
        if(!Input.GetMouseButton(0)) gameObject.GetComponent<Animator>().SetBool("IsSwinging" , false);
        gameObject.GetComponent<Animator>().SetFloat("SwingSpeed", knight.SwingSpeed);
    }

    //fireball stuff
    public void SpawnFireballPov()
    {
        knight.CmdCastFireBall(knight.gameObject.GetComponent<Health>().IsRedTeam);
    }
    public void StopFireBallAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Fireball" , false);
        
    }
    //smoke stuff
    public void StopSmokeAnimPov()
    {
        gameObject.GetComponent<Animator>().SetBool("Smoke" , false);

    }
    public void SpawnSmokePov()
    {
        knight.CmdCastSmoke();
    }

    //ult stuff
    public void StopKnightUltAnimPov()
    {

    }
}
