using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour {


    public int SceneBuildIndex = 1;
    AsyncOperation asyncLoad = null;
	
	void Awake () {
        StartCoroutine(LoadAsyncScene());
	}
	
	
	void Update () {
		
	}


    public void LoadScene()
    {
        if(asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }

    IEnumerator LoadAsyncScene()
    {
        yield return null;
        asyncLoad = SceneManager.LoadSceneAsync(SceneBuildIndex);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
