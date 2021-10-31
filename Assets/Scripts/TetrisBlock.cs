using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    private float fallTime = 1.5f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    private GameObject player;
    private bool touchingPlayer;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            //Debug.Log("Plaer touch");
            touchingPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Plaer no touch");
            touchingPlayer = false;        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LeftMovement") && CheckIfPlayerPossible()) 
        {
            transform.position += new Vector3(-1, 0, 0);
            player.transform.position += new Vector3(-1, 0, 0);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
                player.transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetButtonDown("RightMovement") && CheckIfPlayerPossible())
        {
            transform.position += new Vector3(1, 0, 0);
            player.transform.position += new Vector3(1, 0, 0);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
                player.transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetButtonDown("Interact") && CheckIfPlayerPossible())
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,1,0), 90);
            if (!CheckIfValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 1, 0), -90);
            }
        }
        else if (Input.GetButtonDown("DownMovement") && CheckIfPlayerPossible())
        {
            transform.position += new Vector3(0, 0, -1); //changed from Y -1 to Z -1
            player.transform.position += new Vector3(0, 0, -1);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(0, 0, -1);
                player.transform.position -= new Vector3(0, 0, -1);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
            }
        }
        if (Time.time - previousTime > fallTime && !touchingPlayer)
        {
            transform.position += new Vector3(0, 0, -1); //changed from Y -1 to Z -1
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(0, 0, -1);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
            }
            previousTime = Time.time;
        }
        
    }
    
    bool CheckIfPlayerPossible()
    {
        if (touchingPlayer && player.GetComponent<BeaverMain>().GetTetrisMode())
        {
            return true;
        }
        else
        return false;
    }

    bool CheckIfValidMove()
    {
        
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);
            if (roundedX < 0 || roundedX >= width || roundedZ < 0 || roundedZ >= height)  //changed Z values to Y values
            {
                return false;
            }
            if(grid[roundedX,roundedZ] != null)
            {
                return false;
            }
            
        }
        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);
            grid[roundedX, roundedZ] = children;
        }
    }

    void CheckForLines()
    {
        for (int i = height-1; i>= 0; i--)
        {
            if(HasLine(i))
            {
                    DeleteLine(i);
                    RowDown(i);
                    player.GetComponent<MainManager>().RiseDen();//build house of beaver

            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if (grid[j,i] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Debug.Log(grid[j, i].gameObject);
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j=0; j<width; j++)
            {
                if(grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 0, 1);
                }
            }
        }
    }
}
