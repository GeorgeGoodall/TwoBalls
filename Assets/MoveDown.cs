using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour{

    public float speed = 3.75f;

    private float elapsedTime;

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }


}