using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviour : MonoBehaviour
{
    private bool directionRight = false;
    private float axisChange = -1f;
    private Rigidbody rb;
    [SerializeField]
    private float speedModifier = 1.0f;
    private float speed;
    private GameObject player;
    enum States
    {
        Patrol,
        BeaverSeen,
        Exit
    }
    private States currentState;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        speed = speedModifier * 0.2f;
        if (directionRight)
        {
            axisChange = 1.0f;
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (currentState == States.Patrol)
        {
            transform.rotation = new Quaternion(0, -0.707106829f, 0, 0.707106829f);
            transform.position += new Vector3(speed*axisChange, 0, 0) ;
            if(transform.position.x <= -43)
            {
                Destroy(gameObject);
            }
        }
        if(currentState == States.BeaverSeen)
        {

            Vector3 beaverPosition = player.transform.position;
            float singleStep = speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, beaverPosition - transform.position, 10000f, 1000f));
            transform.position += Vector3.Normalize(beaverPosition - transform.position) * speed;
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            Debug.Log("ChasingPlayer");
            currentState = States.BeaverSeen;
            speed *= 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("LeavingPlayer");
            currentState = States.Patrol;
            speed /= 2;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == player)
        {
            Debug.Log("Caught");
            UnityEngine.SceneManagement.SceneManager.LoadScene("govER");
        }
        if(collision.gameObject.tag == "TreeTag")
        {
            transform.position += new Vector3(0.1f, 0, 0.1f); ;
        }
        
    }
}
