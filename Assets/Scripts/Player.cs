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

    void Start()
    {
        
    }


    void Update()
    {
        if(maze.firstBlock != null)
        {
            if(count == 0)
            {
                this.transform.position = maze.firstBlock.transform.position;
                count2 = 0;
                count++;
            }
        }

        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
  	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   	    

   	        if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Walkable"))
                {

                   Debug.Log("walkable");


                   
                   

                   if(count2 == 0)
                    {
                        frontier.Enqueue(maze.firstBlock);
                        cameFrom.Add(maze.firstBlock, maze.firstBlock);

                      if(hit.collider.gameObject.GetComponent<Visited>().count > 0)  FindPath(hit.collider.gameObject, maze.firstBlock);
                        
                       count2++;
                    } 

                        Debug.Log("direction.Count: " + direction.Count);

                       if(done == true) StartCoroutine(Hop(0.5f));

                }
                else Debug.Log("Please select a valid point");
            }
        }
        
    }

    private IEnumerator Hop(float wait)
    {
      for (int i = direction.Count-1; i > -1; i--)
        {
            yield return new WaitForSeconds(wait);

            Debug.Log("direction[i]: " + direction[i].transform.position);
            if(direction[i].GetComponent<Visited>().playerVisited == false) this.gameObject.transform.position = direction[i].transform.position;
            direction[i].GetComponent<Visited>().playerVisited = true;
        }
    }

    private void Repeat(GameObject go, GameObject current, GameObject final, GameObject secondFinal)
    {
        int iteration = 0;
        GameObject secondFinalee = null;
        bool gotFinal = false;
        bool gotSecondFinal = false;

        Debug.Log("do we get here test");

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
        else if(secondFinal != null)
        {
            for (int i = 0; i < first.Count; i++)
            {
                if(secondFinal == first[i])
                {
                    if(second[i].GetComponent<Visited>() != null)
                        {
                            if(second[i].GetComponent<Visited>().neighbors != null && second[i].GetComponent<Visited>().neighbors.Count > 0 && second[i].GetComponent<Visited>().enabled == true)
                            {
                                foreach (GameObject neighbor in second[i].GetComponent<Visited>().neighbors)
                                {
                                    if(neighbor == final)
                                    {
                                        direction.Add(neighbor);    
                                        final = second[i];
                                        gotFinal = true;
                                    }
                                }
                            }
                        }
                }
            }

                        

                    
            
                if(final != null && final.GetComponent<Visited>() != null)
                {
                    int nieghborCount = 0;

                    if(final.GetComponent<Visited>().neighbors.Count > 0)
                    {
                        foreach (GameObject finalNeighbor in final.GetComponent<Visited>().neighbors)
                        {
                            for (int j = 0; j < first.Count; j++)
                            {
                                if(second[j] != finalNeighbor)
                                {
                                    secondFinalee = second[j];
                                    nieghborCount++;
                                } 
                            }
                        }
                        if(nieghborCount == final.GetComponent<Visited>().neighbors.Count && secondFinalee != null)
                        {
                            secondFinal = secondFinalee;
                            gotSecondFinal= true;
                        } 
                    }
                }

                if((direction[direction.Count-1].transform.position != upSide && direction[direction.Count-1].transform.position != rightSide) && gotSecondFinal == true && gotFinal == true) Repeat(go, current, final, secondFinal);
                else
                {
                    done = true;
                    return;
                } 
        }
    }

   
    private void FindPath(GameObject go, GameObject current) //
    {

        while(frontier.Count > 0)
        {

            newOBJ = frontier.Dequeue();

            if(newOBJ == go)
            {
              GameObject final = null;
              GameObject secondFinal = null;
              //GameObject secondPreFinal = null;
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

                    
                    
              
                if(secondFinal  != null) Debug.Log("secondFinal : " + secondFinal.transform.position);
                if((final != null && secondFinal != null) || (direction.Count == 2)) Repeat(go, current, final, secondFinal);

                break;
            } 

            foreach(GameObject next in newOBJ.GetComponent<Visited>().neighbors)
            {
                if(next != null && cameFrom.ContainsKey(next) == false)
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next, newOBJ);

                   first.Add(next);
                   Debug.Log("first: " + next.transform.position);
                   if(newOBJ != null) second.Add(newOBJ);
                   Debug.Log("second: " + newOBJ.transform.position);
                }
            }

            
        }
    }
}
