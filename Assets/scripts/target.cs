using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{

    [SerializeField] private Color red;
    [SerializeField] private Color normalColor;
    [SerializeField] private float turnBackDelay;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Target Hit");
            transform.GetComponent<Renderer>().material.color = red;
            Invoke("TurnBack", turnBackDelay);
        }
    }

    private void TurnBack()
    {
        transform.GetComponent<Renderer>().material.color = normalColor;
    }
}
