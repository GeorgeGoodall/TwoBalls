using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    Rigidbody2D rb;

    bool canGrab = false;
    GameObject overObject;
    public bool locked {get; private set;} = true;
    bool grabbed = false;
    GameObject grabbedObject;
    Vector2 deltaGrabbedObject;

    List<RopeSegment> attachedRopeSegments;

    bool initialised = false;

    // Start is called before the first frame update
    void Start()
    {
        initialise();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        overObject = collider.gameObject;
        if(collider.gameObject.CompareTag("Grabbable")){
            canGrab = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Grabbable"))
        {
            canGrab = false;
        }
    }

    void initialise(){
        if(!initialised){
            rb = gameObject.GetComponent<Rigidbody2D>();
            attachedRopeSegments = new List<RopeSegment>();
            initialised = true;
        }
    }

    public void attachRopeSegment(RopeSegment rs){
        initialise();
        attachedRopeSegments.Add(rs);
    }

    public void dettachRopeSegment(RopeSegment rs){
        attachedRopeSegments.Remove(rs);
    }

    public void addForce(Vector2 force, ForceMode2D forceMode){
        if(!locked){
            foreach (RopeSegment segment in attachedRopeSegments)
            {
                segment.queueForce(force);
            }
        }
    }

    public bool setPositionFromRope(Vector2 pos){
        if(!locked || grabbed){
            transform.position = pos;
        }
        return !locked;
    }

    public void unbite(){
        locked = false;
        grabbed = false;
    } 

    public void bite() => locked = true;

    public void toggleBite(){
        if(canGrab && !locked){
            locked = true;
            grabbed = true;
            grabbedObject = overObject;
            IWall wall = grabbedObject.GetComponent<IWall>();
            if(wall != null){
                wall.grab(this);
            }
            deltaGrabbedObject =  gameObject.transform.position - grabbedObject.transform.position;
        }else if(locked){
            locked = false;
            grabbed = false;
            if(grabbedObject != null){
                IWall wall = grabbedObject.GetComponent<IWall>();
                if(wall != null){
                    wall.release();
                }
            }
        }
    }
        
    

    public Vector2 velocity() => rb.velocity;
    public Vector2 position(){
        try{
            if(locked && grabbed){
                return grabbedObject.transform.position + new Vector3(deltaGrabbedObject.x, deltaGrabbedObject.y, 0);
            }else{
                return transform.position;
            }
        }catch(System.Exception e){
            return transform.position;
        }
    }

    public void reset(){
        locked = true;
        grabbed = false;
        canGrab = false;
    }
}
