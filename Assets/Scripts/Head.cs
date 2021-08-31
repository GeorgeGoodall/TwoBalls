using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    Rigidbody2D rb;

    bool canGrab = false;
    public bool locked {get; private set;} = true;
    bool grabbed = false;
    GameObject grabbedObject;
    Vector2 deltaGrabbedObject;

    Animator anim;

    List<RopeSegment> attachedRopeSegments;

    DropShadow dropShadow;

    ParticleSystem sphearParticls;
    ParticleSystem coneParticls;

    SpriteRenderer spriteRenderer;

    bool dead = false;
    bool initialised = false;

    Vector3 grabPosition;

    FixedJoint2D fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        initialise();
    }

    List<GameObject> wallsOver;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Grabbable")){
            canGrab = true;
            wallsOver.Add(collider.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Grabbable"))
        {
            wallsOver.Remove(collider.gameObject);
            if(wallsOver.Count <= 0){
                canGrab = false;
            }
        }
    }

    GameObject overObject(){

        if(wallsOver.Count < 1){
            return null;
        }

        GameObject toReturn = wallsOver[0];
        foreach (GameObject wall in wallsOver)
        {
            if((this.transform.position - wall.transform.position).magnitude < (this.transform.position - toReturn.transform.position).magnitude){
                toReturn = wall;
            }
        }

        return toReturn;
    }


    void initialise(){
        if(!initialised){
            rb = gameObject.GetComponent<Rigidbody2D>();
            attachedRopeSegments = new List<RopeSegment>();
            initialised = true;
            anim = gameObject.GetComponent<Animator>();
            dropShadow = gameObject.GetComponent<DropShadow>();
            sphearParticls = gameObject.transform.Find("Sphear Particles").GetComponent<ParticleSystem>();
            coneParticls = gameObject.transform.Find("Cone Particles").GetComponent<ParticleSystem>();
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            wallsOver = new List<GameObject>();
            grabPosition = transform.position;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
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
            rb.AddForce(force,forceMode);
        }
    }

    Vector2 moveToPosition;
    bool movingToPosition = false;
    public void addForceTo(Vector2 position){
        if(!locked){
            moveToPosition = position;
            movingToPosition = true;
        }
    }

    public void stopMovingToPosition(){
        movingToPosition = false;
    }

    public bool setPositionFromRope(Vector2 pos){
        if(!locked || grabbed){
            transform.position = pos;
        }
        return !locked;
    }


    public void setCanGrab(bool _canGrab){
        canGrab = _canGrab;
    }


    public void setBite(bool bite){
        
        if(bite && canGrab && !locked && !dead){
            locked = true;
            grabbed = true;
            grabPosition = transform.position;
            if(anim != null){
                anim.SetBool("grabbing",true);
            }
            if(dropShadow != null){
                dropShadow.setLayer(-1);
            }else{
                Debug.LogError("Ball Object Doesn't Have A Drop Shadow");
            }
            grabbedObject = overObject();
            
            if(grabbedObject != null){
                fixedJoint = gameObject.AddComponent<FixedJoint2D>();
                fixedJoint.autoConfigureConnectedAnchor = false;
                deltaGrabbedObject =  gameObject.transform.position - grabbedObject.transform.position;
                fixedJoint.connectedAnchor = deltaGrabbedObject;
                fixedJoint.connectedBody = grabbedObject.GetComponent<Rigidbody2D>();
            }

            IWall wall = grabbedObject.GetComponent<IWall>();
            if(wall != null){
                wall.grab(this);
            }


        }else if(!bite && locked){
            locked = false;
            grabbed = false;
            rb.constraints = RigidbodyConstraints2D.None;
            if(anim != null){
                anim.SetBool("grabbing",false);
            }
            if(dropShadow != null){
                dropShadow.setLayer(0);
            }else{
                Debug.LogError("Ball Object Doesn't Have A Drop Shadow");
            }
            Destroy(fixedJoint);
            if(grabbedObject != null){
                IWall wall = grabbedObject.GetComponent<IWall>();
                
                if(wall != null){
                    wall.release();
                }
            }

        }
        
    } 

    public Vector2 position(){
        return transform.position;
    }

    public void death(){
        sphearParticls.Emit(250);
        spriteRenderer.enabled = false;
        if(dropShadow != null){
            dropShadow.setActive(false);
        }else{
            Debug.LogError("Ball Object Doesn't Have A Drop Shadow");
        }
        dead = true;
        SoundManager.current.play(SoundManager.AudioType.deathGrab);
    }

    public void fall(){
        coneParticls.Emit(250);
        sphearParticls.Emit(250);
        spriteRenderer.enabled = false;
        if(dropShadow != null){
            dropShadow.setActive(false);
        }else{
            Debug.LogError("Ball Object Doesn't Have A Drop Shadow");
        }
        SoundManager.current.play(SoundManager.AudioType.deathGrab);
        dead = true;
    }

    public void reset(){
        locked = true;
        grabbed = false;
        canGrab = false;
        spriteRenderer.enabled = true;
        dead = false;
        rb.velocity = Vector2.zero;
        if(dropShadow != null){
            dropShadow.setActive(true);
        }else{
            Debug.LogError("Ball Object Doesn't Have A Drop Shadow");
        }
        if(anim != null){
            anim.SetTrigger("Grab");
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
    }

    void Update()
    {
        if(movingToPosition && !locked){
            rb.velocity *= 0.2f;
            if(rb.velocity.magnitude < 10){
                rb.velocity = Vector2.zero;
            }
            rb.AddForce((moveToPosition - rb.position) * 200);
        }
    }

    void LateUpdate()
    {
        if((transform.position.y < -Params.current.screenBounds.y - 5f || transform.position.x < -Params.current.screenBounds.x - 9f || transform.position.x > Params.current.screenBounds.x + 9f) && !dead){
            fall();
        }

        // if((grabbed || locked) && grabbedObject != null){
        //     rb.position = grabbedObject.transform.position + new Vector3(deltaGrabbedObject.x, deltaGrabbedObject.y, 0);
        // }
    }
}
