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

    Animator anim;

    List<RopeSegment> attachedRopeSegments;

    DropShadow dropShadow;

    ParticleSystem sphearParticls;
    ParticleSystem coneParticls;

    SpriteRenderer spriteRenderer;

    bool dead = false;



    bool initialised = false;

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
            overObject = collider.gameObject;
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

    public void setCanGrab(bool _canGrab){
        canGrab = _canGrab;
    }

    public void bite() => locked = true;

    public void setBite(bool bite){
        
            if(bite && canGrab && !locked && !dead){
                locked = true;
                grabbed = true;
                if(anim != null){
                    anim.SetTrigger("Grab");
                }
                dropShadow.setLayer(-1);
                grabbedObject = overObject;
                IWall wall = grabbedObject.GetComponent<IWall>();
                if(wall != null){
                    wall.grab(this);
                }
                
                deltaGrabbedObject =  gameObject.transform.position - grabbedObject.transform.position;

            }else if(!bite && locked){
                locked = false;
                grabbed = false;
                if(anim != null){
                    anim.SetTrigger("Release");
                }
                dropShadow.setLayer(1);
                if(grabbedObject != null){
                    IWall wall = grabbedObject.GetComponent<IWall>();
                    if(wall != null){
                        wall.release();
                    }
                }
            }
        
    } 

    public void toggleBite(){
        
        if(canGrab && !locked){
            locked = true;
            grabbed = true;
            if(anim != null){
                anim.SetTrigger("Grab");
            }
            grabbedObject = overObject;
            IWall wall = grabbedObject.GetComponent<IWall>();
            if(wall != null){
                wall.grab(this);
            }
            deltaGrabbedObject =  gameObject.transform.position - grabbedObject.transform.position;

        }else if(locked){
            locked = false;
            grabbed = false;
            if(anim != null){
                anim.SetTrigger("Release");
            }
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

    public void death(){
        sphearParticls.Emit(150);
        spriteRenderer.enabled = false;
        dropShadow.setActive(false);
        dead = true;
    }

    public void fall(){
        coneParticls.Emit(150);
        dead = true;
    }

    public void reset(){
        locked = true;
        grabbed = false;
        canGrab = false;
        spriteRenderer.enabled = true;
        dead = false;
        dropShadow.setActive(true);
        if(anim != null){
            anim.SetTrigger("Grab");
        }
    }

    void Update()
    {
        if(transform.position.y < -10 && !dead){
            fall();
        }
    }
}
