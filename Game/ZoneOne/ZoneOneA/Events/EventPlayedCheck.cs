using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayedCheck : MonoBehaviour {

    private bool didPlay = false;
	
    public void Played()
    {
        didPlay = true;
    }

    public bool DidPlay()
    {
        return didPlay;
    }

    public void ResetEvent()
    {
        didPlay = false;
    }
  
}
