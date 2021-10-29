using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverMain : MonoBehaviour
{
    int logCount = 0; //stores value of logs beaver has
    // Start is called before the first frame update
    void Start()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButton("Interact") && (Collision.gameObject.tag == TreeTag))
        {
            Debug.Log("Colliding with tree");
        }*/
    }

    public int GetLogCount()
    {
        return logCount;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "TreeTag")
        {
            Debug.Log("Tree");
        }

    }
    void CollectLog(GameObject tree)
    {
       // logCount += 1;
        //tree.AddDamage;
    }
}
