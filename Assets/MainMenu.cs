using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("playgame");
        SceneManager.LoadScene("Intro");
    }

    public void GameQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
