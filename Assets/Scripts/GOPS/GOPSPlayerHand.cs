using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GOPSPlayerHand : MonoBehaviour
{
    public void CardUp()
    {
        gameObject.transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y + 10, transform.GetChild(0).position.z);
    }

    public void CardDown() 
    {
        gameObject.transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y - 10, transform.GetChild(0).position.z);
    }

}
