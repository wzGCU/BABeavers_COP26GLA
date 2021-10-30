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

    private GameObject waterObj;


    void Start()
    {
        waterObj = GameObject.FindWithTag("Water");
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - previousTime > timeToRise)
        {
            previousTime = Time.time;
            Debug.Log("rise water");
        }
    }
    public void RiseDen()
    {
        transform.position = transform.position + new Vector3(0, 1.0f, 0);
        stageDen++;
        Debug.Log("Den at stage: " + stageDen);
    }
    void RiseWater()
    {
        waterObj.transform.position = waterObj.transform.position + new Vector3(0, waterChangeHeight, 0);
        stageWater++;
        Debug.Log("water at stage: " + stageWater);
    }
}
