using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerTrigger : MonoBehaviour {
    public LastPlaceablePosition LastPlacedPosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            LastPlacedPosition.StartReset();
        }
    }
}
