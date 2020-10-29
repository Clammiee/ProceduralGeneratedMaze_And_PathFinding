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
    bool done = false;
    int random = 0;
    [SerializeField] private GameObject passageBlock;
    int spawnCount = 0;
    public int count2 = 0;
    int number = 0;

    void Awake()
    {
        generateMaze();
    }

    void Start()
    {
        Debug.Log("arraySize " + array.Count);
        Debug.Log("arrayXSize " + arrayX.Count);
    }


    void Update()
    {
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

                pos = new Vector3(i*2, 0, j*2);

                if((i == 0 && j == 0))
                {
                    GameObject newBlock = InstantiateBlock(i, j, 1, pos);
                    arrayX.Add(newBlock);
                    firstBlock = newBlock;
                } 

                else arrayX.Add(InstantiateBlock(i, j, 1, pos));

              
            }
        }
        
    }

    private GameObject InstantiateBlock(int i, int j, int number, Vector3 pos)
    {
        GameObject randomBlock = null;

        spawnCount++;
        if(number == 0) randomBlock = goodBlock;
        else randomBlock = badBlock;


        GameObject block = Instantiate(randomBlock, pos, Quaternion.identity, this.gameObject.transform);
        Debug.Log(spawnCount);
        return block;
    }

    private int CarvePassagesFrom(Transform currentTransform, int randomNumber, bool pass)
    {
        
        if(currentTransform == firstBlock ) moreBlocks++;

        if(moreBlocks > 3)
        {
            Debug.Log("Went to start");
            return 0;
        } 

        if(currentTransform.gameObject.GetComponent<Visited>().visited == false)
        {
            lineArray.Add(currentTransform.gameObject);
            currentTransform.gameObject.GetComponent<Visited>().visited = true;
            currentTransform.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
            currentTransform.gameObject.tag = "Walkable";
            CarvePassagesFrom(currentTransform, 1, true); 
        }
        else 
        {

            GoBack(currentTransform);

        }

        return moreBlocks;

    }

    private void GoBack(Transform currentTransform)
    {
        int i = (lineArray.Count-1);
        random = Random.Range(0, 4);
            bool backout = false;
                done = false;

               
               
               while(i > -1 && backout == false && done == false)
               {
                 if(RandomNeighbor(random, lineArray[i], currentTransform.gameObject) == false)
                   {

                       i--;
                   }
                   else done = true;

               }
               if(i <= -1 && backout == false)
                {
                    

                   if(notVisited(currentTransform) == false) return;
                   backout = true;

                    
                } 
              
    }

    private bool notVisited(Transform currentTransform)
    {

        int j = 0;
        bool finalBack = false;
                
        while(j < lineArray.Count && finalBack == false)
        {

            GameObject neighbor = null;

                        
                        
            int randomNumberUgg = Random.Range(0, 4);

            Debug.Log(randomNumberUgg + ": randomNumberUgg");

            if( (RandomNess(randomNumberUgg, j, currentTransform)) == false) j++;
            else return true;


        }
         return false;
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

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?2");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z + lineArray[j].transform.position.z)/2);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                          Debug.Log("do we even get here?3");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                          Debug.Log("do we even get here?4");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);

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
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                          Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?6");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?7");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?8");

                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            
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
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);

                           return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborDown != null && lineArray[j].GetComponent<Visited>().neighborDown.GetComponent<Visited>().visited == false)
                        {

                           Debug.Log("do we even get here?11");
                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborDown.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?12");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?13");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);

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
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborDown.transform, 0, false);

                            return true;
                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborRight != null && lineArray[j].GetComponent<Visited>().neighborRight.gameObject.GetComponent<Visited>().visited == false)
                        {
                            Debug.Log("do we even get here?15");
                             Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborRight.transform.position.z - lineArray[j].transform.position.z)/2+ lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);

                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborRight.transform, 0, false);
                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborLeft != null && lineArray[j].GetComponent<Visited>().neighborLeft.GetComponent<Visited>().visited == false)
                        {

                            Debug.Log("do we even get here?16");
                            Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborLeft.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z);
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborLeft.transform, 0, false);

                            return true;

                        }
                        else if(lineArray[j].GetComponent<Visited>().neighborUp != null && lineArray[j].GetComponent<Visited>().neighborUp.GetComponent<Visited>().visited == false)
                        {

                           Vector3 pos = new Vector3((int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.x - lineArray[j].transform.position.x)/2 + lineArray[j].transform.position.x, 0, (int)(lineArray[j].GetComponent<Visited>().neighborUp.transform.position.z - lineArray[j].transform.position.z)/2 + lineArray[j].transform.position.z );
                            Instantiate(passageBlock, pos, Quaternion.identity, this.gameObject.transform);
                           Debug.Log("do we even get here?17");
                            CarvePassagesFrom(lineArray[j].GetComponent<Visited>().neighborUp.transform, 0, false);
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
                CarvePassagesFrom(go.GetComponent<Visited>().neighborDown.transform, 0, false);
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
                CarvePassagesFrom(go.GetComponent<Visited>().neighborUp.transform, 0, false);
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
                CarvePassagesFrom(go.GetComponent<Visited>().neighborLeft.transform, 0, false);
                return true;
            }
            else return false;
          
        }
        else return false;

    }


}

