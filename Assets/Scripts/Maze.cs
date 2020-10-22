using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int arraySizeX;

    public GameObject[,] array;

    [SerializeField] private GameObject goodBlock;

    [SerializeField] private GameObject badBlock;
    public GameObject firstBlock;
    private GameObject lastBlock;
    public Color c;
    private float timer = 0.1f;
    Vector3 direction;
    Vector3 newDirection;


        
    int number = 0; //0 for good and 1 for bad

    void Start()
    {
        array = new GameObject[arraySizeX, arraySizeX];

        generateMaze();
      //  CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5));
    }


    void Update()
    {
       // CarvePassagesFrom(firstBlock.transform);

       timer = timer - Time.deltaTime;

       if(timer <= 0)
       {
           if(CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5)) == true)
           {
               timer = 0.1f;
           }
           else Debug.Log("Done Maze creation"); 
       } 

    }

    private GameObject[,] generateMaze()
    {
        for (int i = 0; i < arraySizeX; i++)
        {
            for (int j = 0; j < arraySizeX; j++)
            {
                if((i == 0 && j == 0))
                {
                    firstBlock = InstantiateBlock(i, j, 0);
                    array[i, j] = firstBlock;
                } 
                else if(i == arraySizeX-1 && j == arraySizeX-1)
                {
                   array[i, j] = InstantiateBlock(i, j, 0);
                   lastBlock = array[i, j];
                }
                //Good block for 0,0 && another good block for [arraySizeX, arraySizeX]
               /* if((i == 0 && j == 0) || (i == arraySizeX && j == arraySizeX))
                {
                    firstBlock = InstantiateBlock(i, j, 0);
                    array[i, j] = firstBlock;
                } */
                else if(i == 0 || j == 0 || i == arraySizeX-1 || j == arraySizeX-1) array[i, j] = InstantiateBlock(i, j, 1);
                else { array[i, j] = InstantiateBlock(i, j, 1); }

               // array[i, j] = InstantiateBlock(i, j, number);

               // InstantiateBlock(i, j, number);

                //if()
            }
        }
        return array;
    }

    private GameObject InstantiateBlock(int i, int j, int number)
    {
        GameObject randomBlock = null;

      //  int number = Random.Range(0, 2); //DOES NOT INCLUDE NUMBER 2

        if(number == 0) randomBlock = goodBlock;
        else randomBlock = badBlock;

        Vector3 pos = new Vector3(i, 0, j);
        GameObject block = Instantiate(randomBlock, pos, Quaternion.identity);
        return block;
    }

    private bool CarvePassagesFrom(Transform currentTransform, int randomNumber)
    {
        bool moreBlocks = false;
      //  int randomNumber = Random.Range(0, 5);

        if(randomNumber == 0) direction = currentTransform.forward;
        else if(randomNumber == 1) direction = -currentTransform.forward;
        else if(randomNumber == 3) direction = currentTransform.right;
        else if(randomNumber == 4) direction = -currentTransform.right;

        

        RaycastHit hit;

        if (Physics.Raycast(currentTransform.position, direction, out hit, 2f))
        {
        //    Debug.Log(hit.collider.gameObject.name);

            if(hit.collider.gameObject == lastBlock.transform) moreBlocks = false;
             else moreBlocks = true;
            
            if(hit.collider.gameObject.GetComponent<Visited>().visited == false)
            {
                hit.collider.gameObject.GetComponent<Visited>().visited = true;
                hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
                hit.collider.gameObject.tag = "Walkable";
                CarvePassagesFrom(hit.collider.gameObject.transform, Random.Range(0, 5));
            }
            else if(hit.collider.gameObject.GetComponent<Visited>().visited == true)
            {
                if(direction == currentTransform.forward) newDirection = -currentTransform.forward;
                else if(direction == -currentTransform.forward) newDirection = currentTransform.forward;
                else if(direction == currentTransform.right) newDirection = -currentTransform.right;
                else if(direction == -currentTransform.right) newDirection = currentTransform.right;

                if (Physics.Raycast(currentTransform.position, newDirection, out hit, 2f))
                {
                    if(hit.collider.gameObject.GetComponent<Visited>().visited == false)
                    {
                        hit.collider.gameObject.GetComponent<Visited>().visited = true;
                        hit.collider.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
                        hit.collider.gameObject.tag = "Walkable";
                        CarvePassagesFrom(hit.collider.gameObject.transform, Random.Range(0, 5));
                    }
                    else 
                    {
                        int number = Random.Range(0, 5);
                        if(number != randomNumber) CarvePassagesFrom(currentTransform, number);
                        else 
                        { 
                            number = Random.Range(0, 5);
                            CarvePassagesFrom(currentTransform, number);

                        }
                        
                    }
                }
            }
        }
        else 
        {
            CarvePassagesFrom(currentTransform, Random.Range(0, 5));
        }
        return moreBlocks;
    }

}
