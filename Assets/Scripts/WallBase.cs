using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBase : MonoBehaviour, IWall
{

    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;

    protected int width = 1;
    protected int height = 1;

    protected float blockWidth = 2f;

    public int row;

    protected Head attachedHead;

    protected bool headAttached = false;


    // Start is called before the first frame update
    protected void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spriteRenderer.size = new Vector2(width*blockWidth,height*blockWidth);
        //boxCollider.size = new Vector2(width*blockWidth,height*blockWidth);
    }

    public void grab(Head head){
        attachedHead = head;
        headAttached = true;
    }

    public void release(){
        if(headAttached){
            attachedHead.setBite(false);
            attachedHead.setCanGrab(false);
        }
    }


    float blockHeight;

    void Update()
    {
        if(transform.position.y < -Params.current.screenBounds.y - blockWidth){
            WallSpawner.current.removeWallFromList(gameObject);
            release();
            Destroy(gameObject);
        }
    }
}
