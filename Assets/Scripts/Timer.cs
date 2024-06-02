using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timerValue;
    [SerializeField] float timeToCompleateQuestion= 10f;
    [SerializeField] float timeToShowCorrectAnswer=5f;

    public bool isAnsweringQuestion=false;
    public bool loadNewQuestion=false;
    public float fillFraction;
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        // if (timerValue <=0)
        // {
        // timerValue=timeToCompleateQuestion;
        // isAnsweringQuestion=true;
        // }else
        // {
        //     isAnsweringQuestion=false;
        // }

        // if (!isAnsweringQuestion)
        // {
        //     timerValue=timeToShowCorrectAnswer;
        // }
        // else
        // {
        //     isAnsweringQuestion=true;
        // }

        if (isAnsweringQuestion)
        {
            if(timerValue>0)
            {
                fillFraction = timerValue / timeToCompleateQuestion;
            }
            else
            {
                isAnsweringQuestion=false;
                timerValue=timeToShowCorrectAnswer;
            }
        }else
        {
            if(timerValue>0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion=true;
                timerValue=timeToCompleateQuestion;
                loadNewQuestion=true;               
            }
        }




        Debug.Log(timerValue);

    }

public void CancelTimer()
{
    timerValue=0;
}


}
