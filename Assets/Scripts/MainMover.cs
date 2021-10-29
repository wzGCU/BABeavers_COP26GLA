using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMover : MonoBehaviour
{
    // Update is called once per frame, Fixed makes it looks less laggy
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal"); //Gets the horizontal positive/negative axis value
        float vertical = Input.GetAxis("Vertical"); //Gets the vertical axis value

        Vector3 movement = new Vector3(horizontal, vertical);

        transform.position += movement * Time.deltaTime;
    }
}
