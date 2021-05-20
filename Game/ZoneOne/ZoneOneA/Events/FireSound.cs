using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour {


    AudioSource FireLight;
	// Use this for initialization
	void Awake () {

        FireLight = GetComponent<AudioSource>();
        FireLight.time = 1.5f;
        FireLight.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
