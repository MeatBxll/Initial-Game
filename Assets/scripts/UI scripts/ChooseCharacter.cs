using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseCharacter : MonoBehaviour
{
    public TMP_Text[] CCTeamNames;

    [SerializeField] TMP_Text MainText;
    [SerializeField] private float prepClassTimer;
    private float prepClassTimerHoldTime;

    private void Start()
    {
        prepClassTimerHoldTime = prepClassTimer;
    }
    public void PrepareYourClass()
    {
        if(prepClassTimerHoldTime > 0)
        {
            if(prepClassTimerHoldTime == prepClassTimer) MainText.text = "Prepare Your Character";
            Invoke("PrepareYourClass",1);
            prepClassTimerHoldTime--;
        }
        else
        {
            prepClassTimerHoldTime = prepClassTimer;
        }
        


    }
}
