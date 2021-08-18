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
    private GameObject[] balls;

    private Material currentRope;
    public GameObject leftBall;
    public GameObject rightBall;

    // Start is called before the first frame update
    void Start()
    {
        current = this;

        balls = GetAtPath<GameObject>("GameObjects/Heads");
        //ropeMaterials = GetAtPath<Material>("Materials/Ropes");

        // leftBall = Array.Find(balls, b => b.name == "ball1-left");
        // rightBall = Array.Find(balls, b => b.name == "ball1-right");

        setSkin("ball1");
        
    }

    public static T[] GetAtPath<T>(string path)
    {
 
        ArrayList al = new ArrayList();
        string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
 
        foreach (string fileName in fileEntries)
        {
            int assetPathIndex = fileName.IndexOf("Assets");
            string localPath = fileName.Substring(assetPathIndex);
 
            UnityEngine.Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));
 
            if (t != null)
                al.Add(t);
        }
        T[] result = new T[al.Count];
        for (int i = 0; i < al.Count; i++)
            result[i] = (T)al[i];
 
        return result;
    }

    public void setSkin(string skinName){
        GameObject leftBallOld = leftBall;
        GameObject rightBallOld = rightBall;

        leftBall = getBall(skinName+"-left");
        rightBall = getBall(skinName+"-right");
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

