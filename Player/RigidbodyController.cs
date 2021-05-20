


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyController : MonoBehaviour
{

    public LayerMask groundLayer;

    public float lookSensitivity = 5.0f;


    public float horizontalMin = -360f;
    public float horizontalMax = 360f;

    public float verticalMin = -60.0f;
    public float verticalMax = 60.0f;



    public Transform CameraPivot;
    public Transform CameraPivotHorizontal;
    public Transform rigHead;
    public Transform headTurnPivot;

    public float headTurnMin = -45.0f;
    public float headTurnMax = 45.0f;


   

    private Transform feet;
    private Rigidbody rigidbody;

   

    private bool isGrounded;

    private Vector3 movement;
    private Vector3 rotation;

  
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

   

    float speed = 0.0f;

    bool idol = true;

    bool rotatingPlayer = false;
    bool lockPivot = false;

    Quaternion originalRotation;
    Quaternion originalCameraRotation;

    Quaternion originalHorizontalRotation;
    float idolTime;
    int frameCount = 1;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        feet = transform.GetChild(0);

        isGrounded = true;

        movement = Vector3.zero;
        rotation = Vector3.zero;

        originalRotation = transform.localRotation;
        originalCameraRotation = CameraPivot.localRotation;
        originalHorizontalRotation = CameraPivotHorizontal.localRotation;
        prevRotationPlayer = transform.localRotation;
    }

   

    void Update()
    {
        IdolCheck();
        GroundedCheck();
        RotationUpdate();


        IdolMode();
        MovementMode();           
    }



    public float GetSpeed()
    {
        return speed;
    }


    private void IdolCheck()
    {
        idol = !Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && !Input.GetButton("Jump");
    }

    private void GroundedCheck()
    {
        isGrounded = Physics.CheckSphere(feet.transform.position, 0.2f, groundLayer, QueryTriggerInteraction.Ignore);
    }


    Quaternion xQuaternion;
    Quaternion yQuaternion;

    Quaternion prevRotationPlayer;
    Quaternion rotationPlayer;
    Quaternion horizontalRotation;


    private void RotationUpdate()
    {
        rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");



        if (!rotatingPlayer)
        {
            rotationY += Input.GetAxis("Mouse Y") * lookSensitivity;
            rotationX += Input.GetAxis("Mouse X") * lookSensitivity;


            rotationX = ClampAngle(rotationX, horizontalMin, horizontalMax);
            rotationY = ClampAngle(rotationY, verticalMin, verticalMax);

            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            rotationPlayer = originalRotation * xQuaternion;

        }

        CameraPivot.localRotation = originalCameraRotation * yQuaternion;
    }

    #region Idol
    private void IdolMode()
    {
        if (idol)
        {
            idolTime += Time.deltaTime;

            if (lockPivot)
            {
                rotatingPlayer = false;
                

                frameCount = 1;
                originalRotation = transform.localRotation;
                lockPivot = false;
                xQuaternion = originalCameraRotation;
                rotationX = 0;

            }

            CameraPivotHorizontal.localRotation = originalCameraRotation * xQuaternion;
            horizontalRotation = CameraPivotHorizontal.rotation;
        }
    }
    #endregion

    #region movement
    private void MovementMode()
    {
        if (!idol)
        {
            if (!lockPivot)
            {
                CameraPivotHorizontal.localRotation = originalCameraRotation;

                lockPivot = true;

            }

            if (!rotatingPlayer && idolTime > 0.0f)
            {

                idolTime = 0.0f;
                StartCoroutine(controlledRotation(transform.localRotation, rotationPlayer));
            }
            else
            {
                transform.localRotation = Quaternion.Lerp(prevRotationPlayer, rotationPlayer, 1.0f);
                prevRotationPlayer = transform.localRotation;

                headTurnPivot.localRotation = Quaternion.Lerp(headTurnPivot.localRotation, CameraPivotHorizontal.localRotation, 3.0f * Time.deltaTime);
                rigHead.localRotation = Quaternion.Lerp(rigHead.localRotation, CameraPivot.localRotation, 3.0f * Time.deltaTime);

            }

            frameCount += 2;
        }
    }
    #endregion

    #region controllingRotation
    IEnumerator controlledRotation(Quaternion localRotation, Quaternion playerRotation)
    {

        float angleDiff = Quaternion.Angle(localRotation, playerRotation);
        if(angleDiff > 150)
            frameCount *= 4;

        Quaternion rot = localRotation;
        rotatingPlayer = true;
        while (rotatingPlayer)
        {
           

           
            rot = Quaternion.LerpUnclamped(rot, playerRotation, frameCount * Time.deltaTime);

            transform.localRotation = rot;
            if ((rot.eulerAngles == playerRotation.eulerAngles) || (rot == playerRotation))
            {
                break;
            }
            yield return null;
        }
        rotatingPlayer = false;
      
        frameCount = 1;
        yield return null;
    }
    #endregion

    public bool IsGrounded()
    {
        return isGrounded;
    }

   
    private void LateUpdate()
    {
       
        if (lockPivot)
        {
            if (rotatingPlayer)
            {               
                CameraPivotHorizontal.rotation = horizontalRotation;
               
            }          
        }

        #region Headturning
        if (idol)
        {

            float turnY = CameraPivotHorizontal.localRotation.eulerAngles.y;
           
            


            float _boundMin = nfmod(headTurnMin, 360.0f);

           

            if (turnY <= headTurnMax || turnY >= _boundMin) 
            {
                headTurnPivot.localRotation = Quaternion.Lerp(headTurnPivot.localRotation,CameraPivotHorizontal.localRotation, 3.0f * Time.deltaTime);
                rigHead.localRotation = Quaternion.Lerp(rigHead.localRotation,CameraPivot.localRotation, 3.0f * Time.deltaTime);

            }
            else
            {
                headTurnPivot.localRotation = Quaternion.Lerp(headTurnPivot.localRotation, originalHorizontalRotation, 3.0f * Time.deltaTime);
                rigHead.localRotation = Quaternion.Lerp(rigHead.localRotation, originalCameraRotation, 3.0f * Time.deltaTime);
            }
                    
        }
        #endregion
    }


    private void FixedUpdate()
    {
        rigidbody.MovePosition(transform.localPosition);
    }


    #region UtilityFunctions
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

    private float nfmod(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }

    #endregion
}
