using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GOPSCardFunction : MonoBehaviour
{
    [SerializeField] GOPSPlayer player;
    [SerializeField] GOPSAI AI;
    [SerializeField] PointCards PointCards;
    [SerializeField] GameObject PlayerCardUI;
    [SerializeField] Sprite CardBack;
    [SerializeField] int Value;



    public void OnCardClick()
    {
        AI.SelectCard();

        PlayerCardUI.GetComponent<Image>().sprite = this.GetComponent<Image>().sprite;

        if (AI.CardValue + 1 > Value)
        {
            Debug.Log("AI");
            AI.Points += PointCards.PointCardValue;

            if (player.CardsInHand > 1)
            {
                PointCards.NextCard();
            }
        }
        else
        {
            Debug.Log("Player");
            player.Points += PointCards.PointCardValue;

            if (player.CardsInHand > 1)
            {
                PointCards.NextCard();
            }
        }

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


    IEnumerator Delay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
    }
}
