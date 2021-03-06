using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private float waterChangeHeight = 1.0f;
    [SerializeField]
    private float timeToRise = 15.0f;
    private float previousTime;

    private int stageDen = 0;
    private int stageWater = 0;

    //[SerializeField]
    private GameObject waterObj;
    [SerializeField]
    private GameObject spawnerTetris;
   // [SerializeField]
   // private GameObject staleWater;
    [SerializeField]
    private GameObject[] buildObjects;

    private GameObject[] tetrises;


    void Start()
    {
        waterObj = GameObject.FindWithTag("Water");
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - previousTime > timeToRise)
        {
            previousTime = Time.time;
            Debug.Log("rise water");
            RiseWater();
        }
    }
    public void RiseDen()
    {
        stageDen++;
        Debug.Log("Den at stage: " + stageDen);
        if (stageDen == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameWin");
        }
        else
        {
            buildObjects[stageDen - 1].SetActive(true);
        }

    }
    void RiseWater()
    {
        
        if (stageDen != 3)
        {
            waterObj.transform.position += new Vector3(0, waterChangeHeight, 0);
            spawnerTetris.transform.position += new Vector3(0, waterChangeHeight, 0);
            // staleWater.transform.position += new Vector3(0, waterChangeHeight, 0);
            stageWater++;
        }
        
        if (stageWater == 13)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("gameOver");
        }
        Debug.Log("water at stage: " + stageWater);
    }
    public int GetDenStage()
    {
        return stageDen;
    }
}
