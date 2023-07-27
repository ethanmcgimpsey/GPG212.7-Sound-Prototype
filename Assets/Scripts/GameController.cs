using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Levels;

    public AudioSource correctAudioSource;
    public AudioSource incorrectAudioSource;

    [SerializeField] private int currentLevel;
    [SerializeField] private int lives;

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void CorrectAnswer()
    {
        correctAudioSource.Play();
        if (currentLevel + 1 != Levels.Length)
        {
            Levels[currentLevel].SetActive(false);

            currentLevel++;
            Levels[currentLevel].SetActive(true);
        }
        else
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(2);
        }
    }

    public void WrongAnswer()
    {
        incorrectAudioSource.Play();
        lives -= 1;
        Levels[currentLevel].SetActive(false);
        currentLevel++;
        Levels[currentLevel].SetActive(true);
        if (lives == 0)
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(3);
        }
    }
}