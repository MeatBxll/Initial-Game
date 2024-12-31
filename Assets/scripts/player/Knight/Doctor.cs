using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : MonoBehaviour
{
    [HideInInspector] public float Timeholder;
    void Update()
    {
        if(Time.deltaTime!= Timeholder)
            {
                callback();
            }
    }
    public void callback()
    {
        Debug.Log(Timeholder);
    }
}
