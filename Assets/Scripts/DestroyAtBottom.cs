using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtBottom : MonoBehaviour{


    Vector2 screenBounds;
    float blockHeight;

    private void Start() {
        screenBounds = Params.current.screenBounds;
        blockHeight = WallSpawner.current.blockHeight;
    }

    void Update()
    {

        if(transform.position.y < -Params.current.screenBounds.y - blockHeight * 4){
            Destroy(gameObject);
        }
    }


}