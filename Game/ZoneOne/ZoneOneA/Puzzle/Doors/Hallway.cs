using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hallway : MonoBehaviour {


    public Door doorOne;
    public Door doorTwo;


    public void Open()
    {
        Debug.Log("Open");
        doorOne.gameObject.SetActive(false);
        doorTwo.gameObject.SetActive(false);
        
    }


    public void Close()
    {
        Debug.Log("Close");
        doorOne.gameObject.SetActive(true);
        doorTwo.gameObject.SetActive(true);
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
