using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimator : MonoBehaviour
{

    public void StopSwingAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("IsSwinging" , false);

    }

    public void StopFireBallAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Fireball" , false);
        
    }
    public void StopSmokeAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("Smoke" , false);

    }
    public void StopKnightUltAnim()
    {

    }
    
}
