using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPSPlayer : MonoBehaviour
{
    [SerializeField] List<GameObject> hand = new List<GameObject>();

    [HideInInspector] public int CardsInHand = 13;

    

    void Update()
    {
        
    }

}
