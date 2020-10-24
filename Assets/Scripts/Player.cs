using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Maze maze;
    private int count = 0;
    public List<Transform> distanceFromGoal = new List<Transform>();
    public List<Transform> allBlocks = new List<Transform>();
    public GameObject goTo;

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

                   Vector3 dir = (hit.collider.gameObject.transform.position - this.transform.position).normalized;
                   
                   FindPath(hit.collider.gameObject, maze.firstBlock); //FindPath(hit.collider.gameObject, maze.firstBlock);


                   if(this.gameObject == hit.collider.gameObject) Debug.Log("done movement");

                }
                else Debug.Log("Please select a valid point");
            }
            }
        }
    }

    private void FindPath(GameObject go, GameObject current) //
    {
        Transform[] allChildren = maze.gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform block in allChildren)
        {
            //allBlocks.add(block);

            foreach (Transform otherBlock in allChildren)
            {
                if(Vector3.Distance(otherBlock.position, go.transform.position) < Vector3.Distance(block.position, go.transform.position))
                {
                    distanceFromGoal.Add(otherBlock);
                   // allBlocks.Remove()
                } //else distanceFromGoal.Remove(otherBlock);
            }
        }

        Transform remove = null;
        foreach (Transform distanceBlock in distanceFromGoal)
        {
            if(distanceBlock == current.GetComponent<Visited>().newNeighborRight.transform || distanceBlock == current.GetComponent<Visited>().newNeighborLeft.transform || distanceBlock == current.GetComponent<Visited>().newNeighborUp.transform || distanceBlock == current.GetComponent<Visited>().newNeighborDown.transform)
            {
                this.gameObject.transform.position = distanceBlock.position;
                remove = distanceBlock;
            }
        }
        if(remove != null) distanceFromGoal.Remove(remove);

     //   if(go.transform.position != this.gameObject.position) FindPath(go, this.gameObject.position);
      //  else return;

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
