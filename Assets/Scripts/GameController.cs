using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<QuestionAndAnswer> questionAndAnswers = new List<QuestionAndAnswer>();
    public TextMeshProUGUI questionText, livesText, timerText;
    public TMP_InputField answerField;
    public Transform player, playerStartPosition, playerQuestionPosition, playerEndPosition;
    public int questionCounter = 0;

    [SerializeField] private bool isCountingDown;
    [SerializeField] private QuestionAndAnswer currentQuestion;
    [SerializeField] private int lives;
    [SerializeField] private float countDownTimer;

    void Start()
    {
        questionAndAnswers = questionAndAnswers.OrderBy(item => Random.Range(0f, 1f)).ToList(); //Shuffles the question list
        ResetRoom();
        livesText.text = "Lives: " + lives;
    }

    void Update()
    {
        if (isCountingDown == true)
        {
            countDownTimer -= Time.deltaTime;
            DisplayTime(countDownTimer);
            if (countDownTimer <= 0)
            {
                SceneManager.LoadScene("GameOverMenu");
            }
        }
        else
        {
            return;
        }
    }

    // Reset everything in the room
    public void ResetRoom()
    {
        questionText.text = "";
        answerField.text = "";
        player.position = playerStartPosition.position;
        answerField.interactable = false;
        countDownTimer = 24f;
        DisplayTime(countDownTimer);
        StartCoroutine(EnterRoom());
    }

    // Display the timer ui text.
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public IEnumerator EnterRoom()
    {
        float timer = 0;
        while (timer < 2)
        {
            player.position = Vector3.Lerp(playerStartPosition.position, playerQuestionPosition.position, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        answerField.interactable = true;
        currentQuestion = questionAndAnswers[questionCounter++];
        questionText.text = currentQuestion.question;
        isCountingDown = true;
    }

    public IEnumerator ExitRoom()
    {
        answerField.interactable = false;
        float timer = 0;
        isCountingDown = false;
        while (timer < 2)
        {
            player.position = Vector3.Lerp(playerQuestionPosition.position, playerEndPosition.position, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        if (questionCounter>=9)
        {
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            ResetRoom();
        }
        
    }

    public void OnAnswer()
    {
        // DEBVUG LOG
        // if (answerField.text.ToLower() == currentQuestion.answer.ToLower())
        if(currentQuestion.answer.Contains(answerField.text.ToLower()))
        {
            StartCoroutine(ExitRoom());
        }
        else
        {
            lives -= 1;
        }
        livesText.text = "Lives: " + lives;
        if (lives == 0)
        {
            SceneManager.LoadScene("GameOverMenu");
        }
    }

    [System.Serializable]
    public class QuestionAndAnswer
    {
        public string question;
        public List < string > answer;
    }
}
