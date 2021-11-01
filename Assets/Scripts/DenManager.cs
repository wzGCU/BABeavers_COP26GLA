using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DenManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Text interact;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<BeaverMain>().SetDen(true);
            interact.text = "Hide in Den";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<BeaverMain>().SetDen(false);
            interact.text = "Interact";
        }

    }
}
