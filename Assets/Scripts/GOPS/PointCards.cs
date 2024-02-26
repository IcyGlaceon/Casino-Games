using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCards : MonoBehaviour
{
    [SerializeField] List<GameObject> cards = new List<GameObject>();
    [SerializeField] GOPSPlayer player;
    [SerializeField] GOPSAI AI;

    GameObject currentPointCard;
    GameObject nextPointCard;

    // Start is called before the first frame update
    void Start()
    {
        currentPointCard = cards[Random.Range(0, (cards.Count - 1))];

    }





}
