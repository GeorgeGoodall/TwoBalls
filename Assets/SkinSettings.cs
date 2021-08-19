using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SkinSettings : MonoBehaviour
{
    public static SkinSettings current;
    private Material[] ropeMaterials;
    public GameObject[] balls {get; private set;}

    private Material currentRope;
    public GameObject leftBall;
    public GameObject rightBall;

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        balls = Resources.LoadAll<GameObject>("Heads");
        //ropeMaterials = GetAtPath<Material>("Materials/Ropes");

        // leftBall = Array.Find(balls, b => b.name == "ball1-left");
        // rightBall = Array.Find(balls, b => b.name == "ball1-right");

        setSkin("ball1");
        
    }

    public void setSkin(string skinName){
        GameObject leftBallOld = leftBall;
        GameObject rightBallOld = rightBall;

        leftBall = getBall(skinName+"-left");
        rightBall = getBall(skinName+"-right");
    }

    public void updateSkin(string skinName){
        setSkin(skinName);
        BallSpawner.current.DestroyTwoBalls();
        BallSpawner.current.spawnBallsAtViewingPosition();
    }

    public void setSkinOld(string skinName){

        GameObject leftBallOld = leftBall;
        GameObject rightBallOld = rightBall;

        Vector3 leftPos = leftBall.transform.position;
        Vector3 rightPos = rightBall.transform.position;


        leftBall = instantiateBall(skinName+"-left",leftPos);
        rightBall = instantiateBall(skinName+"-right",rightPos);

        leftBall.transform.SetParent(leftBallOld.transform.parent);
        rightBall.transform.SetParent(rightBallOld.transform.parent);

        TwoHeads.current.updateHeads(leftBall,rightBall);

        Destroy(leftBallOld);
        Destroy(rightBallOld);
        // create two new heads

    }

    private GameObject instantiateBall(string skinName, Vector3 position){
        GameObject ball = getBall(skinName);
        
        return Instantiate(ball,position,ball.transform.rotation);
    }

    private GameObject getBall(string skinName) => Array.Find(balls, b => b.name == skinName);
}

