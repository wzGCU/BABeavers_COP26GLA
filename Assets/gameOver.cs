using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            Debug.Log("print game)");
            SceneManager.LoadScene("MainGame");
        }
        if (Input.GetButtonDown("TetrisMode"))
        {
            Debug.Log("menu");
            SceneManager.LoadScene("MenuScene");
        }
    }
}
