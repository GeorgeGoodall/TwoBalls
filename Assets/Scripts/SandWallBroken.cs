using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWallBroken : MonoBehaviour
{

    float force = 1f;
    float rotationalForce = 100f;
    
    // Start is called before the first frame update
    void Start()
    {

        float randomForceChange = Random.RandomRange(-force,force);

        foreach (Transform child in transform)
        {
            GameObject obj = child.gameObject;
            Rigidbody2D rb = child.GetComponent<Rigidbody2D>();
            Vector3 impulse = (child.transform.position-transform.position)*(force+randomForceChange);

            rb.AddForce(impulse,ForceMode2D.Impulse);

            float randomRotation = Random.RandomRange(-rotationalForce,rotationalForce);

            // add rotational impulse
            float deltaX = transform.position.x-child.transform.position.x;
            rb.AddTorque(deltaX*(rotationalForce+randomRotation));
        }


        DistroyAfterTime(4);
        

    }

    IEnumerator DistroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
