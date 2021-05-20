using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFourEventTrigger : MonoBehaviour {


   

    public Animation OutsideDoorOpen;
    public Animation RoomDoorOneClose;
    public string RDClipOneClose;

    public Animation RoomDoorTwoClose;
    public string RDClipTwoClose;
    // Use this for initialization

    bool trigger = false;

    FireEventObject EventFireLit;

    void Awake () {
        EventFireLit = GetComponent<FireEventObject>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!trigger)
        {
            if (EventFireLit)
            {
                if (EventFireLit.IsActivated())
                {
                    trigger = true;
                    OutsideDoorOpen.Play();
                    RoomDoorTwoClose.Play(RDClipOneClose);
                    RoomDoorOneClose.Play(RDClipTwoClose);
                }
            }
        }

	}
}
