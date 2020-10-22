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
                   Debug.Log("walkable");
                   Vector3 dir = (hit.collider.gameObject.transform.position - this.transform.position).normalized;
                   PathFind(hit.collider.gameObject, dir, maze.firstBlock.transform);

                  if(this.gameObject == hit.collider.gameObject) Debug.Log("done movement");
                }
                else Debug.Log("Please select a valid point");
            }
            }
        }
        
    }

    private void PathFind(GameObject go, Vector3 dir, Transform newTransform)
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
    }
}
