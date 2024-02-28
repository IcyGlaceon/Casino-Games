using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPSCardFunction : MonoBehaviour
{
    [SerializeField] GOPSPlayer player;
    [SerializeField] GOPSAI AI;


    public void OnCardClick()
    {
        AI.SelectCard();












        player.CardsInHand--;


        this.gameObject.SetActive(false);
    }

    public void CardUp()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);
    }

    public void CardDown()
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
    }
}
