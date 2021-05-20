using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEventObject : MonoBehaviour {

    public FireManager FireManager;

    public int FireNumber;

    public GameObject[] EventDoors;

    public GameObject[] OtherEventObjects;
    public string OtherEventAnimClipName;
    public string OtherEventAnimClipNameTwo;

    public GameObject[] OtherEventObjects2;

    public string OtherEvent2AnimClipName;
    public string OtherEvent2AnimClipNameTwo;

    public GameObject FireOnGoblet;
    public GameObject FireOffGoblet;
    public GameObject FinalFirePillar;

    public GameObject[] Torches;

    private bool activated = false;

    public void Activate()
    {
        FireOnGoblet.SetActive(true);
        FireOffGoblet.SetActive(false);
        EventPlayedCheck checkEvent;
        foreach(GameObject go in EventDoors)
        {
            Animation openAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (!checkEvent.DidPlay())
                {
                    openAnim.Play("DungeonDoorOpen");
                    checkEvent.Played();
                }
            }
           
        }

        foreach(GameObject go in OtherEventObjects)
        {
            Animation otherAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (!checkEvent.DidPlay())
                {
                    otherAnim.Play(OtherEventAnimClipName);
                    checkEvent.Played();
                }
            }
        }


        foreach (GameObject go in OtherEventObjects2)
        {
            Animation otherAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (!checkEvent.DidPlay())
                {
                    otherAnim.Play(OtherEvent2AnimClipName);
                    checkEvent.Played();
                }
            }
        }

        foreach(GameObject go in Torches)
        {
            go.SetActive(true);
        }

        FinalFirePillar.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        FinalFirePillar.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        FireManager.AddFireToSequence(FireNumber);

        activated = true;
    }

    public void Deactivate()
    {
        EventPlayedCheck checkEvent;
        foreach (GameObject go in EventDoors)
        {
            Animation openAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (checkEvent.DidPlay())
                {
                    if (go.tag != "DontClose")
                    {
                        openAnim.Play("DungeonDoorClose");
                        checkEvent.ResetEvent();
                    }
                    
                }
            }

        }

        foreach (GameObject go in OtherEventObjects)
        {
            Animation otherAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (checkEvent.DidPlay())
                {
                    otherAnim.Play(OtherEventAnimClipNameTwo);
                    checkEvent.ResetEvent();
                }
            }
        }


        foreach (GameObject go in OtherEventObjects2)
        {
            Animation otherAnim = go.GetComponent<Animation>();
            checkEvent = go.GetComponent<EventPlayedCheck>();
            if (checkEvent)
            {
                if (!checkEvent.DidPlay())
                {
                    otherAnim.Play(OtherEvent2AnimClipNameTwo);
                    checkEvent.ResetEvent();
                }
            }
        }

        foreach (GameObject go in Torches)
        {
            go.SetActive(false);
        }
        FireOnGoblet.SetActive(false);
        FireOffGoblet.SetActive(true);
        FinalFirePillar.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        FinalFirePillar.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        activated = false;
    }



    public bool IsActivated()
    {
        return activated;
    }
}
