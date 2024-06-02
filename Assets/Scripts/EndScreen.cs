using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        scoreKeeper=FindAnyObjectByType<ScoreKeeper>();
    }


    public void ShowFinalScore()
    {
        finalScoreText.text="Congratulations!\nYou scored:"+scoreKeeper.CalculateScore()+"%";
    }


}
