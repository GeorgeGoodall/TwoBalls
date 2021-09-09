using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{

    GameObject bg1;
    GameObject bg2;

    public float moveDownSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        bg1 = gameObject.transform.Find("BG1").gameObject;
        bg2 = gameObject.transform.Find("BG2").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bg1.transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;
        bg2.transform.position += Vector3.down * moveDownSpeed * Time.deltaTime;

        if(bg1.transform.position.y < -90){
            bg1.transform.position += new Vector3(0,97*2,0);
        }
        if(bg2.transform.position.y < -90){
            bg2.transform.position += new Vector3(0,97*2,0);
        }
    }
}
