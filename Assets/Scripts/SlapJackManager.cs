using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SlapJackManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text winLose_Txt;
    [SerializeField] SpriteRenderer centerCard;
    [SerializeField] TMP_Text PlayerCardsLeft;
    [SerializeField] TMP_Text EnemyCardsLeft;
    [SerializeField] float AITurnTime;
    [SerializeField] GameObject PlayerDeckButton;
    [SerializeField] GameObject RestartButton;
    [Header("Functionality")]
    [SerializeField] DeckOfCards deck;

    List<Card> playerHand = new List<Card>();
    List<Card> enemyHand = new List<Card>();
    List<Card> cardsInCenter = new List<Card>();
    bool playerTurn = true;
    float timer;
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
            playerTurn = true;
            PlayerDeckButton.SetActive(false);
            RestartButton.SetActive(true);
        }
        if (enemyHand.Count == 0)
        {
            winLose_Txt.text = "You Lose!";
            winLose_Txt.gameObject.SetActive(true);
            playerTurn= true;
            PlayerDeckButton.SetActive(false);
            RestartButton.SetActive(true);
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
        if (playerHand.Count == 0 || enemyHand.Count == 0) yield break;
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
