using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeSpriteTiling : MonoBehaviour
{

    RopeHeads rope;
    LineRenderer lr;

    float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        rope = TwoHeads.current.rope;

        lr = rope.lineRenderer;

        distance = rope.getRopeLength();
    }

    // Update is called once per frame
    void Update()
    {
        lr.material.mainTextureScale = new Vector2(distance * 2, 1);
    }
}
