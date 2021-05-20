using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : MonoBehaviour {


    Camera playerCam;
    public LayerMask groundLayer;
    RigidbodyController controller;
    Rigidbody rigidbody;

    private Vector3 movement;
    private Vector3 rotation;


    Quaternion originalRotation;
    Quaternion originalCameraRotation;

    Transform ladderStopper;
    Transform ladderAnchorBottom;
    Transform ladderAnchorTop;
    Transform feet;
    Transform head;


    public float verticalMin = -60.0f;
    public float verticalMax = 60.0f;

    float rotationX = 0.0f;
    float rotationY = 0.0f;


    bool isGrounded = false;
    bool isHittingCeiling = false;

    public bool climbing = false;

    private Quaternion playerRotation;
    private Quaternion cameraRotation;

    float lookSensitivity;
    private void Awake()
    {
        feet = transform.GetChild(0);
        head = transform.GetChild(1);
        controller = GetComponent<RigidbodyController>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        movement = Vector3.zero;
        rotation = Vector3.zero;

      
        playerCam = Camera.main;
        originalRotation = transform.localRotation;
        originalCameraRotation = playerCam.transform.localRotation;
        lookSensitivity = controller.lookSensitivity;

    }

    private void Update()
    {
        movement = Vector3.zero;
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.2f, groundLayer, QueryTriggerInteraction.Ignore);
        isHittingCeiling = Physics.CheckSphere(head.transform.position, 0.2f, groundLayer, QueryTriggerInteraction.Ignore);
        rotationY += Input.GetAxis("Mouse Y") * lookSensitivity;

        if (!climbing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1.5f))
                {

                    if (hit.transform.tag == "Ladder")
                    {
                        ladderAnchorBottom = hit.transform.GetChild(0);
                        ladderAnchorTop = hit.transform.GetChild(1);
                        ladderStopper = hit.transform.GetChild(2);


                       
                        climbing = true;


                        float distanceToAnchorBottom = 0.0f;
                        float distanceToAnchorTop = 0.0f;

                        distanceToAnchorBottom = Vector3.Distance(transform.position, ladderAnchorBottom.transform.position);
                        distanceToAnchorTop = Vector3.Distance(transform.position, ladderAnchorTop.transform.position);

                        if(distanceToAnchorBottom < distanceToAnchorTop)
                        {
                            transform.localPosition = ladderAnchorBottom.transform.position;
                            transform.localRotation = ladderAnchorBottom.transform.rotation;
                            playerCam.transform.rotation = ladderAnchorBottom.transform.rotation;

                        }
                        else
                        {
                            transform.localPosition = ladderAnchorTop.transform.position;
                            transform.localRotation = ladderAnchorTop.transform.rotation;

                            playerCam.transform.rotation = ladderAnchorTop.transform.rotation;


                        }
                      
                        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY; 

                        ladderAnchorBottom = null;
                        ladderAnchorTop = null;

                        Destroy(controller);
                    }
                }

            }
        }


        if (climbing)
        {

            rigidbody.useGravity = false;
            movement.z = Input.GetAxis("Forwards");

            if (Input.GetAxis("Backwards") != 0)
            {
                movement.y = Input.GetAxis("Backwards");
            }else if(Input.GetAxis("Forwards") != 0)
            {
                movement.y = Input.GetAxis("Forwards");
            }
            
            movement = transform.TransformDirection(movement);


            if (Input.GetButton("Backwards"))
            {
                if (isGrounded)
                {
                    climbing = false;
                    AddController();
                }
            }
            else if (Input.GetButton("Forwards"))
            {
                if (ladderStopper)
                {
                    if (isHittingCeiling)
                    {
                        climbing = false;
                        AddController();
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, ladderStopper.position) <= 1.0f)
                        {
                            climbing = false;
                            AddController();
                        }
                    }
                   
                   
                   
                }
            }else if (Input.GetButtonDown("Jump"))
            {
                climbing = false;
                AddController();
            }

        }
        

        if(!climbing)
        {
           
            ladderStopper = null;
            ladderAnchorBottom = null;
            ladderAnchorTop = null;           
        }

      


    }


    private void AddController()
    {
        controller = gameObject.AddComponent<RigidbodyController>();
        controller.enabled = true;       
        controller.groundLayer = groundLayer;
       
    
        originalRotation = transform.localRotation;
        originalCameraRotation = playerCam.transform.localRotation;
    }

    private void FixedUpdate()
    {
        if (climbing)
        {
            if (Input.GetButton("Forwards"))
            {
                rigidbody.MovePosition(transform.position + movement * 2.0f * Time.fixedDeltaTime);
            }
            else if (Input.GetButton("Backwards"))
            {
                rigidbody.MovePosition(rigidbody.position + movement * 2.0f * Time.fixedDeltaTime);

            }
            else
            {
                rigidbody.MovePosition(rigidbody.position + Vector3.zero * Time.fixedDeltaTime);
                rigidbody.velocity = Vector3.zero;
            }
        }
    }

    private void LateUpdate()
    {

        if (climbing)
        {
            rotationY = ClampAngle(rotationY, verticalMin, verticalMax);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);


            //playerCam.transform.localRotation = originalCameraRotation * yQuaternion;
        }
            
        

    }


    private float ClampAngle(float angle, float minimum, float maximum)
    {
        angle = angle % 360;
        if (angle >= -360f && angle <= 360f)
        {
            if (angle < -360f)
            {
                angle += 360f;
            }

            if (angle > 360f)
            {
                angle -= 360f;
            }
        }

        return Mathf.Clamp(angle, minimum, maximum);
    }


}
