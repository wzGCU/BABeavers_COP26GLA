using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaverAnim : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        { anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

       if(Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Chop");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("Move");
        }

    }
}
