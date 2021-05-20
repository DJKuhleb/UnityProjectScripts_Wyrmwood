using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlaceablePosition : MonoBehaviour {


    public float SaveInterval = 15.0f;

    public ClimbLadder ladder;

    public AudioSource OnResetPlay;

    public FadeScreen Screen;

    Vector3 savePosition;
    Vector3 originalPosition;
    Quaternion saveRotation;
    RigidbodyController controller;
    bool isSaving = false;
    bool resetting = false;
	// Use this for initialization
	void Awake () {
        controller = GetComponent<RigidbodyController>();
        savePosition = transform.position;
        saveRotation = transform.rotation;
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!resetting)
        {
            SaveLastProbablePosition();
        }

        if (!controller)
        {
            try
            {
                controller = GetComponent<RigidbodyController>();
            }catch(Exception e)
            {

            }
        }
    }

    void SaveLastProbablePosition()
    {
        if (!ladder.climbing)
        {
            if (controller)
            {
                if (controller.IsGrounded())
                {
                    if (!isSaving)
                    {
                        StartCoroutine(SavePosition());
                    }
                }
            }
        }
        else
        {
            if (!controller)
            {
                controller = GetComponent<RigidbodyController>();
            }
        }
    }

    public void StartReset()
    {
        if(!resetting)
            StartCoroutine(ResetToProbablePosition());
    }

    IEnumerator ResetToProbablePosition()
    {

        

        OnResetPlay.Play();
        resetting = true;
        controller.enabled = false;
        Screen.SetSpeedAdditive(1.5f);
        Screen.FadeInCall();
        yield return new WaitForSeconds(2.0f);
        transform.position = savePosition;
        transform.rotation = saveRotation;
        yield return new WaitForSeconds(0.5f);
        Screen.FadeOutCall();
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
        resetting = false;
    }

    IEnumerator SavePosition() {
        isSaving = true;
        savePosition = transform.position;
        saveRotation = transform.rotation;
        yield return new WaitForSeconds(SaveInterval);
        isSaving = false;
    }

    
}
