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
    Dictionary <GameObject, Vector3> cameFrom = new Dictionary<GameObject, Vector3>();
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
   	    
   	        // Casts the ray and get the first game object hit
   	        if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.CompareTag("Walkable"))
                {
                    //call function with input hit.collider.gameObject
                   // PathFind(hit.collider.gameObject);
                   Debug.Log("walkable");

                   //Vector3 dir = (hit.collider.gameObject.transform.position - this.transform.position).normalized;
                   
                   

                   if(count2 == 0)
                    {
                        frontier.Enqueue(maze.firstBlock);
                        cameFrom.Add(maze.firstBlock, Vector3.zero);
                        //could be in other function

                       

                        FindPath(hit.collider.gameObject, maze.firstBlock);
                        

                       count2++;
                    } //FindPath(hit.collider.gameObject, maze.firstBlock);

                       // for (int i = direction.Count-1; i > -1; i--)
                      //  {
                            //Debug.Log("direction[i]: " + direction[i].transform.position);
                           /// StartCoroutine(Hop(0.5f, direction[i].transform.position));
                       // }

                       /* if(direction.Count > 0)
                        {
                            if(count2 > 0 && count3 == 0)
                            {
                                n = (direction.Count-1);
                                count3++;
                            } 
                                if(n >= 0)
                            
                                {
                                    Debug.Log("direction.Count: " + direction.Count);
                                    Debug.Log("direction: " + direction[n].transform.position);
                                    StartCoroutine(Hop(0.5f, direction[n].transform.position));
                                    
                                } else return;
                        } */
                        Debug.Log("direction.Count: " + direction.Count);
                        StartCoroutine(Hop(0.5f));

                   if(this.gameObject.transform.position != hit.collider.gameObject.transform.position)
                   {
                       /* foreach (KeyValuePair<GameObject, Vector3> came in cameFrom)
                        {
                            first.Add(came.Key.transform.position);
                            second.Add(came.Value);
                        } */

                     //  foreach (Vector3 dir in direction)
                      // {
                            
                               //if(this.gameObject.transform.position == hit.collider.gameObject.transform.position) break;
                         // }

                          //  foreach (GameObject dir in direction)
                           // {
                        
                        
                            
                   } 
                   else if(this.gameObject.transform.position == hit.collider.gameObject.transform.position) Debug.Log("done movement");

                }
                else Debug.Log("Please select a valid point");
            }
        }
        
    }

    private IEnumerator Hop(float wait)
    {
        
        
      //  for (int i = direction.Count-1; i > -1; i--)
      for (int i = 0; i < direction.Count; i++)
        {
            yield return new WaitForSeconds(wait);

            Debug.Log("direction[i]: " + direction[i].transform.position);
            if(direction[i].GetComponent<Visited>().playerVisited == false) this.gameObject.transform.position = direction[i].transform.position;
            direction[i].GetComponent<Visited>().playerVisited = true;
            

           // StartCoroutine(Hop(0.5f, direction[i].transform.position));
        }
       // this.gameObject.transform.position = goTo;
      //  for (int i = 0; i < dir.Count; i++)
        //{
           // n--;
       // }
    
    }

    private void Repeat(GameObject go, GameObject current)
    {
      /*  GameObject prev = null;
        float distanceBetween =  Vector3.Distance(current.transform.position, go.transform.position);

        for (int i = 0; i < first.Count; i++)
        {
            for (int j = 0; j < second.Count; j++)
            {
                if(first[i].transform.position == second[j].transform.position)
                {
                    prev = first[i];

                    direction.Add(prev);
                } 
            }
        } */
       // direction.Add(go);
      /* frontierTemp.Enqueue(current); //go?????


        while(frontierTemp.Count > 0)
        {
            
            newDir = frontierTemp.Dequeue();

            if(newDir == go) break; //current???
            
            if(newDir != null)
            {

                direction.Add(newDir);
        
            

                
                   // for (int k = 0; k < direction.Count; k++)
                  //  {
                        //loop thru all neighrbos to get each of their distance from 000
                        
                            
                            distance.Clear();

                          //  Debug.Log("dir nieghbors count: " + dir.GetComponent<Visited>().neighbors.Count);

                          //  List<GameObject> neighbors = ;
                          for (int k = 0; k < newDir.GetComponent<Visited>().neighbors.Count; k++)
                          {
                                float d = Vector3.Distance(go.transform.position, newDir.GetComponent<Visited>().neighbors[k].transform.position); //// current distance??
                                //if(d == 0) return;
                                distance.Add(d);
                          }

                            //foreach (GameObject neighbor in )
                         //   {
                                //if(neighbor != null)
                               // {
                                    
                               // } 
                          //  }
                                //GET neighbor with smallest distance from 000
                                Vector3 nextNeighbor = Vector3.zero;
                                float min = 0;
                                int iteration = 0;

                                for (int l = 0; l < distance.Count; l++)
                                {
                                    if(l == 0)
                                    {
                                        min = distance[l];
                                        iteration = l;
                                    }

                                    if(distance[l] < min)
                                    {
                                        min = distance[l];
                                        iteration = l;
                                    } 
                                }
                                
                                for (int j = 0; j < newDir.gameObject.GetComponent<Visited>().neighbors.Count; j++)
                                {
                                // if(dir.gameObject.GetComponent<Visited>().neighbors[j] != null) Debug.Log(dir.gameObject.GetComponent<Visited>().neighbors[j].transform.position);

                                    if(iteration == j && newDir.gameObject.GetComponent<Visited>().playerVisited == false)
                                    {
                                        nextNeighbor = newDir.gameObject.GetComponent<Visited>().neighbors[j].transform.position;
                                        newDir.gameObject.GetComponent<Visited>().playerVisited = true;
                                    } 
                                    
                                         
                                       // else if(nextNeighbor == current.transform.position) return;
                                }

                                for (int i = 0; i < second.Count; i++)
                                {
                                    if(second[i].transform.position == nextNeighbor && nextNeighbor != current.transform.position)
                                        {
                                           // direction.Add(second[i]);
                                          //  m++;
                                           // Repeat(go, current, direction[m]);
                                           frontierTemp.Enqueue(second[i]);
                                          
                                        } 
                                   
                                }
                            }
        }
        return; */
    }

    //Needs to be a coroutine
    private void FindPath(GameObject go, GameObject current) //
    {
      //  Debug.Log(frontier.Peek().transform.position);
            //Start hoping

    
                
        while(frontier.Count > 0)
        {
            

            newOBJ = frontier.Dequeue();



            if(newOBJ == go)
            {
              //  for (int i = first.Count-1; i > -1; i--)
              //  {
                   // for (int j = 0; j < second.Count; j++)
                   // {
                       // if(first[i] == second[j]) direction.Add(first[j]);
                   // }
                //   direction.Add(first[first.Count-2]);
                /*  direction.Add(second[first.Count-2]);

               // } /
                 
                
                    for (int j = 0; j < direction.Count; j++)
                        {
                            for (int i = 0; i < first.Count; i++)
                            {
                                if(direction[j] == first[i]) direction.Add(second[i]);
                            }
                        }*/

               // for (int i = 0; i < second.Count; i++)
               // {
                  //  if(second[i] == go) direction.Add(go);
               // }
                //direction.Add(go);
              //  m = 0;
               //
              // frontierTemp.Enqueue(go);
                Repeat(go, current);

                break;
            } 

            foreach(GameObject next in newOBJ.GetComponent<Visited>().neighbors)
            {
                if(next != null && cameFrom.ContainsKey(next) == false)
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next, newOBJ.transform.position);
                    //frontier.Remove(newOBJ);
                   // Debug.Log(frontier.Peek().transform.position);
                   first.Add(next);
                   Debug.Log("first: " + next.transform.position);
                   second.Add(newOBJ);
                   Debug.Log("second: " + newOBJ.transform.position);
                }
            }

            
        }

        
         

        


       /* if(Vector3.Distance(current.GetComponent<Visited>().newNeighborDown.transform.position, go.transform.position) < Vector3.Distance(current.GetComponent<Visited>().newNeighborUp.transform.position, go.transform.position) && Vector3.Distance(current.GetComponent<Visited>().newNeighborDown.transform.position, go.transform.position) < Vector3.Distance(current.GetComponent<Visited>().newNeighborLeft.transform.position, go.transform.position) && Vector3.Distance(current.GetComponent<Visited>().newNeighborDown.transform.position, go.transform.position) < Vector3.Distance(current.GetComponent<Visited>().newNeighborRight.transform.position, go.transform.position))
        {
            this.transform.position = current.GetComponent<Visited>().newNeighborDown.transform.position;
            FindPath(go, current.GetComponent<Visited>().newNeighborDown);
        }*/
       /* yield return new WaitForSeconds(waitTime);

        if(current.GetComponent<Visited>().newNeighborUp != null && (current.GetComponent<Visited>().newNeighborDown == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborUp.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborDown.transform.position)) && (current.GetComponent<Visited>().newNeighborLeft == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborUp.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborLeft.transform.position)) && (current.GetComponent<Visited>().newNeighborRight == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborUp.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborRight.transform.position)))
        {
            this.transform.position = current.GetComponent<Visited>().newNeighborUp.transform.position;
            StartCoroutine(FindPath(go, current.GetComponent<Visited>().newNeighborUp, 0.5f));
        }
        else if(current.GetComponent<Visited>().newNeighborRight != null && (current.GetComponent<Visited>().newNeighborDown == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborRight.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborDown.transform.position)) && (current.GetComponent<Visited>().newNeighborLeft == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborRight.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborLeft.transform.position)) && (current.GetComponent<Visited>().newNeighborUp == null || Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborRight.transform.position) < Vector3.Distance(go.transform.position, current.GetComponent<Visited>().newNeighborUp.transform.position)))
        {
            this.transform.position = current.GetComponent<Visited>().newNeighborRight.transform.position;
            StartCoroutine(FindPath(go, current.GetComponent<Visited>().newNeighborRight, 0.5f));
        } */
       /* Transform[] allChildren = maze.gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform block in allChildren)
        {
            allBlocks.Add(block);

            foreach (Transform otherBlock in allBlocks)
            {
                if(Vector3.Distance(block.position, go.transform.position) < Vector3.Distance(otherBlock.position, go.transform.position))
                {
                    distanceFromGoal.Add(block);
                   // allBlocks.Remove()
                } //else distanceFromGoal.Add(otherBlock);
            }
        }

        for (int i = 0; i < distanceFromGoal.Count; i++)
        {
                if(Vector3.Distance(distanceFromGoal[i].position, current.transform.position) < Vector3.Distance(distanceFromGoal[j].position, current.transform.position))
                {
                    distanceFromCurrent.Add(distanceFromGoal[i]);
                } //else distanceFromCurrent.Add(distanceFromGoal[i+1]);
        }
       // foreach(Transform distanceFromGoalTransform in distanceFromGoal)
       // {
            
        //}
        //take distancefrom goal and order them according to distance from current

        Transform remove = null;
        //foreach (Transform distanceBlock in distanceFromGoal)
        //{
            //instead if using distanceFromGoal[0] use distanceFromCurrent[0] 
            if((current.GetComponent<Visited>().newNeighborRight != null && distanceFromCurrent[0] == current.GetComponent<Visited>().newNeighborRight.transform) || (current.GetComponent<Visited>().newNeighborLeft != null && distanceFromCurrent[0] == current.GetComponent<Visited>().newNeighborLeft.transform) || (current.GetComponent<Visited>().newNeighborUp != null && distanceFromCurrent[0] == current.GetComponent<Visited>().newNeighborUp.transform) || (current.GetComponent<Visited>().newNeighborDown !=null && distanceFromCurrent[0] == current.GetComponent<Visited>().newNeighborDown.transform))
            {
                this.gameObject.transform.position = distanceFromCurrent[0].position;
                remove = distanceFromCurrent[0];
            }
       // }
        if(remove != null) distanceFromCurrent.Remove(remove);*/

       // if(go.transform.position == this.gameObject.transform.position) return; 
        return;

    }

    /*private void PathFind(GameObject go, Vector3 dir, Transform newTransform)
    {
        RaycastHit hit;

        if (Physics.SphereCast(newTransform.position, 0.5f, dir, out hit, 2f))
        {
            if(hit.collider.CompareTag("Walkable"))
            {
                this.transform.position = hit.collider.transform.position;
                PathFind(hit.collider.gameObject, dir, hit.collider.transform);
                Debug.Log("do we get here");
            }
            else
            {
                int randomNumber = Random.Range(0, 5);

                Vector3 direction = Vector3.zero;

                if(randomNumber == 0) direction = transform.forward;
                else if(randomNumber == 1) direction = -transform.forward;
                else if(randomNumber == 3) direction = transform.right;
                else if(randomNumber == 4) direction = -transform.right;

                if(direction != dir) PathFind(go, direction, newTransform);
                else PathFind(go, -direction, newTransform);
            }
        }
    }*/
}
