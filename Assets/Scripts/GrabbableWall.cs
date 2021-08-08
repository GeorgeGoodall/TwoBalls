using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableWall : MonoBehaviour, IWall
{

    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    private int width = 1;
    private int height = 1;

    private float blockWidth = 3.75f;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spriteRenderer.size = new Vector2(width*blockWidth,height*blockWidth);
        boxCollider.size = new Vector2(width*blockWidth,height*blockWidth);
    }

    public void grab(Head head){

    }

    public void release(){

    }
}
