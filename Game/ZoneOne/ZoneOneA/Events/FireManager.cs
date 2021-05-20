using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

    List<int> fireSequence = new List<int>();

    public GameObject[] Fires;
    public GameObject[] LastRoomFires;
    public AudioSource TriggerSound;
  

    private void Awake()
    {

    }

    private void Update()
    {
        
    }

    public int FireLitCount()
    {
        return fireSequence.Count;
    }

    public void AddFireToSequence(int number)
    {
        if(fireSequence.Count < 6)
            fireSequence.Add(number);
    }

    public bool Check()
    {
        bool sequence = false;
        if(fireSequence.Count == 6)
        {
            if(fireSequence[0] == 1)
                if(fireSequence[1] == 2)
                    if (fireSequence[2] == 3)
                        if (fireSequence[3] == 4)
                            if(fireSequence[4] == 5)
                                if(fireSequence[5] == 6)
                                {
                                    sequence = true;
                                    TriggerSound.Play();
                                }
                        

                                     
        }
        return sequence;
    }


    public void ResetSequence()
    {
        
        foreach(GameObject go in Fires)
        {
            go.GetComponent<FireEventObject>().Deactivate();
        }

        foreach(GameObject go in LastRoomFires)
        {
           go.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
           go.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        fireSequence.Clear();
    }


}
