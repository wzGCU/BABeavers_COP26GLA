using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverMain : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    private bool colidingTree;
    private GameObject treeToKill;
    private bool touchingTetris;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        //Movement of Beaver
        float horizontalX = Input.GetAxis("Horizontal"); //Gets the horizontal positive/negative axis value
        float horizontalZ = Input.GetAxis("Vertical"); //Gets the vertical axis value
        Vector3 movement = new Vector3(horizontalX, 0, horizontalZ);
        transform.position += movement * Time.deltaTime * speed;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = true;
            treeToKill = col.gameObject;
        }
        if(col.gameObject.tag == "Tetris")
        {
            touchingTetris = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            colidingTree = false;
            treeToKill = null;
        }
        if (col.gameObject.tag == "Tetris")
        {
            touchingTetris = false;
        }
    }

    void Interact()
    {
        if (colidingTree)
        {
            colidingTree = false;
            Destroy(treeToKill);
            colidingTree = false;
            treeToKill = null;
            FindObjectOfType<SpawnTetris>().NewTetromino();

        }
    }
    public bool GetTouchingTetris()
    {
        return touchingTetris;
    }
}
