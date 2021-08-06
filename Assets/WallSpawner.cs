using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{ 

    public static WallSpawner current;

    public List<GameObject> walls;

    int columnCount = 4;

    int currentPosition = 0;

    bool movingUp = true;

    public Vector2 screenBounds;
    public float blockHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        blockHeight = walls[0].GetComponent<SpriteRenderer>().size.y;
        elapsedTime = spawnTime;
        current = this;
    }

    float spawnTime = 4f;

    float elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        int random = Random.RandomRange(0,walls.Count);
        

        GameObject wallToSpawn = walls[random];


        if(elapsedTime >= spawnTime){

            GameObject currentWall = Instantiate(
                wallToSpawn,
                new Vector3(getColumnPosition(currentPosition),screenBounds.y+(blockHeight/2),1f),
                Quaternion.EulerAngles(0,0,0)
            );

            MoveDown md = currentWall.AddComponent<MoveDown>();
            md.speed = blockHeight / spawnTime;
            
            DistroyAtBottom dab = currentWall.AddComponent<DistroyAtBottom>();

            elapsedTime = 0f;
            

        }
    }

    float getColumnPosition(int i)
    {

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

        return blockHeight * (i - ((columnCount-1) / 2f));
    }
}
