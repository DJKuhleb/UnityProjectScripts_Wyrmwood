using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour {

    public Animation animation;
    public string ClipName;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Go()
    {
        if (animation)
        {
            if (ClipName != "")
            {
                animation.Play(ClipName);
            }
            else
            {
                animation.Play();
            }
        }
    }
}
