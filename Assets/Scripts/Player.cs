using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Maze maze;
    private int count = 0;
    public List<Transform> distanceFromGoal = new List<Transform>();
    public List<Transform> distanceFromCurrent = new List<Transform>();
    public List<Transform> allBlocks = new List<Transform>();
    public GameObject goTo;

    Queue<GameObject> frontier = new Queue<GameObject>();
    Dictionary <GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();
    private int count2 = 0;
    private int count3 = 0;
    GameObject newOBJ = null;
    GameObject newDir = null;
    List<GameObject> first = new List<GameObject>();
    List<GameObject> second = new List<GameObject>();
    List<GameObject> direction = new List<GameObject>();
    List<float> distance = new List<float>();
    int n = 0;
    int m = 0;
    Queue<GameObject> frontierTemp = new Queue<GameObject>();
    Vector3 upSide = new Vector3(0, 0, 1);
    Vector3 rightSide = new Vector3(1, 0, 0);
    GameObject finalee = null;
    bool done = false;

    void Update()
    {
        //As long as the maze has a first block set our position to be the same as this first block (and do it only once)
        if(maze.firstBlock != null)
        {
            if(count == 0)
            {
                this.transform.position = maze.firstBlock.transform.position;
                count2 = 0;
                count++;
            }
        }

        //if the player clicks on a green block
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	        if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Walkable"))
                {
                   if(count2 == 0) //do this once...
                    {
                        //initial these two things for the FindPath function
                        frontier.Enqueue(maze.firstBlock); 
                        cameFrom.Add(maze.firstBlock, maze.firstBlock);

                        //if we already calculated the neighbors of the block we hit via Visited.cs
                        if(hit.collider.gameObject.GetComponent<Visited>().count > 0) FindPath(hit.collider.gameObject, maze.firstBlock); //find path from our current pos to the pos of the block we clicked/ hit with our mouse button
                        
                       count2++;
                    } 
                    if(done == true) StartCoroutine(Hop(0.5f)); //if we are done finding path than start Hop/ moving the player towards the destination
                }
                else Debug.Log("Please select a valid point"); //if we didnt hit a green walkable block than print this
            }
        }
        
    }

    //called in Update, at every new iteration every 0.5f sec we make this pos == the direction[i] pos
    private IEnumerator Hop(float wait)
    {
      for (int i = direction.Count-1; i > -1; i--)
        {
            yield return new WaitForSeconds(wait);
            if(direction[i].GetComponent<Visited>().playerVisited == false) this.gameObject.transform.position = direction[i].transform.position;
            direction[i].GetComponent<Visited>().playerVisited = true;
        }
    }

    //called in FindPath, adds directions from destination to start in direction list, in reversed order, so that we can hop from start to finish
    private void Repeat(GameObject go, GameObject current, GameObject final, GameObject secondFinal)
    {
        int iteration = 0;

        if(secondFinal == null)
        {
            for (int i = 0; i < first.Count; i++)
            {
                if(first[i] == direction[direction.Count-1])
                {
                    direction.Add(first[i]);
                    final = first[i];
                    iteration = i;
                    int nieghborCount = 0;

                    if(final != null)
                    {
                        foreach (GameObject finalNeighbor in final.GetComponent<Visited>().neighbors)
                        {
                            if(second[iteration] != finalNeighbor) nieghborCount++;
                        }
                        if(nieghborCount == final.GetComponent<Visited>().neighbors.Count) secondFinal = second[iteration];
                        else 
                        {
                            direction.Add(second[iteration]);
                            if(final != null && (direction[direction.Count-1].transform.position != upSide && direction[direction.Count-1].transform.position != rightSide)) Repeat(go, current, final, secondFinal);
                            else
                            {   
                                done = true;
                                return;
                            } 
                        }
                    }
                } 
            }
        }
    }

   
    private void FindPath(GameObject go, GameObject current) //Find path from current to go
    {
        while(frontier.Count > 0)
        {
            newOBJ = frontier.Dequeue();

            if(newOBJ == go) //if newOBJ is our destination block
            {
                GameObject final = null;
                GameObject secondFinal = null;
                int iteration = 0;

                for (int i = 0; i < first.Count; i++)
                {
                    if(first[i] == go)
                    {
                        direction.Add(first[i]);
                        final = first[i];
                        iteration = i;
                    } 
                }
                int nieghborCount = 0;
                if(final != null)
                {
                    foreach (GameObject finalNeighbor in final.GetComponent<Visited>().neighbors)
                    {
                        if(second[iteration] != finalNeighbor) nieghborCount++;
                    }
                    if(nieghborCount == final.GetComponent<Visited>().neighbors.Count) secondFinal = second[iteration];
                    else 
                    {
                        direction.Add(second[iteration]);
                    }
                }
                if((final != null && secondFinal != null) || (direction.Count == 2)) Repeat(go, current, final, secondFinal); //recursively add all blocks on path to direction array, in reversed order

                break;
            } 

            foreach(GameObject next in newOBJ.GetComponent<Visited>().neighbors)
            {
                if(next != null && cameFrom.ContainsKey(next) == false)
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next, newOBJ);

                   first.Add(next);
                   if(newOBJ != null) second.Add(newOBJ);
                }
            }
        }
    }
}
