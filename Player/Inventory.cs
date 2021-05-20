using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public string[] interactableTags;
    public GameObject InteractAlert;

    public float CastDistance = 1.5f;
    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray cam = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool interacted = false;
        if (Physics.Raycast(cam, out hit, CastDistance))
        {
        
            foreach (string tag in interactableTags)
            {
                if (tag == hit.transform.tag)
                {
                    InteractAlert.SetActive(true);
                    interacted = true;
                }
            }
        }
        else
        {
            interacted = false;
        }

        if (!interacted)
        {
            InteractAlert.SetActive(false);
        }



    }
}
