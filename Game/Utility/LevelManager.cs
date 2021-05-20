using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class LevelManager : MonoBehaviour {


    public bool EnableBackgroundUse;
    public bool LockCursor;


    bool paused = false;

	// Use this for initialization
	void Awake () {
        if(LockCursor)
            Cursor.lockState = CursorLockMode.Locked;

        if (EnableBackgroundUse)
            Application.runInBackground = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Cancel"))
        {
            if (!paused)
            {
                paused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                paused = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;             
            }
        }

         
	}
}
