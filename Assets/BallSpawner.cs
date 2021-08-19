using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner current;

    public GameObject TwoBallsPrefab;

    public GameObject ropePrefab;

    private GameObject TwoBalls;



    public Vector3 startPos;

    private float ballSeperation = 1f;
    private float ropeHeight = 1.5f;

    private void Start() {
        current = this;
        spawnBallsAtViewingPosition();
    }

    public void spawnBallsAtViewingPosition(){
        spawnTwoBallsAtPosition(new Vector3(0,5,0));
    }

    public void spawnTwoBalls(){
        spawnTwoBallsAtPosition(startPos);
    }

    public void spawnTwoBallsAtPosition(Vector3 pos){
        TwoBalls = Instantiate(TwoBallsPrefab, pos, Quaternion.EulerAngles(0,0,0));

        GameObject leftBallPrefab = SkinSettings.current.leftBall;
        GameObject rightBallPrefab = SkinSettings.current.rightBall;

        GameObject leftBall = Instantiate(
            SkinSettings.current.leftBall,
            getBallSpawnPosition(true, pos),
            leftBallPrefab.transform.rotation,
            TwoBalls.transform
        );

        GameObject rightBall = Instantiate(
            SkinSettings.current.rightBall,
            getBallSpawnPosition(false, pos),
            rightBallPrefab.transform.rotation,
            TwoBalls.transform
        );

        GameObject rope = Instantiate(
            ropePrefab, 
            pos, 
            Quaternion.EulerAngles(0,0,0),
            TwoBalls.transform
        );

        //rope.GetComponent<LineRenderer>().SetPositions(genRopePath(10,pos,ballSeperation,1.5f));

        //TwoHeads.current.updateRope(rope);
        TwoHeads.current.updateHeads(leftBall,rightBall);
        
        
        // need to add rope
    }

    public void DestroyTwoBalls(){
        Destroy(TwoBalls);
    }

    public Vector3 getBallSpawnPosition(bool isLeft, Vector3 pos){
        if(isLeft){
            return new Vector3(pos.x-ballSeperation/2,pos.y,pos.z);
        }else{
            return new Vector3(pos.x+ballSeperation/2,pos.y,pos.z);
        }
    }

    public Vector3 getBallSpawnPosition(bool isLeft){
        if(isLeft){
            return new Vector3(startPos.x-ballSeperation/2,startPos.y,startPos.z);
        }else{
            return new Vector3(startPos.x+ballSeperation/2,startPos.y,startPos.z);
        }
    }


    private Vector3[] genRopePath(int pointCount, Vector3 centre, float width, float height){

        // y = -ax^2 + height
        // y = 0
        // x = (+/-)width / 2
        // 0 = (-a * width / 2)^2 + height
        // (-a * width / 2)^2 = - height
        // -a * width / 2 = srt(height)
        // -a = (2 * srt(height))/width
        // a = -(2 * srt(height))/width
        float a = -(2*Mathf.Sqrt(height))/width;

        Vector3[] points = new Vector3[pointCount];

        float xInterval = width/pointCount;

        for(int i = 0; i<pointCount; i++){
            
            float x = xInterval*(i-(pointCount/2));
            float y = Mathf.Pow((-a*x),2)+height;
            Vector3 pointLocal = new Vector3(x,y,0);
            points[i] = (pointLocal + centre);
        }

        return points;
    }
}
