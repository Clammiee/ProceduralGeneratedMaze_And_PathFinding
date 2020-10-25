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
    GameObject newOBJ = null;
    List<Vector3> first = new List<Vector3>();
    List<Vector3> second = new List<Vector3>();
    List<Vector3> direction = new List<Vector3>();

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

                   if(this.gameObject.transform.position != hit.collider.gameObject.transform.position)
                   {
                       /* foreach (KeyValuePair<GameObject, Vector3> came in cameFrom)
                        {
                            first.Add(came.Key.transform.position);
                            second.Add(came.Value);
                        } */

                     //  foreach (Vector3 dir in direction)
                      // {
                            
                           //
                           Debug.Log(direction.Count);

                            foreach (Vector3 dir in direction)
                            {
                                Debug.Log(dir);
                                StartCoroutine(Hop(0.5f, dir));
                                if(this.gameObject.transform.position == hit.collider.gameObject.transform.position) break;
                            }
                           
                      // } 
                   } 
                   else if(this.gameObject.transform.position == hit.collider.gameObject.transform.position) Debug.Log("done movement");

                }
                else Debug.Log("Please select a valid point");
            }
        }
        }
    }

    private IEnumerator Hop(float wait, Vector3 goTo)
    {
        yield return new WaitForSeconds(wait);

        this.gameObject.transform.position = goTo;
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

               // }
                
                
                    for (int j = 0; j < direction.Count; j++)
                        {
                            for (int i = 0; i < first.Count; i++)
                            {
                                if(direction[j] == first[i]) direction.Add(second[i]);
                            }
                        }*/

                for (int i = 0; i < second.Count; i++)
                {
                    if(i == second.Count-1) direction.Add(first[i]);


                    //direction for loop
                   for (int j = 0; j < direction[0].GetComponent<Visited>().neighbors.Count; j++)
                   {
                       //GET neighbor with smallest distance from 000

                       if(second[i] == direction[0].GetComponent<Visited>().neighbors[j].transform.position)
                       {
                           direction.Add(second[i]);
                       }
                   }
                    
                }

                return;
            } 

            foreach(GameObject next in newOBJ.GetComponent<Visited>().neighbors)
            {
                if(next != null && cameFrom.ContainsKey(next) == false)
                {
                    frontier.Enqueue(next);
                    cameFrom.Add(next, newOBJ.transform.position);
                    //frontier.Remove(newOBJ);
                   // Debug.Log(frontier.Peek().transform.position);
                   first.Add(next.transform.position);
                   Debug.Log("first: " + next.transform.position);
                   second.Add(newOBJ.transform.position);
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
