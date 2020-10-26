using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int arraySizeX;

    public List<List<GameObject>> array = new List<List<GameObject>>();
    public List<GameObject> arrayX;

    [SerializeField] private GameObject goodBlock;

    [SerializeField] private GameObject badBlock;
    public GameObject firstBlock;
    private GameObject lastBlock;
    public Color c;
    public Color cRed;
    private float timer = 0.5f;
    Vector3 direction;
    Vector3 newDirection;
    private List<GameObject> stack = new List<GameObject>();
    int positionInStack = 0;
    public List<GameObject> lineArray = new List<GameObject>();
    int moreBlocks = 0;
    public int count = 0;
   // bool backout = false;
    bool done = false;
    //bool finalBack = false;
    int random = 0;
    [SerializeField] private GameObject passageBlock;
    int spawnCount = 0;
    public int count2 = 0;


        
    int number = 0; //0 for good and 1 for bad

    void Awake()
    {
      //  arraySizeX = arraySizeX;
       // array = new GameObject[arraySizeX, arraySizeX];
      //  stack = new GameObject[arraySizeX * arraySizeX -1];
        generateMaze();
    }

    void Start()
    {
        
    Debug.Log("arraySize " + array.Count);
    Debug.Log("arrayXSize " + arrayX.Count);
        
      //  CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5))
       //generate();
       //CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5));
      // CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5));
      
      
    }


    void Update()
    {
       // CarvePassagesFrom(firstBlock.transform);
      // Debug.Log(lineArray.Count);

              

       timer = timer - Time.deltaTime;

       if(timer <= 0)
       {
           if(count == 0)
           {
               CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5), false);
               count++;
           }

            if(count2 == 0 && count > 0)
            {
                foreach (Transform child in this.transform)
                {
                    child.GetComponent<Collider>().enabled = true;
                    child.GetComponent<Visited>().enabled = true;
                }
                count2++;
            }
           //if count2 == 0

            //if count > 0
            //Get all children of this gameObject and add add enable collider if they dont have one enabled yet



           //CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5));
         //  if(CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5)) < 3)
           //{
              // timer = 0.1f;
          // }
           //while(CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5)) < 2)
          // {
               
           //}
          // CarvePassagesFrom(firstBlock.transform, Random.Range(0, 5));
          // { 
              
              //timer = 0.1f;
           //    timer = 0.1f;
                
         // }
          // else Debug.Log("Done Maze creation"); 
       } 

    }

    private void generateMaze()
    {
        for (int i = 0; i < arraySizeX; i++)
        {
            arrayX = new List<GameObject>();

            array.Add(arrayX);

            for (int j = 0; j < arraySizeX; j++)
            {
                Vector3 pos = Vector3.zero;

            /*if(i % 2 == 0 && j % 2 == 0)
            {
                pos = new Vector3(i, 0, j);
            }
            else if(i % 2 != 0 && j % 2 == 0)
            {
                pos = new Vector3(i*2, 0, j);
            } 
            else if(i % 2 == 0 && j % 2 != 0)
            {
                pos = new Vector3(i, 0, j*2);
            } 
            else if(i % 2 != 0 && j % 2 != 0)
            {
                pos = new Vector3(i*2, 0, j*2);
            } */
                pos = new Vector3(i*2, 0, j*2);

                if((i == 0 && j == 0))
                {
                    GameObject newBlock = InstantiateBlock(i, j, 1, pos);
                    arrayX.Add(newBlock);
                    firstBlock = newBlock;
                } 
              //  else if(i == (arraySizeX*2-1)-1 && j == (arraySizeX*2-1)-1)
               // {
                 //  array[i, j] = InstantiateBlock(i, j, 0, pos);
                  // lastBlock = array[i, j];
               // }
                //else if(i == 0 || j == 0 || i == arraySizeX || j == arraySizeX) array[i, j] = InstantiateBlock(i, j, 1);
               // else if(i < arraySizeX && j < arraySizeX) arrayX.Add(InstantiateBlock(i, j, 1, pos));
                else arrayX.Add(InstantiateBlock(i, j, 1, pos));

               // array[i, j] = InstantiateBlock(i, j, number);

               // InstantiateBlock(i, j, number);

                //if()
            }
        }
        // The initialization of the individual positions are later.

        

        // Only the first position is initialized and set at random

       /* int startX = Random.Range(0, arraySizeX-1);
        int startZ = Random.Range(0, arraySizeX-1);
        stack.Add(array[startX, startZ]);
        positionInStack = 0;

        // The first cel is set to 'visited'
        array[startX, startZ].GetComponent<Visited>().visited = true;
        array[startX, startZ].GetComponent<Renderer>().material.SetColor("_Color", c);
        array[startX, startZ].gameObject.tag = "Walkable";

      //  Debug.Log(stack.Count);*/

       // return;

        
    }

    private GameObject InstantiateBlock(int i, int j, int number, Vector3 pos)
    {
        GameObject randomBlock = null;
      // Vector3 pos = Vector3.zero;
      //  int number = Random.Range(0, 2); //DOES NOT INCLUDE NUMBER 2
        spawnCount++;
        if(number == 0) randomBlock = goodBlock;
        else randomBlock = badBlock;

        //make every other number have +1 to its pos
      /*  if(i % 2 == 0 && j % 2 == 0) pos = new Vector3(i, 0, j);
        else if(i % 2 != 0 && j % 2 == 0) pos = new Vector3(i+1, 0, j);
        else if(i % 2 == 0 && j % 2 != 0) pos = new Vector3(i, 0, j+1);
        else if(i % 2 != 0 && j % 2 != 0) pos = new Vector3(i+1, 0, j+1); */

        GameObject block = Instantiate(randomBlock, pos, Quaternion.identity, this.gameObject.transform);
        Debug.Log(spawnCount);
        return block;
    }

    private int CarvePassagesFrom(Transform currentTransform, int randomNumber, bool pass)
    {
        // FIRST TRY HERE ----------------------------------------------------------------------------------------------------------
        
      //  int randomNumber = Random.Range(0, 5);

        /*if(randomNumber == 0) direction = currentTransform.forward;
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
                           // CarvePassagesFrom(currentTransform, number);

                        }
                        
                    }
                }
            }
        }
        else 
        {
            CarvePassagesFrom(currentTransform, Random.Range(0, 5));
        }
        return moreBlocks; */

        // FIRST TRY END HERE -----------------------------------------------------------------------------------------------------

        //int moreBlocks = false;

        if(currentTransform == firstBlock ) moreBlocks++;

        if(moreBlocks > 3)
        {
            Debug.Log("Went to start");
            return 0;
        } 
        
     /*   if(randomNumber == 0) direction = currentTransform.forward;
        else if(randomNumber == 1) direction = -currentTransform.forward;
        else if(randomNumber == 3) direction = currentTransform.right;
        else if(randomNumber == 4) direction = -currentTransform.right; */




        if(currentTransform.gameObject.GetComponent<Visited>().visited == false)
        {
            lineArray.Add(currentTransform.gameObject);
            currentTransform.gameObject.GetComponent<Visited>().visited = true;
            currentTransform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
            currentTransform.gameObject.tag = "Walkable";
            CarvePassagesFrom(currentTransform, 1, true);

            /*int random = Random.Range(0, 4);

            if(random == 0 && currentTransform.gameObject.GetComponent<Visited>().neighborUp != null)
            {
                currentTransform.gameObject.GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited = true;
                currentTransform.gameObject.GetComponent<Visited>().neighborUp.GetComponent<Renderer>().material.SetColor("_Color", cRed);
                currentTransform.gameObject.GetComponent<Visited>().neighborUp.tag = "Untagged";
                CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().neighborUp.transform, 1, true);
            }
            else if(random == 1 && currentTransform.gameObject.GetComponent<Visited>().neighborRight != null)
            {
                currentTransform.gameObject.GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited = true;
                currentTransform.gameObject.GetComponent<Visited>().neighborRight.GetComponent<Renderer>().material.SetColor("_Color", cRed);
                currentTransform.gameObject.GetComponent<Visited>().neighborRight.tag = "Untagged";
                CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().neighborRight.transform, 1, true);
            }
            else if(random == 2 && currentTransform.gameObject.GetComponent<Visited>().neighborDown != null)
            {
                currentTransform.gameObject.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited = true;
                currentTransform.gameObject.GetComponent<Visited>().neighborDown.GetComponent<Renderer>().material.SetColor("_Color", cRed);
                currentTransform.gameObject.GetComponent<Visited>().neighborDown.tag = "Untagged";
                CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().neighborDown.transform, 1, true);
            }
            else if(random == 3 && currentTransform.gameObject.GetComponent<Visited>().neighborLeft != null)
            {
                currentTransform.gameObject.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited = true;
                currentTransform.gameObject.GetComponent<Visited>().neighborLeft.GetComponent<Renderer>().material.SetColor("_Color", cRed);
                currentTransform.gameObject.GetComponent<Visited>().neighborLeft.tag = "Untagged";
                CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().neighborLeft.transform, 1, true);
            } */
        }
        else //if(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction) != null && currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited == true)
        {
          //  if(notVisited() == false) return 0;
            GoBack(currentTransform);
           // for (int i = lineArray.Count-1; i > -1; i--)
         //  {
            /*   int i = lineArray.Count-1;
               int random = Random.Range(0, 4);

                done = false;

               Debug.Log(random + ": random");
               
               
               while(i > -1 && backout == false && done == false)
               {
                   if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false)
                   {
                       i--;
                   }
                   else done = true;
                    //i = lineArray.Count-1; //RandomNeighbor(Random.Range(0, 5), lineArray[i], currentTransform.gameObject);
                    Debug.Log("i: " + i);
               }
               //if(i > -1) backout = false;
               if(i <= -1)
               {
                    backout = true;
                    notVisited(currentTransform, true); //, Random.Range(0, 5));
                    Debug.Log("why dont we see this");
               } /*

               if(backout == true)
               {
                   int j = 0;
                   finalBack = false;
                
                   while(j < lineArray.Count && finalBack == false && final == false)
                   {
                       Debug.Log(j + " j");

                       if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
                        {
                            backout = false;
                            final = true;
                            finalBack = true;
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                          //  j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                            backout = false;
                            final = true;
                            finalBack = true;
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {
                            backout = false;
                            final = true;
                            finalBack = true;
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {
                            backout = false;
                            final = true;
                            finalBack = true;
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //j = lineArray.Count;
                        }
                        else j++;

                   }
               }*/

               //if(backout == true) CarvePassagesFrom(currentTransform.gameObject.transform , 0);
             //  RandomNeighbor(Random.Range(0, 5), lineArray[lineArray.Count-1], currentTransform.gameObject);

               /* if(lineArray[i].GetComponent<Visited>().neighborRight != null && lineArray[i].GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
                {
                   // lineArray.Remove(lineArray[i]);
                    CarvePassagesFrom(lineArray[i].GetComponent<Visited>().neighborRight.transform, 0);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborLeft != null && lineArray[i].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                {
                   // lineArray.Remove(lineArray[i]);
                    CarvePassagesFrom(lineArray[i].GetComponent<Visited>().neighborLeft.transform, 0);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborUp != null && lineArray[i].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                {
                   // lineArray.Remove(lineArray[i]);
                    CarvePassagesFrom(lineArray[i].GetComponent<Visited>().neighborUp.transform, 0);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborDown != null && lineArray[i].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                {
                   // lineArray.Remove(lineArray[i]);
                    CarvePassagesFrom(lineArray[i].GetComponent<Visited>().neighborDown.transform, 0);
                }*/
           //}
        }

        return moreBlocks;

    }

    private void GoBack(Transform currentTransform)
    {
        int i = (lineArray.Count-1);
        random = Random.Range(0, 4);
            bool backout = false;
                done = false;

              // Debug.Log(random + ": random");
               
               
               while(i > -1 && backout == false && done == false)
               {
                 if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false)
                   {
                      // notVisited();
                       i--;
                   }
                   else done = true;
                    //i = lineArray.Count-1; //RandomNeighbor(Random.Range(0, 5), lineArray[i], currentTransform.gameObject);
                   // Debug.Log("i: " + i);
               }
               if(i <= -1 && backout == false)
                {
                    
                   // done = true; 
                   if(notVisited(currentTransform) == false) return;
                   backout = true;
                  //  i = (lineArray.Count-1);
                //  RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false
                   // notVisited(); //, Random.Range(0, 5));
                  //  Debug.Log("why dont we see this");
                    
                } 
              
    }

    private bool notVisited(Transform currentTransform)
    {
       /* int i = lineArray.Count-1;
               int random = Random.Range(0, 4);

                done = false;

               Debug.Log(random + ": random");
               
               
               while(i > -1 && backout == false && done == false)
               {
                   if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false)
                   {
                       i--;
                   }
                   else done = true;
                    //i = lineArray.Count-1; //RandomNeighbor(Random.Range(0, 5), lineArray[i], currentTransform.gameObject);
                    Debug.Log("i: " + i);
               }
               //if(i > -1) backout = false;
               if(i <= -1)
               {
                    backout = true;
                    Debug.Log("why dont we see this");
               }*/

                   int j = 0;
                   bool finalBack = false;
                
                   while(j < lineArray.Count && finalBack == false)
                   {
                     //  Debug.Log(j + " j");

                       GameObject neighbor = null;

                        
                        
                        int randomNumberUgg = Random.Range(0, 4);

                        Debug.Log(randomNumberUgg + ": randomNumberUgg");

                        if( (RandomNess(randomNumberUgg, j, currentTransform)) == false) j++;
                        else return true;
                        
                      /* if(lineArray[j].GetComponent<Visited>().neighborRight != null && neighbor.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?");
                            finalBack = true;
                            
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            //
                            return true;
                          //  j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && neighbor.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                            //final = true;
                            finalBack = true;
                            
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                            //finalBack = true;
                            return true;
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && neighbor.GetComponent<Visited>().visited == false)
                        {
                           // backout = false;
                           // final = true;
                           finalBack = true;
                           
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                            //finalBack = true;
                           // j = lineArray.Count;
                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && neighbor.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                           // final = true;
                           finalBack = true;
                           
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //finalBack = true;
                            return true;
                            //j = lineArray.Count;
                        }*/
                      //  else j++;

                   }
                   return false;
        }

        private bool RandomNess(int number, int j, Transform currentTransform)
        {
            

            if(number == 0)
            {
              /*  if(lineArray[j].GetComponent<Visited>().neighborRight != null) neighbor = ;
            else if(lineArray[j].GetComponent<Visited>().neighborLeft!= null) neighbor = lineArray[j].GetComponent<Visited>().neighborLeft;
            else if(lineArray[j].GetComponent<Visited>().neighborUp != null) neighbor = lineArray[j].GetComponent<Visited>().neighborUp;
            else if(lineArray[j].GetComponent<Visited>().neighborDown!= null) neighbor = lineArray[j].GetComponent<Visited>().neighborDown;
            else return false; */

                if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?1");
                           // finalBack = true;
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            //
                            return true;
                          //  j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                            //final = true;
                           // finalBack = true;
                           Debug.Log("do we even get here?2");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z + lineArray[j].transform.position.z)/2);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                            //finalBack = true;
                            return true;
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {
                           // backout = false;
                           // final = true;
                          // finalBack = true;
                          Debug.Log("do we even get here?3");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                            //finalBack = true;
                           // j = lineArray.Count;
                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                           // final = true;
                          // finalBack = true;
                          Debug.Log("do we even get here?4");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //finalBack = true;
                            return true;
                            //j = lineArray.Count;
                        }
            }
            else if(random == 1)
            {
                
                        if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                            //final = true;
                           // finalBack = true;
                           Debug.Log("do we even get here?5");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                            //finalBack = true;
                            return true;
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {
                           // backout = false;
                           // final = true;
                          // finalBack = true;
                          Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?6");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                            //finalBack = true;
                           // j = lineArray.Count;
                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                           // final = true;
                        //   finalBack = true;
                           Debug.Log("do we even get here?7");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //finalBack = true;
                            return true;
                            //j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?8");
                          //  finalBack = true;
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            //
                            return true;
                          //  j = lineArray.Count;
                        }
            }
            else if(random == 2)
            {
                if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {
                           // backout = false;
                           // final = true;
                         //  finalBack = true;
                         Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?9");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                            //finalBack = true;
                           // j = lineArray.Count;
                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                           // final = true;
                         //  finalBack = true;
                           Debug.Log("do we even get here?11");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //finalBack = true;
                            return true;
                            //j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?12");
                           // finalBack = true;
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            //
                            return true;
                          //  j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                            //final = true;
                          // finalBack = true;
                            Debug.Log("do we even get here?13");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                            //finalBack = true;
                            return true;
                           // j = lineArray.Count;
                        }
            }
            else if(random == 3)
            {
                
                        if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                           // final = true;
                          // finalBack = true;
                           Debug.Log("do we even get here?14");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);
                            //finalBack = true;
                            return true;
                            //j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?15");
                          //  finalBack = true;
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2+ lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            //
                            return true;
                          //  j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                          //  backout = false;
                            //final = true;
                           // finalBack = true;
                            Debug.Log("do we even get here?16");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);
                            //finalBack = true;
                            return true;
                           // j = lineArray.Count;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {
                           // backout = false;
                           // final = true;
                           //finalBack = true;
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?17");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
                            //finalBack = true;
                           // j = lineArray.Count;
                           return true;
                        }
            }

                     return false;   

        }


    private bool RandomNeighbor(int random, GameObject go, GameObject current)
    {
        GameObject neighbor = null;

        if(go.GetComponent<Visited>().neighborRight != null) neighbor = go.GetComponent<Visited>().neighborRight;
        else if(go.GetComponent<Visited>().neighborLeft!= null) neighbor = go.GetComponent<Visited>().neighborLeft;
        else if(go.GetComponent<Visited>().neighborUp != null) neighbor = go.GetComponent<Visited>().neighborUp;
        else if(go.GetComponent<Visited>().neighborDown!= null) neighbor = go.GetComponent<Visited>().neighborDown;
        else Debug.Log("fail1");


        if(random == 0)
        {
            Debug.Log("testtt0");
            if(go.GetComponent<Visited>().neighborRight != null && go.GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                Debug.Log("hello its me");
                done = true;

                Vector3 pos = new Vector3((int)((go.GetComponent<Visited>().neighborRight.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborRight.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                CarvePassagesFrom(go.GetComponent<Visited>().neighborRight.transform, 0, false);
                return true;
            }
            else 
            {
                return false;
            }
           /* else if(go.GetComponent<Visited>().neighborDown != null && go.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborUp != null && go.GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborLeft != null && go.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform, 0);
            } */
        }
        else if(random == 1)
        {
            Debug.Log("testtt1");
            if(go.GetComponent<Visited>().neighborDown != null && go.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                Debug.Log("hello its me2");
                done = true;
                Vector3 pos = new Vector3(((int)(go.GetComponent<Visited>().neighborDown.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborDown.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform, 0, false);
                return true;
            }
            else return false;
           /* else if(go.GetComponent<Visited>().neighborRight != null && go.GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborRight.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborUp != null && go.GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborLeft != null && go.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform, 0);
            } */
        }
        else if(random == 2)
        {
            Debug.Log("testtt2");
            if(go.GetComponent<Visited>().neighborUp != null && go.GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
            {
                
                // lineArray.Remove(lineArray[i]);
                Vector3 pos = new Vector3((int)((go.GetComponent<Visited>().neighborUp.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborUp.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                Debug.Log("hello its me3");
                done = true;
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform, 0, false);
                return true;
            }
            else return false;
          /*  else if(go.GetComponent<Visited>().neighborRight != null && go.GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborRight.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborDown != null && go.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborLeft != null && go.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform, 0);
            }*/
        }
        else if(random == 3)
        {
            Debug.Log("testtt");
            if(go.GetComponent<Visited>().neighborLeft != null && go.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
            {
                
                // lineArray.Remove(lineArray[i]);
                Debug.Log("hello its me4");
                done = true;
                Vector3 pos = new Vector3((int)((go.GetComponent<Visited>().neighborLeft.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborLeft.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform, 0, false);
                return true;
            }
            else return false;
           /*else if(go.GetComponent<Visited>().neighborRight != null && go.GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborRight.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborDown != null && go.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform, 0);
            }
            else if(go.GetComponent<Visited>().neighborUp != null && go.GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
            {
                // lineArray.Remove(lineArray[i]);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform, 0);
            }*/
        }
        else return false;

        //return true;
    }

  /*  private int[] CheckNeighbors(GameObject go)
    {
        Vector3 currentPosition = go.transform.position;

        List<int> result = new List<int>();

        int x = (int)currentPosition.x;
        int z = (int)currentPosition.z;

        // Check wall 0
        if (x - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x - 1, z].GetComponent<Visited>().visited == false) result.Add(0); // Add wall 0
        }

          // Check wall 5 
        if (x + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x + 1, z].GetComponent<Visited>().visited == false) result.Add(3); // Add wall 5
        }


        // Check wall 1 
       /* if (y - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x, z].GetComponent<Visited>().visited == false) result.Add(1); // Add wall 1
        }

        // Check wall 4 
        if (y + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x, z].GetComponent<Visited>().visited == false) result.Add(4); // Add wall 4
        } */


        // Check wall 2 
       /* if (z - 1 >= 0) // Are we still in the size of the maze
        {
            if (array[x, z - 1].GetComponent<Visited>().visited == false) result.Add(1); // Add wall 2
        }

        // Check wall 3
        if (z + 1 < arraySizeX) // Are we still in the size of the maze
        {
            if (array[x, z + 1].GetComponent<Visited>().visited == false) result.Add(2); // Add wall 3
        }

        return result.ToArray();
    } */

  /*  private void generate()
    {
        while(positionInStack >= 0)
        {
            // The cell we have to check is saved in the stack at the current position
            int x = (int)stack[positionInStack].transform.position.x;
          //  int y = stack[positionInStack].transform.position.y;
            int z = (int)stack[positionInStack].transform.position.z;

            // Check the cells around (neighbours)
            int[] possibleNeighbors = CheckNeighbors(stack[positionInStack]);
            

            if(possibleNeighbors.Length > 0)
            {
                //Debug.Log(possibleNeighbors.Length);
                // Choose a random wall and open it to the next cell
                
                int wall = possibleNeighbors[Random.Range(0, possibleNeighbors.Length-1)];
               // Debug.Log("wall " + wall);

               // Debug.Log("x " + x + "z " + z);

                // Open the wall by setting the wall to false
                //array[x, z].GetComponent<Visited>().walls[wall] = false;
               
                // Get the new coordinates
                if (wall == 0)
                {
                    direction = -array[x, z].transform.forward;
                    x--;
                } 
                else if (wall == 3)
                {
                    direction = array[x, z].transform.forward;
                    x++;
                } 
               // else if (wall == 1) y--;
               // else if (wall == 4) y++;
                else if (wall == 1)
                {
                    direction = -array[x, z].transform.right;
                    z--;
                } 
                else if (wall == 2)
                {
                    direction = array[x, z].transform.right;
                    z++;
                } 

                // Set the new cell to visitied and open the wall on the opposite side of the previous cell
                // and increase the stack by one and set the new position
                positionInStack++;

               // Debug.Log("New x " + x);
             //  Debug.Log("New z " + z);

                stack.Add(array[x, z]);
               // Debug.Log("positionInStack " + positionInStack);
                //.GetComponent<Visited>().MazePosition(x, z);

                /*if(array[x, z].GetComponent<Visited>().FindNeighbor(direction) != null && array[x, z].GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited == false)
                {
                  
                // currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited = true;
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Renderer>().material.SetColor("_Color", c);
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).tag = "Walkable";
                //  CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).transform, Random.Range(0, 5));

                    array[x, z].GetComponent<Visited>().visited = true;
                    //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
                    array[x, z].GetComponent<Renderer>().material.SetColor("_Color", c);
                    array[x, z].gameObject.tag = "Walkable";
                }*/
                //Recursion(array[x, z]);
           /*  }
            else positionInStack--;
        }
    }

    private void Recursion(GameObject go)
    {
        if(go.GetComponent<Visited>().visited == false)
        {
            go.GetComponent<Visited>().visited = true;
            lineArray.Add(go);
            //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
            go.GetComponent<Renderer>().material.SetColor("_Color", c);
            go.gameObject.tag = "Walkable";

            Recursion(go);
        }
        else if(go.GetComponent<Visited>().visited == true)
        {
            for (int i = lineArray.Count-1; i > -1; i--)
            {
                if(lineArray[i].GetComponent<Visited>().neighborRight != null && lineArray[i].GetComponent<Visited>().neighborRight.GetComponent<Visited>().visited == false)
                {
                    Recursion(lineArray[i].GetComponent<Visited>().neighborRight);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborLeft != null && lineArray[i].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                {
                    Recursion(lineArray[i].GetComponent<Visited>().neighborLeft);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborUp != null && lineArray[i].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                {
                    Recursion(lineArray[i].GetComponent<Visited>().neighborUp);
                }
                else if(lineArray[i].GetComponent<Visited>().neighborDown != null && lineArray[i].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                {
                    Recursion(lineArray[i].GetComponent<Visited>().neighborDown);
                }
            }
        } */
      //  else Recursion(go, -direction);
      /* if(go.GetComponent<Visited>().FindNeighbor(direction) != null)
                {
                  GameObject newGo = go.GetComponent<Visited>().FindNeighbor(direction);

                  if(newGo != null && newGo.GetComponent<Visited>().visited == false)
                  {
                      // currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Visited>().visited = true;
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).GetComponent<Renderer>().material.SetColor("_Color", c);
                    //currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).tag = "Walkable";
                //  CarvePassagesFrom(currentTransform.gameObject.GetComponent<Visited>().FindNeighbor(direction).transform, Random.Range(0, 5));

                    newGo.GetComponent<Visited>().GetComponent<Visited>().visited = true;
                    //  array[x, z].GetComponent<Visited>().walls[3-wall] = false;
                     newGo.GetComponent<Visited>().GetComponent<Renderer>().material.SetColor("_Color", c);
                     newGo.GetComponent<Visited>().gameObject.tag = "Walkable";
                  }
                  else Recursion(newGo, -direction);
                
                } */
  //  }
}

