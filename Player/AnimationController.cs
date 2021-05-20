

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(RigidbodyController))]
[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour {



    Animator animator;
    RigidbodyController controller;

    public float AnimationSpeed;

    float movement = 0f;
   
    float turning;

    float vAxis = 0.0f;
    float hAxis = 0.0f;
    float sprint = 0.0f;
    float jump = 0.0f;



    bool grounded;
    bool sprinting;

	// Use this for initialization
	void Awake () {
       
        animator = GetComponent<Animator>();
        controller = GetComponent<RigidbodyController>();

	}
	
	// Update is called once per frame
	void Update () {


      
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
        sprint = Input.GetAxis("Sprint");
        jump = Input.GetAxis("Jump");
        sprinting = Input.GetButton("Sprint");
        grounded = controller.IsGrounded();


        animator.SetBool("Grounded", grounded);
        animator.SetBool("Sprint", sprinting);


        animator.SetFloat("VerticalAxis", vAxis);
        animator.SetFloat("HorizontalAxis", hAxis);
        animator.SetFloat("Jump", jump);
       
        

        if (sprint > 0)
        {
            if (vAxis < 0)
            {
                animator.SetFloat("VerticalAxis", vAxis - sprint);
            }
            else if (vAxis > 0)
            {
                animator.SetFloat("VerticalAxis", vAxis + sprint);
            }

            if (hAxis < 0)
            {
                animator.SetFloat("HorizontalAxis", hAxis - sprint);
            }
            else if (hAxis > 0)
            {
                animator.SetFloat("HorizontalAxis", hAxis + sprint);
            }
        }



        
    }

   
}
