using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game.Events
{

    public class PlayerMiscEventManager : MonoBehaviour
    {


        Camera playerCam;
        public float CastDistance;
        public GameObject[] EventObjects;

        // Use this for initialization
        void Awake()
        {
            playerCam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {


            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit = new RaycastHit();

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit, CastDistance);


                if (hit.transform)
                {
                    string hitTag = hit.transform.tag;
                    foreach (GameObject go in EventObjects)
                    {
                        string tag = go.tag;
                        if (tag == hitTag)
                        {

                            if (GameObjectUT.HasComponent(go, "MiscEvent"))
                            {
                                TimelineEvent miscEvent = go.GetComponent<TimelineEvent>();
                                miscEvent.Play();
                            }
                        }
                    }
                }

            }

        }
    }
}
