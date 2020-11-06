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
    public Color c; //green color to show that the block is walkable
    private float timer = 0.01f;
    [HideInInspector] public List<GameObject> lineArray = new List<GameObject>(); //array of unvisited blocks
    [HideInInspector] public int count = 0;
    private bool done = false;
    private int random = 0;
    [SerializeField] private GameObject passageBlock;
    [HideInInspector] public int count2 = 0;

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
        done = false; //for stopping the while loop
 
        while(i > -1 && done == false) //for every visited block choose a random neihgbor and check if its visited or not, if so cut the loop, if not, go to previously visited block
        {
            if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false) //if there are no unvisited nieghbors
            {
                i--; //decreasing from current lineArray of unvisited nieghbors
            }
            else done = true; //if RandomNeighbor returns true, than done = true; cuts the while loop
        }
    
        if(i <= -1) //if RANDOMLY we went through the random unvisited nieghbors of every block in lineArray
        {
            if(notVisited(currentTransform) == false) return; //return if we are done looping through all the unvisited neighbors of current
        }     
    }

    private bool notVisited(Transform currentTransform) //while loop function that loops through all visited niehbors (not randomly) and checks if they have unvisited nieghbors one by one
    {
        int j = 0; //start iteration of visited blocks at 0
        while(j < lineArray.Count) //&& finalBack == false)
        {
            int randomNumberAgain = Random.Range(0, 4);

            if((RandomNess(randomNumberAgain, j)) == false) j++; //if the current block has no unvisited neighbors, selected in randomOrder, than go to next block in lineArray 
            else return true; //returning true when we havent found a next unvisited block to go to
        }
        return false; //returns false when we found a next unvisited block and are calculating 
    }

    private bool RandomNess(int number, int j)
    {
        //switch case for number  //depending on this number the order of checking each unvisited number will change
        switch(number)
        {
            case 0:
                if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborRight, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborLeft, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborUp, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborDown, j) == true) return true;
                else return false;
               // break; //normally you use a break; in a switch case but here we actually return something so there is no need
            case 1:
                if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborLeft, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborUp, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborDown, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborRight, j) == true) return true;
                else return false;
               // break; //normally you use a break; in a switch case but here we actually return something so there is no need
            case 2:
                if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborUp, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborDown, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborRight, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborLeft, j) == true) return true;
                else return false;
                //break; //normally you use a break; in a switch case but here we actually return something so there is no need
            case 3:
                if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborDown, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborRight, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborLeft, j) == true) return true;
                else if(NeighborCheck(lineArray[j].GetComponent<Visited>().neighborUp, j) == true) return true;
                else return false;
               // break; //normally you use a break; in a switch case but here we actually return something so there is no need
        }
            
        return false;
    }
    
    //called inside RandomNess, for every neighbor we want to check if unvisited, than instantiate a passage block to carve a passage between the nieghbor and current block in lineArray
    public bool NeighborCheck(GameObject neighbor, int j)
    {
        if(neighbor != null && neighbor.GetComponent<Visited>().visited == false)
        {
            Vector3 pos = new Vector3((int)(neighbor.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(neighbor.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z );
            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
            CarvePassagesFrom(neighbor.transform);
            return true; //if we were able to carve a passage
        }
        else return false;
    }

    //called in GoBack, returns true if we found an unvisited neighbor of current block, than continue Carving passage restarting at this unvisited neihgbor
    private bool RandomNeighbor(int random, GameObject go, GameObject current)
    {
        GameObject neighbor = null;

        //fill the neighbor gameobject with appropriate neighbor
        if(go.GetComponent<Visited>().neighborRight != null) neighbor = go.GetComponent<Visited>().neighborRight;
        else if(go.GetComponent<Visited>().neighborLeft!= null) neighbor = go.GetComponent<Visited>().neighborLeft;
        else if(go.GetComponent<Visited>().neighborUp != null) neighbor = go.GetComponent<Visited>().neighborUp;
        else if(go.GetComponent<Visited>().neighborDown!= null) neighbor = go.GetComponent<Visited>().neighborDown;

        //determines which nieghbor we are doing the visited check on
        switch(random)
        {
            case 0:
                return RandomNeighborCheck(go, go.GetComponent<Visited>().neighborRight);
            case 1:
                return RandomNeighborCheck(go, go.GetComponent<Visited>().neighborDown);
            case 2:
                return RandomNeighborCheck(go, go.GetComponent<Visited>().neighborUp);
            case 3:
                return RandomNeighborCheck(go, go.GetComponent<Visited>().neighborLeft);
        }
        return false;
    }

    //called in RandomNeighbor, returns true when we carve a passage to an unvisited neighbor from the input
    private bool RandomNeighborCheck(GameObject go, GameObject neighbor)
    {
        if(neighbor != null && neighbor.GetComponent<Visited>().visited == false)
        {
            done = true;
            Vector3 pos = new Vector3((int)((neighbor.transform.position.x - go.transform.position.x)/2 + go.transform.position.x), 0, (int)(neighbor.transform.position.z - go.transform.position.z)/2 + go.transform.position.z);
            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
            CarvePassagesFrom(neighbor.transform);
            return true;
        }
        else return false;
    }

}

