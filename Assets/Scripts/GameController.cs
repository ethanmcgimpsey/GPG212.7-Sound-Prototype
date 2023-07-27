using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<QuestionAndAnswer> questionAndAnswers = new List<QuestionAndAnswer>();
    public GameObject imageChoicePanelPrefab;
    public int questionCounter = 0;

    [SerializeField] private QuestionAndAnswer currentQuestion;

    void Start()
    {
        questionAndAnswers = questionAndAnswers.OrderBy(item => Random.Range(0f, 1f)).ToList(); //Shuffles the question list
        ResetRoom();
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

    public void OnAnswer(Texture2D selectedImage)
    {
        if (currentQuestion.answerImages.Contains(selectedImage))
        {
            StartCoroutine(ExitRoom());
        }
    }

    // Show image choices instead of text question
    private void ShowImageChoices()
    {
        currentQuestion = questionAndAnswers[questionCounter++];

        // Clear any existing image choice panels
        var existingPanels = GameObject.FindGameObjectsWithTag("ImageChoicePanel");
        foreach (var panel in existingPanels)
        {
            Destroy(panel);
        }

        // Find the parent container for image choices
        GameObject choicesContainerParent = GameObject.Find("ImageChoiceContainerParent");
        if (choicesContainerParent == null)
        {
            Debug.LogError("ImageChoiceContainerParent not found in the scene.");
            return;
        }

        // Create image choice container (empty game object)
        GameObject choicesContainer = new GameObject("ImageChoiceContainer");
        choicesContainer.transform.SetParent(choicesContainerParent.transform, false);

        // Create image choice panels for each answer option
        foreach (var answerImage in currentQuestion.answerImages)
        {
            GameObject choicePanel = Instantiate(imageChoicePanelPrefab);
            choicePanel.tag = "ImageChoicePanel";
            choicePanel.transform.SetParent(choicesContainer.transform, false);

            // Set the image of the Image component
            var image = choicePanel.GetComponent<Image>();
            Sprite sprite = Sprite.Create(answerImage, new Rect(0, 0, answerImage.width, answerImage.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;

            var button = choicePanel.GetComponent<Button>();
            button.onClick.AddListener(() => OnAnswer(answerImage));
        }
    }

    [System.Serializable]
    public class QuestionAndAnswer
    {
        public string question;
        public List<Texture2D> answerImages;
    }
}
