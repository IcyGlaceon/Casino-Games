using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PointCards : MonoBehaviour
{
    [SerializeField] List<GameObject> cards = new List<GameObject>();
    [SerializeField] GameObject PointCardUI;
    [SerializeField] GOPSPlayer player;
    [SerializeField] GOPSAI AI;

    GameObject currentPointCard;

    int RandPointCard;
    [HideInInspector] public int PointCardValue;

    void Start()
    {
        NextCard();
    }

    public void NextCard()
    {
        RandPointCard = Random.Range(0, cards.Count);

        currentPointCard = cards[RandPointCard];

        PointCardValue = currentPointCard.GetComponent<Card>().value;

        PointCardUI.GetComponent<Image>().sprite = currentPointCard.GetComponent<Card>().cardFront;

        cards.Remove(currentPointCard);
    }



}
