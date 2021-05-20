using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    // Use this for initialization
    public LayerMask ignoreCollisionLayer;
    public float collisionDetectionRadius = 0.3f;
    public float transitionSpeed = 2.0f;
    public float ReturnDelaySeconds = 0.25f;
    public GameObject HeadWithMaterial;

    Transform parent;

    Vector3 originalPosition;
    float originalDistance;



    Color cr;
   

	void Awake () {
        parent = transform.parent;
        originalPosition = transform.localPosition;
        originalDistance = Vector3.Distance(transform.position, parent.position);
        cr = HeadWithMaterial.GetComponent<Renderer>().material.color;    
     
    }

    bool cameraCollided = false;

    Collider[] hitColliders;
    Collider[] prevColliders;

    bool hold = false;

    private void Update()
    {
        cameraCollided = Physics.CheckSphere(transform.position, collisionDetectionRadius, ~ignoreCollisionLayer);
        hitColliders = Physics.OverlapSphere(transform.position, collisionDetectionRadius, ~ignoreCollisionLayer);

        if (hitColliders != null)
        {
            if (cameraCollided)
            {
                StartCoroutine(ColliderDistanceCheck(hitColliders));
            }
        }

        if (cameraCollided)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, parent.transform.localPosition, transitionSpeed * Time.deltaTime);
          
        }

        float distanceCull = Vector3.Distance(transform.position, parent.transform.position);

        if(distanceCull < originalDistance / 2)
        {
            cr.a = 0.25f;
 
        }
        else
        {

            cr.a = 1.0f;
        }

     
    }

    IEnumerator ColliderDistanceCheck(Collider[] colliders)
    {
        if (!hold)
        {
            hold = true;
            yield return new WaitForSeconds(ReturnDelaySeconds);

            if (colliders.Length > 0)
            {
                float avgDistance = 0.0f;
                float div = 0f;
                foreach (Collider c in colliders)
                {

                    avgDistance += Vector3.Distance(transform.position, c.transform.position);
                    div += 1;
                }

                if (div == 0)
                    div = 1;

                avgDistance /= div;
                

                if (avgDistance >= originalDistance / 1.5f)
                {
                    while (transform.localPosition != originalPosition)
                    {
                        transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, transitionSpeed * Time.deltaTime);
                        yield return null;
                    }
                }
            }

            hold = false;
        }
    }

    // Update is called once per frame
    void LateUpdate ()
    {
      
    }


}
