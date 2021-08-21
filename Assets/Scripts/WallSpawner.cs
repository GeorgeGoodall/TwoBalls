using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{ 


    public enum WallTypes
    {
        Empty,
        GRABBABLE,
        SAND,
        BLACKHOLE,
        IMPASSABLE
    }

    public static WallSpawner current;

    public GameObject GrabbableBlock;
    public GameObject SandBlock;
    public GameObject BLACKHOLE;
    public GameObject Impassable_block;
    public List<GameObject> wallCreated;

    Dictionary<WallTypes,GameObject> walls;

    int columnCount = 5;

    int currentPosition = 0;
    public float blockHeight;

    public Sprite wallImage;

    WallImage currentWallImage;

    // Start is called before the first frame update
    void Start()
    {

        blockHeight = GrabbableBlock.GetComponent<SpriteRenderer>().size.x * GrabbableBlock.gameObject.transform.lossyScale.x;
        elapsedDistance = blockHeight;
        current = this;

        currentWallImage = new WallImage(wallImage);

        walls = new Dictionary<WallTypes, GameObject>(){    
            { WallTypes.GRABBABLE, GrabbableBlock},
            { WallTypes.SAND, SandBlock},
            { WallTypes.BLACKHOLE, BLACKHOLE},
            { WallTypes.IMPASSABLE, Impassable_block}
        };
    }

    

    float elapsedDistance = 100f;

    public bool running = false;

    // Update is called once per frame
    void Update()
    {
        if(running){
            elapsedDistance += Time.deltaTime * MoveDown.currentSpeed();
            if(currentWallImage != null){
                spawnFromWallImage();
            }else{
                randomSpawn();
            }

            // randomSpawn();
        }
        

        
    }

    float getColumnPosition(int i)
    {
        return blockHeight * (i - ((columnCount-1) / 2f));
    }


    bool movingUp = true;
    // methods of selecting blocks


    void spawnFromWallImage(){
        if(elapsedDistance >= blockHeight){
            WallTypes[] row = currentWallImage.getCurrentRow();

            for(int i = 0; i < row.Length; i++){

                if(row[i] == WallTypes.Empty){
                    continue;
                }

                GameObject currentWall = createWall(walls[row[i]],new Vector3(getColumnPosition(i),Params.current.screenBounds.y+(blockHeight/2),1f));

                MoveDown md = currentWall.AddComponent<MoveDown>();
                DestroyAtBottom dab = currentWall.AddComponent<DestroyAtBottom>();
            }
            elapsedDistance = 0f;
        }
    }

    int spawnedDeath = 0;
    void randomSpawn(){
        if(elapsedDistance >= blockHeight){
            WallTypes[] indexs = new WallTypes[]{
                WallTypes.GRABBABLE,
                WallTypes.GRABBABLE,
                WallTypes.GRABBABLE,
                WallTypes.SAND,
                WallTypes.SAND,
                WallTypes.SAND,
                WallTypes.BLACKHOLE,
            };

            if(spawnedDeath != 0){
                indexs = new WallTypes[]{
                    WallTypes.GRABBABLE,
                    WallTypes.GRABBABLE,
                    WallTypes.GRABBABLE,
                    WallTypes.SAND,
                    WallTypes.SAND,
                    WallTypes.SAND,
                };
                spawnedDeath--;
            }

            

            int random = Random.RandomRange(0,indexs.Length);

            if(indexs[random] == WallTypes.BLACKHOLE){
                spawnedDeath = 2;
            }
            
            GameObject wallToSpawn = walls[indexs[random]];

            int columnIndex = Random.RandomRange(0,5);

            GameObject currentWall = createWall(wallToSpawn,new Vector3(getColumnPosition(columnIndex),Params.current.screenBounds.y+(blockHeight/2),1f));

            MoveDown md = currentWall.AddComponent<MoveDown>();

            elapsedDistance = 0f;
        }
    }


    GameObject startWalls;

    public void spawnStartBlocks(){
        GameObject grabbableWall = walls[WallTypes.GRABBABLE];
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
