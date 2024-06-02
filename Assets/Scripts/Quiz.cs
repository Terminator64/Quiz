using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    public bool hasAnsweredEarly = true;
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;


    void Start()
    {
        timer=FindObjectOfType<Timer>();
        scoreKeeper=FindObjectOfType<ScoreKeeper>();
        // DisplayQuestion();
        scoreText.text="Score: 0%";
        progressBar.maxValue=questions.Count;
        progressBar.value=0;
    }

    void Update()
    {
        timerImage.fillAmount=timer.fillFraction;
        if (timer.loadNewQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNewQuestion=false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);

        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly=true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text="Score: "+scoreKeeper.CalculateScore()+"%";
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text=currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable=state;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count>0)
        {
        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        DisplayQuestion();
        progressBar.value++;
        scoreKeeper.IncrementQuestionsSeen();            
        }
        else
        {
            isComplete=true;
        }
        

    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
        questions.Remove(currentQuestion);
        }
        
    }

    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonImage=answerButtons[i].GetComponent<Image>();
            buttonImage.sprite=defaultAnswerSprite;
        }

    }


    void DisplayAnswer(int index) {
        {
        Image buttonImage;
        if(index==currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Thats Correct answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "Thats wrong answer! The correct answer is:\n"+currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        }
    }

} 
