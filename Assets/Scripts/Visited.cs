using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visited : MonoBehaviour
{
    public bool visited = false;
    public List<bool> walls = new List<bool>(); 

    public GameObject neighborRight;
    public GameObject neighborLeft;
    public GameObject neighborUp;
    public GameObject neighborDown;

    void Start()
    {
        //walls[0] = true;
      //  walls[1] = true;
      //  walls[2] = true;
       // walls[3] = true;
    }


    void Update()
    {
        neighborRight = FindNeighbor(this.transform.right);
        neighborLeft = FindNeighbor(-this.transform.right);
        neighborUp = FindNeighbor(this.transform.up);
        neighborDown = FindNeighbor(-this.transform.up);
    }

    public GameObject FindNeighbor(Vector3 direction)
    {
        GameObject neighbor = null;
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, 0.5f, direction, out hit, 1f))
        {
            neighbor = hit.collider.gameObject;
        }

        return neighbor;
    }

    public int x, z;

    public GameObject MazePosition(int posX, int posZ)
    {
        x = posX;
       // y = posY;
        z = posZ;
        
        return this.gameObject;
    }
}

//public class MazePosition
//{
    
//}
