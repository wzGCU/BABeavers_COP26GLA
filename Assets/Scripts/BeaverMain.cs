using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeaverMain : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    private bool colidingTree;
    private GameObject treeToKill;
    private bool modeTetris = false;
    [SerializeField]
    private Text modeTextObject;
    private bool inDen = false;
    private bool hiding = false;
    
    void Update()
    {
        if (Input.GetButtonDown("TetrisMode"))
        {
            modeTetris = !modeTetris;
            if (modeTetris)
            {
                modeTextObject.text = "ON";
            }
            else
            {
                modeTextObject.text = "OFF";
            }
        }
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        if (!hiding)
        {
            if (!modeTetris)
            {
                //Movement of Beaver
                float horizontalX = Input.GetAxis("Horizontal"); //Gets the horizontal positive/negative axis value
                float horizontalZ = Input.GetAxis("Vertical"); //Gets the vertical axis value
                Vector3 movement = new Vector3(horizontalX, 0, horizontalZ);
                transform.position += movement * Time.deltaTime * speed;
            }
            
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = true;
            treeToKill = col.gameObject;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = false;
            treeToKill = null;
        }
    }

    void Interact()
    {
        if (colidingTree)
        {
            colidingTree = false;
            treeToKill.GetComponent<TreeBehaviour>().EatTree();
            colidingTree = false;
            treeToKill = null;
        }
        if (hiding)
        {
            hiding = false;
            transform.position += new Vector3(0f, 0f, 20f);
        }
        if (inDen)
        {
            hiding = true;
            transform.position += new Vector3(0f,0f, -20f);
        }
    }

    public bool GetTetrisMode()
    {
        return modeTetris;
    }

    public void SetDen(bool value)
    {
        inDen = value;
    }
}
