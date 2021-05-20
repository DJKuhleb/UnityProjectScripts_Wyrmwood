using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePuzzle : MonoBehaviour {


    int numberOfFires = 6;
    int numberLit = 0;

    public bool LightFireOne;
    public bool LightFireTwo;
    public bool LightFireThree;
    public bool LightFireFour;
    public bool LightFireFive;
    public bool LightFireSix;

    public bool Light;
    public bool ClearSequence;


    public Fire FireOne;
    public Fire FireTwo;
    public Fire FireThree;
    public Fire FireFour;
    public Fire FireFive;
    public Fire FireSix;


    List<Fire> currentSequence = new List<Fire>();

    public void AddFireToSequence(Fire fire)
    {
        if(currentSequence.Count < 7)
        {
            if (!currentSequence.Contains(fire))
            {
                if (currentSequence.Count > 0)
                {
                    Fire prev = currentSequence[currentSequence.Count-1];
                    fire.SetPreviousFire(prev);
                }
                currentSequence.Add(fire);
                numberLit++;
            }
        }
    }

    public int UpdateFireCount()
    {
        int fireCount = 0;

        
        if (FireOne.IsLit())
        {
            fireCount++;
        }
        if (FireTwo.IsLit())
        {
            fireCount++;
        }
        if (FireThree.IsLit())
        {
            fireCount++;
        }
        if (FireFour.IsLit())
        {
            fireCount++;
        }
        if (FireFive.IsLit())
        {
            fireCount++;
        }
        if (FireSix.IsLit())
        {
            fireCount++;
        }

        return fireCount;
    }

	
	void Start () {
		
	}
	

	void Update () {


        if (Light)
        {
            if (LightFireOne)
            {
               
                AddFireToSequence(FireOne);
                FireOne.LightFire();
            }
            else
            {
               
            }

            if (LightFireTwo)
            {
                
                AddFireToSequence(FireTwo);
                FireTwo.LightFire();
            }
            else
            {
                

            }

            if (LightFireThree)
            {
                
                AddFireToSequence(FireThree);
                FireThree.LightFire();
            }
            else
            {
                
            }

            if (LightFireFour)
            {
                
                AddFireToSequence(FireFour);
                FireFour.LightFire();
            }
            else
            {
               
            }

            if (LightFireFive)
            {
                AddFireToSequence(FireFive);
                FireFive.LightFire();
            }
            else
            {
                
            }

            if (LightFireSix)
            {

              
                AddFireToSequence(FireSix);
                FireSix.LightFire();
            }
            else
            {
               
            }

           

            Light = false;
        }
        if (ClearSequence)
        {
            FireOne.UnLight();
            FireTwo.UnLight();
            FireThree.UnLight();
            FireFour.UnLight();
            FireFive.UnLight();
            FireSix.UnLight();

            currentSequence.Clear();
            ClearSequence = false;
        }
        UpdateFireCount();

       
	}
}
