using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int arraySizeX;
    [HideInInspector] public List<List<GameObject>> array = new List<List<GameObject>>();
    [HideInInspector] public List<GameObject> arrayX;
    [SerializeField] private GameObject initBlock;
    [HideInInspector] public GameObject firstBlock;
    private GameObject lastBlock;
    public Color c;
    private float timer = 0.01f;
    Vector3 direction;
    Vector3 newDirection;
    private List<GameObject> stack = new List<GameObject>();
    int positionInStack = 0;
    [HideInInspector]public List<GameObject> lineArray = new List<GameObject>(); //array of unvisited blocks
    int moreBlocks = 0;
    [HideInInspector]public int count = 0;
    bool done = false;
    int random = 0;
    [SerializeField] private GameObject passageBlock;
    [HideInInspector]public int count2 = 0;
    int number = 0;

    void Awake()
    {
        generateMaze();
    }

    void Update()
    {
        //use countdown timer to make sure CarvePassagesFrom... happens after generateMaze();
       timer = timer - Time.deltaTime;
       if(timer <= 0)
       {
           if(count == 0) //do this once
           {
               CarvePassagesFrom(firstBlock.transform); //generates passages between initial green blocks that have spaces
               count++;
           }

            if(count2 == 0 && count > 0) //do this once + got to make sure its done after CarvePassagesFrom
            {
                foreach (Transform child in this.transform) //enable colliders + Visited script on all the blocks that were spawned
                {
                    child.GetComponent<Collider>().enabled = true;
                    child.GetComponent<Visited>().enabled = true;
                }
                count2++;
            }
           
       } 

    }

    private void generateMaze() //generates all the initial blocks for the maze, with spaces between them
    {
        for (int i = 0; i < arraySizeX; i++)
        {
            arrayX = new List<GameObject>();

            array.Add(arrayX);

            for (int j = 0; j < arraySizeX; j++)
            {
                Vector3 pos = Vector3.zero;

                pos = new Vector3(i*2, 0, j*2); //*2 used to put spaces so we can after Carve passages between them

                if((i == 0 && j == 0))
                {
                    GameObject newBlock = InstantiateBlock(i, j, pos);
                    arrayX.Add(newBlock);
                    firstBlock = newBlock;
                } 
                else arrayX.Add(InstantiateBlock(i, j, pos));
            }
        }
        
    }

    //used in the generateMaze() to instantiate new blocks at a position + returns the block we just spawned
    private GameObject InstantiateBlock(int i, int j, Vector3 pos)
    {
        GameObject block = Instantiate(initBlock, pos, Quaternion.identity, this.gameObject.transform);
        return block;
    }

    //used in Update() to carve passages between the green blocks
    private void CarvePassagesFrom(Transform currentTransform) 
    {
        if(currentTransform.gameObject.GetComponent<Visited>().visited == false)
        {
            lineArray.Add(currentTransform.gameObject);
            currentTransform.gameObject.GetComponent<Visited>().visited = true;
            currentTransform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
            currentTransform.gameObject.tag = "Walkable";
            CarvePassagesFrom(currentTransform); //recursively call this on current unvisited block
        }
        else //if current is visited than find a neirhboring unvisited block via GoBack...
        {
            GoBack(currentTransform);
        }
    }

    //called in CarvePassagesFrom, used to find current neighbors
    private void GoBack(Transform currentTransform)
    {
        int i = (lineArray.Count-1); //make i = the last block we previously visited
        random = Random.Range(0, 4); 
        bool backout = false; //not really necessary /// REMOVE THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!??????????????????????????????????????????????????????????????
        done = false; //for stopping the while loop
 
        while(i > -1 && backout == false && done == false) //for every visited block choose a random neihgbor and check if its visited or not, if so cut the loop, if not, go to previously visited block
        {
            if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false) //if there are no unvisited nieghbors
            {
                i--; //decreasing from current lineArray of unvisited nieghbors
            }
            else done = true; //if RandomNeighbor returns true, than done = true; cuts the while loop
        }
    
        if(i <= -1 && backout == false) //if RANDOMLY we went through the random unvisited nieghbors of every block in lineArray
        {
            if(notVisited(currentTransform) == false) return; //return if we are done looping through all the unvisited neighbors of current
           // backout = true; //make sure we dont check the if statement + while loop again // probably not necessary but you never know //DELETE THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }     
    }

    private bool notVisited(Transform currentTransform) //while loop function that loops through all visited niehbors (not randomly) and checks if they have unvisited nieghbors one by one
    {
        int j = 0; //start iteration of visited blocks at 0
       // bool finalBack = false; // ???????????????????????????????????????????????????????!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        while(j < lineArray.Count) //&& finalBack == false)
        {
            //GameObject neighbor = null; //IS THIS NECESSARY??????????????!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            int randomNumberAgain = Random.Range(0, 4);

            if((RandomNess(randomNumberAgain, j, currentTransform)) == false) j++; //if the current block has no unvisited neighbors, selected in randomOrder, than go to next block in lineArray 
            else return true; //returning true when we havent found a next unvisited block to go to
        }
        return false; //returns false when we found a next unvisited block and are calculating 
    }

        private bool RandomNess(int number, int j, Transform currentTransform)
        {
            if(number == 0)
            {

                if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?1");

                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?2");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z + lineArray[j].transform.position.z)/2);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                          Debug.Log("do we even get here?3");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                          Debug.Log("do we even get here?4");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform);

                            return true;

                        }
            }
            else if(random == 1)
            {
                
                        if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?5");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                          Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?6");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?7");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?8");

                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform);
                            
                            return true;

                        }
            }
            else if(random == 2)
            {
                if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                         Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?9");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?11");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?12");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform);
                            return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?13");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform);

                            return true;

                        }
            }
            else if(random == 3)
            {
                
                        if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?14");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform);

                            return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?15");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2+ lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform);
                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {

                            Debug.Log("do we even get here?16");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?17");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform);
                           return true;
                        }
            }

                     return false;   

        }

    //called in GoBack, returns true if we found an unvisited neighbor of current block, than continue Carving passage restarting at this unvisited neihbor
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

                CarvePassagesFrom(go.GetComponent<Visited>().neighborRight.transform);
                return true;
            }
            else 
            {
                return false;
            }
        }
        else if(random == 1)
        {
            Debug.Log("testtt1");
            if(go.GetComponent<Visited>().neighborDown != null && go.GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
            {
                Debug.Log("hello its me2");
                done = true;
                Vector3 pos = new Vector3(((int)(go.GetComponent<Visited>().neighborDown.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborDown.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform);
                return true;
            }
            else return false;

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
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform);
                return true;
            }
            else return false;

        }
        else if(random == 3)
        {
            Debug.Log("testtt");
            if(go.GetComponent<Visited>().neighborLeft != null && go.GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
            {
                
                Debug.Log("hello its me4");
                done = true;
                Vector3 pos = new Vector3((int)((go.GetComponent<Visited>().neighborLeft.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(go.GetComponent<Visited>().neighborLeft.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
                Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform);
                return true;
            }
            else return false;
          
        }
        else return false;

    }


}

