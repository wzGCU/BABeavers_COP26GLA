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
    [SerializeField]
    private Text interactText;
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
                interactText.text = "Rotate Block";
            }
            else
            {
                modeTextObject.text = "OFF";
                interactText.text = "Interact";
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

                Vector3 movementDir = new Vector3(horizontalX, 0, horizontalZ);
                movementDir.Normalize();
                transform.position += movementDir * Time.deltaTime * speed;
                if (movementDir != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDir, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720*Time.deltaTime);
                    //transform.forward = movementDir;
                }
                
            }
            
        }
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = true;
            treeToKill = col.gameObject;
            interactText.text = "Chop the Tree";
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = false;
            treeToKill = null;
            interactText.text = "Interact";
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
            interactText.text = "Interact";
        }
        if (hiding)
        {
            hiding = false;
            transform.position += new Vector3(0f, 0f, 20f);
            interactText.text = "Interact";
        }
        if (inDen)
        {
            hiding = true;
            transform.position += new Vector3(0f,0f, -20f);
            Invoke("UpdateText", 0.1f);
            
            
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
    void UpdateText()
    {
        interactText.text = "Leave the Den";
    }

   
}
