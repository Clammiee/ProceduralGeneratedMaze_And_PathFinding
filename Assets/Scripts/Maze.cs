using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int arraySizeX;

    public GameObject[,] array;

    [SerializeField] private GameObject goodBlock;

    [SerializeField] private GameObject badBlock;
    public GameObject firstBlock;
    private GameObject lastBlock;
    public Color c;
    private float timer = 0.1f;
    Vector3 direction;
    Vector3 newDirection;
    private List<GameObject> stack = new List<GameObject>();
     int positionInStack = 0;


        
    int number = 0; //0 for good and 1 for bad

    void Start()
    {
        array = new GameObject[arraySizeX, arraySizeX];
      //  stack = new GameObject[arraySizeX * arraySizeX -1];

        generateMaze();
      //  CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5))
       
    }


    void Update()
    {
       // CarvePassagesFrom(firstBlock.transform);

       timer = timer - Time.deltaTime;

       if(timer <= 0)
       {
         //  if(CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5)) == true)
          // {
              
              timer = 0.1f;
           //    timer = 0.1f;
                generate();
          // }
          // else Debug.Log("Done Maze creation"); 
       } 

    }

    private GameObject[,] generateMaze()
    {
        for (int i = 0; i < arraySizeX+1; i++)
        {
            for (int j = 0; j < arraySizeX+1; j++)
            {
                if((i == 0 && j == 0))
                {
                    firstBlock = InstantiateBlock(i, j, 0);
                    array[i, j] = firstBlock;
                } 
                else if(i == arraySizeX-1 && j == arraySizeX-1)
                {
                   array[i, j] = InstantiateBlock(i, j, 0);
                   lastBlock = array[i, j];
                }
                //else if(i == 0 || j == 0 || i == arraySizeX || j == arraySizeX) array[i, j] = InstantiateBlock(i, j, 1);
                else 
                { 
                    if(i < arraySizeX && j < arraySizeX) array[i, j] = InstantiateBlock(i, j, 1); 
                }

               // array[i, j] = InstantiateBlock(i, j, number);

               // InstantiateBlock(i, j, number);

                //if()
            }
        }
        // The initialization of the individual positions are later.

        

        // Only the first position is initialized and set at random

        int startX = Random.Range(0, arraySizeX-1);
        int startZ = Random.Range(0, arraySizeX-1);
        stack.Add(array[startX, startZ]);
        positionInStack = 0;

        // The first cel is set to 'visited'
        array[startX, startZ].GetComponent<Visited>().visited = true;
        array[startX, startZ].GetComponent<Renderer>().material.SetColor("_Color", c);
        array[startX, startZ].gameObject.tag = "Walkable";

      //  Debug.Log(stack.Count);

        return array;

        
    }

    private GameObject InstantiateBlock(int i, int j, int number)
    {
        GameObject randomBlock = null;

      //  int number = Random.Range(0, 2); //DOES NOT INCLUDE NUMBER 2

        if(number == 0) randomBlock = goodBlock;
        else randomBlock = badBlock;

        Vector3 pos = new Vector3(i, 0, j);
        GameObject block = Instantiate(randomBlock, pos, Quaternion.identity);
        return block;
    }

    private bool CarvePassagesFrom(Transform currentTransform, int randomNumber)
    {
        // FIRST TRY HERE ----------------------------------------------------------------------------------------------------------
        /*bool moreBlocks = false;
      //  int randomNumber = Random.Range(0, 5);

        if(randomNumber == 0) direction = currentTransform.forward;
        else if(randomNumber == 1) direction = -currentTransform.forward;
        else if(randomNumber == 3) direction = currentTransform.right;
        else if(randomNumber == 4) direction = -currentTransform.right;

        

        RaycastHit hit;

        if (Physics.Raycast(currentTransform.position, direction, out hit, 2f))
        {
        //    Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject == lastBlock.transform) moreBlocks = false;
             else moreBlocks = true;
            
            if(hit.collider.gameObject.GetComponent<Visited>().visited == false)
            {
                hit.collider.gameObject.GetComponent<Visited>().visited = true;
                hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
                hit.collider.gameObject.tag = "Walkable";
                CarvePassagesFrom(hit.collider.gameObject.transform, Random.Range(0, 5));
            }
            else if(hit.collider.gameObject.GetComponent<Visited>().visited == true)
            {
                if(direction == currentTransform.forward) newDirection = -currentTransform.forward;
                else if(direction == -currentTransform.forward) newDirection = currentTransform.forward;
                else if(direction == currentTransform.right) newDirection = -currentTransform.right;
                else if(direction == -currentTransform.right) newDirection = currentTransform.right;

                if (Physics.Raycast(currentTransform.position, newDirection, out hit, 2f))
                {
                    if(hit.collider.gameObject.GetComponent<Visited>().visited == false)
                    {
                        hit.collider.gameObject.GetComponent<Visited>().visited = true;
                        hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
                        hit.collider.gameObject.tag = "Walkable";
                        CarvePassagesFrom(hit.collider.gameObject.transform, Random.Range(0, 5));
                    }
                    else 
                    {
                        int number = Random.Range(0, 5);
                        if(number != randomNumber) CarvePassagesFrom(currentTransform, number);
                        else 
                        { 
                            number = Random.Range(0, 5);
                           // CarvePassagesFrom(currentTransform, number);

                        }
                        
                    }
                }
            }
        }
        else 
        {
            CarvePassagesFrom(currentTransform, Random.Range(0, 5));
        }
        return moreBlocks; */

        // FIRST TRY END HERE -----------------------------------------------------------------------------------------------------

        bool moreBlocks = false;

        if(currentTransform == lastBlock.transform) moreBlocks = false;
        else moreBlocks = true;
        
        if(randomNumber == 0) direction = currentTransform.forward;
        else if(randomNumber == 1) direction = -currentTransform.forward;
        else if(randomNumber == 3) direction = currentTransform.right;
        else if(randomNumber == 4) direction = -currentTransform.right;

        if(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction) != null && currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited == false)
        {
            currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited = true;
            currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Renderer>().material.SetColor("_Color", c);
            currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).tag = "Walkable";
            CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).transform, Random.Range(0, 5));
        }
        else 
        {
            CarvePassagesFrom(currentTransform, Random.Range(0, 5));
        }

        return moreBlocks;

    }

    private int[] CheckNeighbors(GameObject go)
    {
        Vector3 currentPosition = go.transform.position;

        List<int> result = new List<int>();

        int x = (int)currentPosition.x;
        int z = (int)currentPosition.z;

        // Check wall 0
        if (x - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x - 1, z].GetComponent<Visited>().visited == false) result.Add(0); // Add wall 0
        }

          // Check wall 5 
        if (x + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x + 1, z].GetComponent<Visited>().visited == false) result.Add(3); // Add wall 5
        }


        // Check wall 1 
       /* if (y - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x, z].GetComponent<Visited>().visited == false) result.Add(1); // Add wall 1
        }

        // Check wall 4 
        if (y + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x, z].GetComponent<Visited>().visited == false) result.Add(4); // Add wall 4
        } */


        // Check wall 2 
        if (z - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x, z - 1].GetComponent<Visited>().visited == false) result.Add(1); // Add wall 2
        }

        // Check wall 3
        if (z + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x, z + 1].GetComponent<Visited>().visited == false) result.Add(2); // Add wall 3
        }

        return result.ToArray();
    }

    private void generate()
    {
        while(positionInStack >= 0)
        {
            // The cell we have to check is saved in the stack at the current position
            int x = (int)stack[positionInStack].transform.position.x;
          //  int y = stack[positionInStack].transform.position.y;
            int z = (int)stack[positionInStack].transform.position.z;

            // Check the cells around (neighbours)
            int[] possibleNeighbors = CheckNeighbors(stack[positionInStack]);
            

            if(possibleNeighbors.Length > 0)
            {
                //Debug.Log(possibleNeighbors.Length);
                // Choose a random wall and open it to the next cell
                
                int wall = possibleNeighbors[Random.Range(0, possibleNeighbors.Length-1)];
               // Debug.Log("wall " + wall);

               // Debug.Log("x " + x + "z " + z);

                // Open the wall by setting the wall to false
                //array[x, z].GetComponent<Visited>().walls[wall] = false;
               
                // Get the new coordinates
                if (wall == 0)
                {
                    direction = -array[x, z].transform.forward;
                    x--;
                } 
                else if (wall == 3)
                {
                    direction = array[x, z].transform.forward;
                    x++;
                } 
               // else if (wall == 1) y--;
               // else if (wall == 4) y++;
                else if (wall == 1)
                {
                    direction = -array[x, z].transform.right;
                    z--;
                } 
                else if (wall == 2)
                {
                    direction = array[x, z].transform.right;
                    z++;
                } 

                // Set the new cell to visitied and open the wall on the opposite side of the previous cell
                // and increase the stack by one and set the new position
                positionInStack++;

               // Debug.Log("New x " + x);
             //  Debug.Log("New z " + z);

                stack.Add(array[x, z]);
               // Debug.Log("positionInStack " + positionInStack);
                //.GetComponent<Visited>().MazePosition(x, z);

                /*if(array[x, z].GetComponent<Visited>().FindNeighbor(direction) != null && array[x, z].GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited == false)
                {
                  
                // currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited = true;
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Renderer>().material.SetColor("_Color", c);
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).tag = "Walkable";
                //  CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).transform, Random.Range(0, 5));

                    array[x, z].GetComponent<Visited>().visited = true;
                    //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
                    array[x, z].GetComponent<Renderer>().material.SetColor("_Color", c);
                    array[x, z].gameObject.tag = "Walkable";
                }*/
                Recursion(array[x, z], direction);
             }
            else positionInStack--;
        }
    }

    private void Recursion(GameObject go, Vector3 direction)
    {
        if(go.GetComponent<Visited>().visited == false)
        {
            go.GetComponent<Visited>().visited = true;
                    //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
            go.GetComponent<Renderer>().material.SetColor("_Color", c);
            go.gameObject.tag = "Walkable";
        }
        else Recursion(go, -direction);
       /* if(go.GetComponent<Visited>().FindNeighbor(direction) != null)
                {
                  GameObject newGo = go.GetComponent<Visited>().FindNeighbor(direction);

                  if(newGo != null && newGo.GetComponent<Visited>().visited == false)
                  {
                      // currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited = true;
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Renderer>().material.SetColor("_Color", c);
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).tag = "Walkable";
                //  CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).transform, Random.Range(0, 5));

                    newGo.GetComponent<Visited>().GetComponent<Visited>().visited = true;
                    //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
                     newGo.GetComponent<Visited>().GetComponent<Renderer>().material.SetColor("_Color", c);
                     newGo.GetComponent<Visited>().gameObject.tag = "Walkable";
                  }
                  e
                
                }*/
    }
}

