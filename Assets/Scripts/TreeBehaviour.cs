using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private Vector3 positionToGo;
    [SerializeField]
    private float lowTP = 5;
    private float positionToReady;
    [SerializeField]
    private float speedGrow = 0.0002f;
    [SerializeField]
    private float fixer;
    [SerializeField]
    private GameObject leavesToDisable;
    

    private void Start()
    {
        positionToGo = transform.position;
        positionToReady = positionToGo.y - fixer;
    }
    private void Update()
    {
        if (transform.position != positionToGo)
        { 
            transform.position = Vector3.Lerp(transform.position, positionToGo, speedGrow);
        }
        if (transform.position.y >= positionToReady)
        {
            gameObject.tag = "TreeTag";
            leavesToDisable.SetActive(true); 
        }
        else
        {
            gameObject.tag = "Untagged";
            leavesToDisable.SetActive(false);
        }
    }
    public void EatTree()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y-lowTP, transform.position.z);
        FindObjectOfType<SpawnTetris>().NewTetromino();
    }
}

