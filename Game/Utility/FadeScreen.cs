using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour {


    public bool FadeIn = true;
    public bool FadeOut = false;
    public Image fadeImage;

    public bool StartOnAwake = false;

    bool faded = false;

    public bool OnTrigger = true;

    public float SpeedAdditive = 0.0f;

	// Use this for initialization
	void Awake () {

        Color c = fadeImage.color;

        if (FadeIn)
        {
            FadeOut = false;
           
            c.a = 0;
            fadeImage.color = c;
        }
        if (FadeOut)
        {
            FadeIn = false;
            c.a = 1;
            fadeImage.color = c;
        }

        if (StartOnAwake)
        {
            StartFade();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartFade()
    {
        if (!faded)
        {
            faded = true;
            StartCoroutine(Fade());

        }
    }

    public void FadeInCall()
    {
        FadeIn = true;
        FadeOut = false;
        StartCoroutine(Fade());
    }

    public void FadeOutCall()
    {
        FadeIn = false;
        FadeOut = true;
        StartCoroutine(Fade());
    }

    public void SetSpeedAdditive(float speed)
    {
        SpeedAdditive = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnTrigger)
        {
            if(other.tag == "Player")
            {
                StartFade();
            }
        }
    }

    IEnumerator Fade()
    {
        yield return null;
        if (FadeIn)
        {
            Color c = fadeImage.color;
            float f = 0.0f;
            while(f < 1.0f)
            {
                f += (0.5f + SpeedAdditive) * Time.deltaTime;
                c.a = f;
                fadeImage.color = c;
                yield return null;
            }
          
        }
        else if (FadeOut)
        {
            

            Color c = fadeImage.color;
            float f = 1.0f;
            while (f > 0.0f)
            {
           
                f -= (0.25f + SpeedAdditive) * Time.deltaTime;
             
                c.a = f;
                fadeImage.color = c;
                yield return null;
            }
        }
    }
}
