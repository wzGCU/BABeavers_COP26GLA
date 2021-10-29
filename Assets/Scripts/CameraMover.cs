using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    // Update is called once per frame, Fixed makes it looks less laggy
    void FixedUpdate()
    {
        float horizontalX = Input.GetAxis("Horizontal"); //Gets the horizontal positive/negative axis value

        Vector3 movement = new Vector3(horizontalX, 0, 0);

        transform.position += movement * Time.deltaTime;
    }
}
