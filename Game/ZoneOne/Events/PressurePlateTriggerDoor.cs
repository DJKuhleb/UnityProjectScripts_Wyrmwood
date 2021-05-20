using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateTriggerDoor : MonoBehaviour {


    public GameObject door;
    public GameObject pressurePlate;
    public AudioSource activatedSound;
    public AudioSource doorOpening;

    private bool activated = false;

	
    private void OnTriggerEnter(Collider other)
    {
        
        if (!activated)
        {
            Debug.Log(other.tag);
            if (other.tag == "Player")
            {
                StartCoroutine(playDelayAnimations());              
                activated = true;
            }
        }
        
    }

    IEnumerator playDelayAnimations()
    {

        if (pressurePlate)
        {
            pressurePlate.GetComponent<Animation>().Play();
            activatedSound.Play();
        }

        yield return new WaitForSeconds(2.0f);

        if (door)
        {
            door.GetComponent<Animation>().Play();
            doorOpening.Play();
        }

        yield return null;
    }

    private void Update()
    {
        if(activatedSound.time > 1.0f)
        {
            activatedSound.Stop();
        }
    }
}
