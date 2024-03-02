using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SlapJackManager : MonoBehaviour
{
    [SerializeField] TMP_Text winLose_Txt;
    [SerializeField] DeckOfCards deck;
    [SerializeField] List<Card> playerHand;
    [SerializeField] List<Card> enemyHand;
    [SerializeField] SpriteRenderer centerCard;
    [SerializeField] float AITurnTime;

    bool playerTurn = true;
    float timer;

    void Start()
    {
        centerCard.sprite = null;
        winLose_Txt.gameObject.SetActive(false);
        ShuffleAndDeal();
        timer = AITurnTime;
    }

    void Update()
    {
        if (playerHand.Count == 0)
        {
            winLose_Txt.text = "You Win!";
            winLose_Txt.gameObject.SetActive(true);
        }
        if (enemyHand.Count == 0)
        {
            winLose_Txt.text = "You Lose!";
            winLose_Txt.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(centerCard.sprite.name.Contains("J"))
            {
                winLose_Txt.text = "You Slapped Jack!";
                winLose_Txt.gameObject.SetActive(true);
            }
            else
            {

            }
        }

        if (!playerTurn)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (enemyHand.Count != 0)
                {
                    centerCard.sprite = enemyHand[1].cardFront;
                    enemyHand.RemoveAt(1);
                    timer = AITurnTime;
                    playerTurn = true;
                }
            }
        }
    }

    public void Click_Deck()
    {
        if (playerTurn)
        {
            if(playerHand.Count != 0)
            {
                centerCard.sprite = playerHand[1].cardFront;
                playerHand.RemoveAt(1);
                playerTurn = false;
            }
        }
    }

    public void ShuffleAndDeal()
    {
        deck.Shuffle();
        int halfDeck = deck.cards.Count / 2;
        for (int i = 1; i < halfDeck; i++)
        {
            playerHand.Add(deck.Draw());
            enemyHand.Add(deck.Draw());
        }
    }
}
