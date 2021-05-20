using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {



    public int fireNumber;
    private bool isLit = false;

    public Fire prev;

    public List<Hallway> connectedHallways;
    


   

    public void SetPreviousFire(Fire prev)
    {
        this.prev = prev;
    }



    public void OpensHallways()
    {
        foreach(Hallway h in connectedHallways)
        {
            h.Open();
        }

        ClosesHallways();
    }


    public void ClosesHallways()
    {
        if (prev)
        {
            foreach (Hallway h in prev.connectedHallways)
            {
                if (!connectedHallways.Contains(h))
                {
                    h.Close();
                }
            }
        }
    }

    public void LightFire()
    {

        isLit = true;
        OpensHallways();    
    }

    public void UnLight()
    {
        isLit = false;
        foreach(Hallway h in connectedHallways)
        {
            h.Close();
        }
    }


    public bool IsLit()
    {
        return isLit;
    }

  
	void Awake () {
		
	}
	
	
	void Update () {
		
	}
}
