using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ActivateFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray cam = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(cam,out hit,1.0f))  
            {
                if(hit.transform.tag == "Fire")
                {
                    FireEventObject feo = hit.transform.GetComponent<FireEventObject>();
                    feo.Activate();
                }else if (hit.transform.tag == "LastFire")
                {
                    LastFire lf = hit.transform.GetComponent<LastFire>();
                    lf.LightLastFire();
                }
            }

        }

	}
}
