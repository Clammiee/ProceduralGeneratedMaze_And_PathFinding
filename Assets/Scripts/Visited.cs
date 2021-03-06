﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visited : MonoBehaviour //attached to every block object
{
    public bool visited = false;
    public bool playerVisited = false;

    public GameObject neighborRight;
    public GameObject neighborLeft;
    public GameObject neighborUp;
    public GameObject neighborDown;

    public GameObject newNeighborRight;
    public GameObject newNeighborLeft;
    public GameObject newNeighborUp;
    public GameObject newNeighborDown;
    public List<GameObject> neighbors = new List<GameObject>();
    [HideInInspector] public int count = 0;

    void Start()
    {
        neighborRight = FindNeighbor(this.transform.right);
        neighborLeft = FindNeighbor(-this.transform.right);
        neighborUp = FindNeighbor(this.transform.forward);
        neighborDown = FindNeighbor(-this.transform.forward);
    }   


    void Update()
    {
        if(this.gameObject.transform.parent.GetComponent<Maze>().count > 0 && this.gameObject.transform.parent.GetComponent<Maze>().count2 > 0)
        {
            newNeighborRight = FindNewNeighbor(this.transform.right);
            newNeighborLeft = FindNewNeighbor(-this.transform.right);
            newNeighborUp = FindNewNeighbor(this.transform.forward);
            newNeighborDown = FindNewNeighbor(-this.transform.forward);

            if(count == 0)
            {
                if(newNeighborRight != null) neighbors.Add(newNeighborRight);
                if(newNeighborLeft != null) neighbors.Add(newNeighborLeft);
                if(newNeighborUp != null) neighbors.Add(newNeighborUp);
                if(newNeighborDown != null) neighbors.Add(newNeighborDown);

                count++;
            }
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

}

