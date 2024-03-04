using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GOPSAI : MonoBehaviour
{
    [SerializeField] List<GameObject> hand = new List<GameObject>();

    [SerializeField] public GameObject AICardUI;

    [HideInInspector] public GameObject Card;
    [HideInInspector] public int CardValue;
    [HideInInspector] public int Points = 0;

    public void SelectCard()
    {
        CardValue = Random.Range(0, hand.Count);

        Card = hand[CardValue];

        AICardUI.GetComponent<Image>().sprite = Card.GetComponent<Image>().sprite;
        
        hand.Remove(Card);

        Card.SetActive(false);
    }
}
