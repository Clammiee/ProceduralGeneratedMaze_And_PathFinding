﻿using System.Collections;
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

    public GameObject newNeighborRight;
    public GameObject newNeighborLeft;
    public GameObject newNeighborUp;
    public GameObject newNeighborDown;



    void Awake()
    {
        
    }

    void Start()
    {
        //walls[0] = true;
      //  walls[1] = true;
      //  walls[2] = true;
       // walls[3] = true;

       neighborRight = FindNeighbor(this.transform.right);
        neighborLeft = FindNeighbor(-this.transform.right);
        neighborUp = FindNeighbor(this.transform.forward);
        neighborDown = FindNeighbor(-this.transform.forward);
       
    }


    void Update()
    {
        if(this.gameObject.transform.parent.GetComponent<Maze>().count > 0)
        {
            newNeighborRight = FindNewNeighbor(this.transform.right);
            newNeighborLeft = FindNewNeighbor(-this.transform.right);
            newNeighborUp = FindNewNeighbor(this.transform.forward);
            newNeighborDown = FindNewNeighbor(-this.transform.forward);
        }
    }

    public GameObject FindNeighbor(Vector3 direction)
    {
        GameObject neighbor = null;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit, 3f))
        {
            neighbor = hit.collider.gameObject;
        }

        return neighbor;
    }

    public GameObject FindNewNeighbor(Vector3 direction)
    {
        GameObject neighbor = null;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit, 1f))
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
