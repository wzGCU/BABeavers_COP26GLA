using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockZ : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    private static Transform[,] grid = new Transform[width, height];
    private int logLinesCount = 0;
    private int logLinesRequired = 2;
    private int lineToDelete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0,1,0), 90);
            if (!CheckIfValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }


        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, 0, -1);
            if (!CheckIfValidMove())
            {
                transform.position -= new Vector3(0, 0, -1);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetris>().NewTetromino();
            }
            previousTime = Time.time;
        }
    }
    
    bool CheckIfValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedZ = Mathf.RoundToInt(children.transform.position.z);
            if (roundedX < 0 || roundedX >= width || roundedZ < 0 || roundedZ >= height)
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
                Debug.Log(logLinesCount);
                logLinesCount += 1;
                if(logLinesCount == 1)
                {
                    lineToDelete = i;
                }
                else if(logLinesCount >= logLinesRequired)
                {
                    logLinesCount = 0;
                    DeleteLine(lineToDelete);
                    DeleteLine(i);
                    RowDown(lineToDelete);
                    RowDown(i);
                    //build house of beaver
                }
                
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