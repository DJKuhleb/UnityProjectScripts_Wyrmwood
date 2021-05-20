using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUT {

	

    public static bool HasComponent(GameObject gameObject, string componentName)
    {
        bool result = true;

        try
        {
            Component comp = gameObject.GetComponent(componentName);
        }catch(Exception e)
        {
            result = false;
        }

        return result;
    }

}
