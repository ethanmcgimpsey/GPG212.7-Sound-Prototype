using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<Question> questionAndAnswers = new List<Question>();
    public int questionCounter = 0;
    public Button[] currentButton;

    [SerializeField] private Question currentQuestion;

    void Start()
    {
        ResetRoom();
    }

    void Update()
    {

    }

    // Reset everything in the room
    public void ResetRoom()
    {
        StartCoroutine(EnterRoom());
    }

    public IEnumerator EnterRoom()
    {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        currentQuestion = questionAndAnswers[questionCounter++];
    }

    public IEnumerator ExitRoom()
    {
        float timer = 0;
        while (timer < 2)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        if (questionCounter >= 9)
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
        if (currentQuestion.answer = currentButton[questionCounter])
        {
            StartCoroutine(ExitRoom());
        }
    }

    [System.Serializable]
    public class Question
    {
        public GameObject currentQuestion;
        public Button answer;
    }
}