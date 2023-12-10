using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;


//Algorithm: Depth First Search
public class DungeonGenerator : MonoBehaviour
{
    //inforamtion of evry cell inside maze
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [Header("Maze Parameters")]
    [SerializeField] public Vector2 size;
    [SerializeField] public int startPosition = 0;
    List<Cell> board;
    public GameObject room;
    public Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room,new Vector3(i*offset.x,0,-j*offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);

                    newRoom.name += " " + i + "-" + j;
                }
                
            }
        }
    }

    //Generationg a maze
    void MazeGenerator()
    {
        board = new List<Cell>();

        for(int i = 0; i< size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPosition;

        Stack<int> path = new Stack<int>();

        int k = 0;
        // it could be replace with "while(true)" but it will crashed by unity
        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }

            //check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if(neighbors.Count == 0)
            {
                if(path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop(); //the last cell added in to path
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0,neighbors.Count)];

                if(newCell > currentCell)
                {
                    //down or right
                    if(newCell -1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    //Check the neighbors
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //check right neighbor
        if ((cell +1) % size.x != board.Count && !board[Mathf.FloorToInt(cell +1)].visited)  //because: cell % size.x = size.x - 1
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check down neighbor
        if (cell  % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)  //because: cell % size.x = size.x - 1
        {
            neighbors.Add(Mathf.FloorToInt(cell -1));
        }

        return neighbors;
    }
}
