using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastFire : MonoBehaviour {

    public FireManager manager;



    public bool light = false;
    public Animation anim;
    bool isLit = false;

    bool playAnim = false;
	// Use this for initialization
	void Awake () {
	}
	
	// Update is called once per frame
	void Update () {
		if(manager.FireLitCount() == 6)
        {
            if (!playAnim)
            {
                anim.Play("LastFireRise");
                playAnim = true;
            }
        }
	}

    public void LightLastFire()
    {
        if (!isLit)
        {
           
            light = true;
            if (manager.Check())
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                anim.Play("LastFireClose");
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                StartCoroutine(Deactivate());
            }
            isLit = true;
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(2.0f);
        manager.ResetSequence();
        isLit = false;
        playAnim = false;
    }
}
