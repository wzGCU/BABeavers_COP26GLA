using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private Vector3 positionToGo;
    private float positionToReady;
    [SerializeField]
    private float speedGrow = 0.0002f;
    [SerializeField]
    private float fixer;
    [SerializeField]
    private Material notReady;
    [SerializeField]
    private Material ready;

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
            gameObject.GetComponent<MeshRenderer>().material = ready;
        }
        else
        {
            gameObject.tag = "Untagged";
            gameObject.GetComponent<MeshRenderer>().material = notReady;
        }
    }
    public void EatTree()
    {
        
        transform.position = new Vector3(transform.position.x, transform.position.y-5, transform.position.z);
        FindObjectOfType<SpawnTetris>().NewTetromino();
    }
}

