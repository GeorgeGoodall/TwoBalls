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
    [Header("Wall Types")]
    public GameObject GrabbableBlock;
    public GameObject SandBlock;
    public GameObject BLACKHOLE;
    public GameObject Impassable_block;

    [HideInInspector]
    public List<GameObject> wallCreated;
    Dictionary<WallTypes,GameObject> walls;

    [HideInInspector]
    public float blockHeight;

    [Space]
    [Space]
    public Sprite[] wallImages;
    [Space]
    [Header("Camera Zoom Settings")]
    public int blocksToZoomInAdvance = 3;
    public int ignoreCameraChangeWithinBlockCount = 5;

    int currentWallImageIndex = 0;

    WallImage currentWallImage;

    float spawnHeight;

    int startBlocksHeight = 20;
    int rowsSpawned = 0;

    [HideInInspector]
    public float halfScreenHeightBlocks;
    [HideInInspector]
    public bool running = false;

    

    // Start is called before the first frame update
    void Start()
    {
        blockHeight = GrabbableBlock.GetComponent<SpriteRenderer>().size.x * GrabbableBlock.gameObject.transform.lossyScale.x;

        spawnHeight = blockHeight * startBlocksHeight;

        elapsedDistance = blockHeight;
        current = this;

        currentWallImage = new WallImage(wallImages[0]);

        walls = new Dictionary<WallTypes, GameObject>(){    
            { WallTypes.GRABBABLE, GrabbableBlock},
            { WallTypes.SAND, SandBlock},
            { WallTypes.BLACKHOLE, BLACKHOLE},
            { WallTypes.IMPASSABLE, Impassable_block}
        };

        halfScreenHeightBlocks = (Params.current.screenBounds.y/2)/blockHeight;
    }

    

    float elapsedDistance = 0f;


    int skipRows = 0;

    // Update is called once per frame
    void Update()
    {
        if(running){
            elapsedDistance += Time.deltaTime * MoveDown.currentSpeed();
            if(elapsedDistance >= blockHeight){    
                spawnFromWallImage();
            }
        }
    }

    float getColumnPosition(int i)
    {
        return blockHeight * (i - ((currentWallImage.rowWidth-1) / 2f));
    }   

    public void spawnFromWallImage(){
        spawnFromWallImageAt(spawnHeight);
    }

    public void spawnFromWallImageAt(float height){
        if(currentWallImage.hasFinished()){
            currentWallImageIndex++;

            int oldWallWidth = currentWallImage.rowWidth;
            currentWallImage = new WallImage(wallImages[currentWallImageIndex]);
            int newWallWidth = currentWallImage.rowWidth;
            CallFunctionAtHeight.AfterDistanceDelegate e = currentWallImage.updateViewWidth;
            int heightForEvent = startBlocksHeight+rowsSpawned-blocksToZoomInAdvance;
            if(oldWallWidth > newWallWidth){
                heightForEvent = startBlocksHeight+rowsSpawned+(int)Mathf.Ceil((Params.current.screenBounds.y/2)/blockHeight)*2+blocksToZoomInAdvance;
            }

            CallFunctionAtHeight.addEvent(heightForEvent,e);
            spawnFromWallImageAt(height);
        }else{
            WallTypes[] row = currentWallImage.getCurrentRow();

            for(int i = 0; i < row.Length; i++){
                if(row[i] == WallTypes.Empty){
                    continue;
                }
                GameObject currentWall = createWall(walls[row[i]],new Vector3(getColumnPosition(i),height,1f));
                MoveDown md = currentWall.AddComponent<MoveDown>();
                DestroyAtBottom dab = currentWall.AddComponent<DestroyAtBottom>();
            }
            elapsedDistance = 0f;
            rowsSpawned++;
        }
    }


    public void start(){

        
        float maxHeight = 0f;
        for (int i = 0; i < startBlocksHeight + halfScreenHeightBlocks; i++)
        {
            maxHeight = blockHeight * i-halfScreenHeightBlocks;
            spawnFromWallImageAt(maxHeight);
        }
        spawnHeight = maxHeight;
        running = true;
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
        currentWallImageIndex = 0;
        //running = false;
    }


    [Header("Initial Speed Settings")]
    public int slowRowsCount;
    public float endSpeed = 1.5f;
    private float startElapsedDistance = 0f;

    // Update is called once per frame
    void LateUpdate()
    {
        
        int additionalRowsSpawned = rowsSpawned - startBlocksHeight - (int)Mathf.Floor(halfScreenHeightBlocks);

        if(additionalRowsSpawned < slowRowsCount && running){

            float currentHeight = 0;
            if(TwoHeads.current.head1 != null && TwoHeads.current.head2 != null){
                currentHeight = (TwoHeads.current.head1.position().y + TwoHeads.current.head2.position().y)/2;   
            }

            if(currentHeight > 0 || MoveDown.speed > 0){ 
                if(additionalRowsSpawned >= slowRowsCount){
                    MoveDown.speed = endSpeed;
                }else{
                    float forcedMinSpeed =  (additionalRowsSpawned / (float)slowRowsCount) * endSpeed;
                    MoveDown.speed = forcedMinSpeed;
                }
                
            }
        }

        CallFunctionAtHeight.checkEvents(rowsSpawned);

        
    }
}

public static class CallFunctionAtHeight{
    
    public delegate void AfterDistanceDelegate();
    static Dictionary<int,AfterDistanceDelegate> events = new Dictionary<int, AfterDistanceDelegate>();


    public static void addEvent(int rowsSpawned, AfterDistanceDelegate m_methodToCall){

        // if an event is added that happens before an existing event, delete the existing event
        lock (events)
        {
            foreach (var item in events)
            {
                if(item.Key >= rowsSpawned - WallSpawner.current.ignoreCameraChangeWithinBlockCount){
                    events.Remove(item.Key);
                }
            }

            events.Add(rowsSpawned, m_methodToCall);
        }
        
    }

    public static void checkEvents(int rowsSpawned){

        lock(events){
            int halfScreenHeightInBlocks = (int)Mathf.Ceil((Params.current.screenBounds.y/2)/WallSpawner.current.blockHeight);

            foreach (var item in events)
            {
                if(item.Key <= rowsSpawned + halfScreenHeightInBlocks){
                    item.Value();
                    events.Remove(item.Key);
                }
            }
        }
    }

}