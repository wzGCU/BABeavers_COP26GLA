using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float spawnTime = 10f;
    private float previousTime;
    private float rightX = 42.5f;
    private float height = 7;
    private float lowPosition = 0f;
    private float highPosition = 30f;
    [SerializeField]
    private GameObject Fox;
    private GameObject player;
    private int stageToStart = 1;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - previousTime > spawnTime)
        {
            if (player.GetComponent<MainManager>().GetDenStage() >= 1)
            {
                Debug.Log("SpawnFox");
                SpawnFox();
            }

            previousTime = Time.time;
        }

    }
    void SpawnFox()
    {
        float randomHeight = Random.Range(lowPosition, highPosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, randomHeight);
        Instantiate(Fox, transform.position, Quaternion.identity);
    }
}
