using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GOPSPlayer : MonoBehaviour
{
    [SerializeField] List<GameObject> hand = new List<GameObject>();

    [SerializeField] TMP_Text PlayerPointsUI;

    [SerializeField] GOPSAI AI;

    [SerializeField] GameObject GameOver;
    [SerializeField] GameObject GameLost;
    [SerializeField] GameObject GameWon;

    [HideInInspector] public int CardsInHand = 13;
    [HideInInspector] public int Points = 0;

    

    void Update()
    {
        PlayerPointsUI.text = Points.ToString();

        if(CardsInHand == 0)
        {
            GameOver.SetActive(true);
            if (AI.Points > Points)
            {
                GameWon.SetActive(false);
                GameLost.SetActive(true);
            }
            else
            {
                GameLost.SetActive(false);
                GameWon.SetActive(true);
            }
        }
    }

}
