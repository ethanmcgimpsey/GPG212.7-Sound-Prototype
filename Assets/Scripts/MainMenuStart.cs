using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStart : MonoBehaviour
{
    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("GameScene");
    }
}
