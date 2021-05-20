using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNextScene : MonoBehaviour {



    public BackgroundManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            manager.LoadScene();
        }    
    }
}
