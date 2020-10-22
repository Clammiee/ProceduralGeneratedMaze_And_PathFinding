using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Maze maze;
    private int count = 0;

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
                   PathFind(hit.collider.gameObject);

                  if(this.gameObject == hit.collider.gameObject) Debug.Log("done movement");
                }
                else Debug.Log("Please select a valid point");
            }
            }
        }
        
    }

    private void PathFind(GameObject go)
    {
        Vector3 dir = (go.transform.position - this.transform.position).normalized;
        
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, 2, dir, out hit, 5f))
        {
            if(hit.collider.CompareTag("Walkable"))
            {
                this.transform.position = hit.collider.transform.position;
                PathFind(hit.collider.gameObject);
                Debug.Log("do we get here");
            }

        }
    }
}
