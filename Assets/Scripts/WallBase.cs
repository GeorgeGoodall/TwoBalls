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

    protected List<Head> attachedHeads;


    // Start is called before the first frame update
    protected void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        spriteRenderer.size = new Vector2(width*blockWidth,height*blockWidth);
        attachedHeads = new List<Head>();
        //boxCollider.size = new Vector2(width*blockWidth,height*blockWidth);
    }

    public void grab(Head head){
        attachedHeads.Add(head);
    }

    public void release(Head head){
        head.setBite(false);
        attachedHeads.Remove(head);
        // attachedHead.setCanGrab(false);
    }

    public void release(){
        foreach (Head head in attachedHeads.ToArray())
        {
            head.setBite(false);
        }
        // attachedHead.setCanGrab(false);
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
