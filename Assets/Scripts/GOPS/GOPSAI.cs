using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GOPSAI : MonoBehaviour
{
    [SerializeField] List<GameObject> hand = new List<GameObject>();

    [HideInInspector] public GameObject Card;




    public void SelectCard()
    {
        Card = hand[Random.Range(0, hand.Count)];

        hand.Remove(Card);

        Card.SetActive(false);
    }
}
