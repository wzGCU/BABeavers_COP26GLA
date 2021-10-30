using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetris : MonoBehaviour

{
    public GameObject[] Tetrominoes;

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, transform.rotation);
    }
}
