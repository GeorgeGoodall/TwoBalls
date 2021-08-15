using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{ 

    public static WallSpawner current;

    public List<GameObject> walls;
    public List<GameObject> wallCreated;

    int columnCount = 5;

    int currentPosition = 0;

    

    
    public float blockHeight;
    float spawnTime = 4f;
    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

        blockHeight = walls[0].GetComponent<SpriteRenderer>().size.x * walls[0].gameObject.transform.lossyScale.x;
        spawnTime =  blockHeight/speed;
        elapsedTime = spawnTime;
        current = this;

        speed = Params.current.initialWallFallSpeed;
    }

    

    float elapsedTime = 100f;

    public void setSpeed(float _speed) {
        spawnTime = _speed;
    }

    public bool running = false;

    // Update is called once per frame
    void Update()
    {
        if(running){
            elapsedTime += Time.deltaTime;
            spawnTime = blockHeight/speed;
            randomSpawn();
        }
        

        
    }

    float getColumnPosition(int i)
    {
        return blockHeight * (i - ((columnCount-1) / 2f));
    }


    bool movingUp = true;
    // methods of selecting blocks
    int columnIndexStairwell(){
        if(currentPosition > columnCount-2){
            movingUp = false;
        }else if(currentPosition < 1){
            movingUp = true;
        }

        if(movingUp){
            currentPosition++;
        }else{
            currentPosition--;
        }
        
        return currentPosition;
    }

    int columnIndexRandom(){
        return Random.RandomRange(0,5);
    }

    void straightGrabbable(){
        if(elapsedTime >= spawnTime){

            for(int i = 0; i < columnCount; i++){
                GameObject currentWall = Instantiate(
                    walls[0],
                    new Vector3(getColumnPosition(i),Params.current.screenBounds.y+(blockHeight/2),1f),
                    Quaternion.EulerAngles(0,0,0)
                );

                MoveDown md = currentWall.AddComponent<MoveDown>();
                md.speed = speed;
                
                DistroyAtBottom dab = currentWall.AddComponent<DistroyAtBottom>();
            }


            elapsedTime = 0f;
        }
    }


    bool spawnedDeath = false;


    void randomSpawn(){



        int[] indexs = new int[]{0,0,0,1,1,2};

        if(spawnedDeath){
            indexs = new int[]{0,0,0,1,1};
            spawnedDeath = false;
        }

        

        int random = Random.RandomRange(0,indexs.Length);

        if(indexs[random] == 2){
            spawnedDeath = true;
        }
        
        GameObject wallToSpawn = walls[indexs[random]];




        if(elapsedTime >= spawnTime){

            int columnIndex = columnIndexRandom();

            GameObject currentWall = createWall(wallToSpawn,new Vector3(getColumnPosition(columnIndex),Params.current.screenBounds.y+(blockHeight/2),1f));

            MoveDown md = currentWall.AddComponent<MoveDown>();
            md.speed = speed;

            elapsedTime = 0f;
        }
    }

    GameObject startWalls;

    public void spawnStartBlocks(){
        GameObject grabbableWall = walls[0];
        GameObject row;

        int additionalRows = 4;
        int rowCount = (int)Mathf.Ceil(Params.current.screenBounds.y/(blockHeight/2))+additionalRows;

        Destroy(startWalls);

        startWalls = new GameObject("startWalls");

        for(int i = 1; i < columnCount-1; i++){
            for(int j = 0; j < rowCount; j++){

                float xPos = blockHeight * (i - ((columnCount-1) / 2f));
                float yPos = blockHeight * (j - ((rowCount-1) / 2f) + additionalRows/2);

                GameObject currentWall = createWall(grabbableWall,new Vector3(xPos,yPos,1f));


                currentWall.transform.parent = startWalls.transform;

            }
        }

        StartWalls sw = startWalls.AddComponent<StartWalls>();
        sw.height = rowCount*blockHeight;
        
    }

    GameObject createWall(GameObject wall, Vector3 position){
        GameObject currentWall = Instantiate(
            wall,
            new Vector3(position.x,position.y,1f),
            Quaternion.EulerAngles(0,0,0)
        );

        wallCreated.Add(currentWall);

        return currentWall;

    }

    public void removeWallFromList(GameObject wall){
        wallCreated.Remove(wall);
    }

    public void reset(){
        foreach (GameObject wall in wallCreated)
        {
            Destroy(wall);
        }
        wallCreated.Clear();
        running = false;
    }




}
