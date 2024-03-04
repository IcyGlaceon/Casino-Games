using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] TMP_Text PlayerCardsLeft;
    [SerializeField] TMP_Text EnemyCardsLeft;

    bool playerTurn = true;
    float timer;
    List<Card> cardsInCenter = new List<Card>();
    float textTimer;

    void Start()
    {
        centerCard.sprite = null;
        winLose_Txt.gameObject.SetActive(false);
        ShuffleAndDeal();
        timer = AITurnTime;
        textTimer = 2;
        playerTurn = true;
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
        if (winLose_Txt.text.Contains("Jack"))
        {
            textTimer -= Time.deltaTime;
            if (textTimer < 0)
            {
                winLose_Txt.gameObject.SetActive(false);
            }
        }
        PlayerCardsLeft.text = playerHand.Count.ToString();
        EnemyCardsLeft.text = enemyHand.Count.ToString();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            textTimer = 2;
            if (centerCard.sprite.name.Contains("J"))
            {
                winLose_Txt.text = "You Slapped Jack!";
                winLose_Txt.gameObject.SetActive(true);
                centerCard.sprite = null;
                foreach(Card card in cardsInCenter)
                {
                    enemyHand.Add(card);
                }
            }
            else
            {
                winLose_Txt.text = "You Didn't Slap Jack!";
                winLose_Txt.gameObject.SetActive(true);
                centerCard.sprite = null;
                foreach (Card card in cardsInCenter)
                {
                    playerHand.Add(card);
                }
            }
            cardsInCenter.Clear();
        }

        if (!playerTurn)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                if (enemyHand.Count != 0)
                {
                    centerCard.sprite = enemyHand[0].cardFront;
                    cardsInCenter.Add(enemyHand[0]);
                    enemyHand.RemoveAt(0);
                    timer = AITurnTime;
                    playerTurn = true;
                    StartCoroutine(AI_Slap());
                }
            }
        }
    }

    IEnumerator AI_Slap()
    {
        int chance = Random.Range(1, 16);

        if(chance == 1)
        {
            yield return new WaitForSeconds(1);
            textTimer = 2;
            if(centerCard.sprite != null)
            {
                if (centerCard.sprite.name.Contains("J"))
                {
                    winLose_Txt.text = "AI Slapped Jack!";
                    winLose_Txt.gameObject.SetActive(true);
                    centerCard.sprite = null;
                    foreach (Card card in cardsInCenter)
                    {
                        playerHand.Add(card);
                    }
                }
                else
                {
                    winLose_Txt.text = "AI Didn't Slap Jack!";
                    winLose_Txt.gameObject.SetActive(true);
                    centerCard.sprite = null;
                    foreach (Card card in cardsInCenter)
                    {
                        enemyHand.Add(card);
                    }
                }
                cardsInCenter.Clear();
            }
        }
    }

    public void Click_Deck()
    {
        if (playerTurn)
        {
            if(playerHand.Count != 0)
            {
                centerCard.sprite = playerHand[0].cardFront;
                cardsInCenter.Add(playerHand[0]);
                playerHand.RemoveAt(0);
                playerTurn = false;
            }
        }
        StartCoroutine(AI_Slap());
    }

    public void ShuffleAndDeal()
    {
        playerHand.Clear();
        enemyHand.Clear();
        deck.Shuffle();
        int halfDeck = deck.cards.Count / 2;
        for (int i = 0; i < halfDeck; i++)
        {
            playerHand.Add(deck.Draw());
            enemyHand.Add(deck.Draw());
        }
    }
}
