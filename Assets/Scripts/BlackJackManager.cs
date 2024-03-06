using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BlackJackManager : MonoBehaviour
{
    [SerializeField] DeckOfCards deck;
    [SerializeField] TMP_Text winText;
    [SerializeField] TMP_Text dealerText;
    [SerializeField] TMP_Text playerText;
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject dealerPos;
    [SerializeField] List<SpriteRenderer> playerSprites;
    [SerializeField] List<SpriteRenderer> dealerSprites;
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();

    int playerScore = 0;
    int dealerScore = 0;

    int dealerCurrent = 0;
    int playerCurrent = 0;

    void Start()
    {
        deck.Shuffle();
        StartGame();
    }


    void Update()
    {

    }

    //Resets scores, clears current hands, and reshuffles cards back into the deck
    public void StartGame()
    {
        playerScore = 0;
        dealerScore = 0;
        foreach (Card card in playerCards)
        {
            deck.discard.Add(card);
            //deck.Discard(card);
            //deck.discard.Append(card);
        }
        playerCards.Clear();
        foreach (Card card in dealerCards)
        {
            deck.discard.Add(card);
            //deck.Discard(card);
        }
        dealerCards.Clear();
        deck.ReturnToDeck(true);
        StartDeal();
    }

    public void Stay()
    {
        DealerTurn();
    }

    //Deals player a card then adds value to score and checks for blackjack or bust
    public void DealPlayer()
    {
        Card card = deck.Draw();

        playerCards.Add(card);
        if (card.value > 10)
        {
            playerScore += 10;
        }
        else playerScore += card.value;

        playerText.text = playerScore.ToString();
        playerPos.transform.position.x.Equals(playerPos.transform.position.x + 10);
        CheckScore(playerCards);

    }

    //Deals dealer a card then adds value to score and checks for blackjack or bust
    void DealDealer()
    {
        Card card = deck.Draw();

        dealerCards.Add(card);
        if (card.value > 10)
        {
            dealerScore += 10;
        }
        else dealerScore += card.value;

        dealerText.text = dealerScore.ToString();

        PlaceCard(dealerPos, card, true);
        dealerPos.transform.position.x.Equals(dealerPos.transform.position.x + 10);
        CheckScore(dealerCards);
    }

    //First hands dealt when game starts
    void StartDeal()
    {
        DealDealer();
        DealDealer();
        DealPlayer();
        DealPlayer();
    }

    void DealerTurn()
    {
        while (dealerScore <= 16)
        {
            DealDealer();
        }

        CheckWin();
    }

    void CheckScore(List<Card> cards)
    {
        //cards.Sort();
        List<Card> sortedCards = cards.OrderBy(card => card.value).ToList();
        int score = 0;
        foreach (Card card in sortedCards)
        {
            if (card.value == 1)
            {
                if (score + 11 > 21)
                {
                     score += 1;
                }
                else score += 11;
            }

            else if (card.value > 10)
            {
                score += 10;
            }

            else score += card.value;
        }

        if (score >= 21) CheckWin();

    }

    //Checks who won the game
    void CheckWin()
    {
        if (playerScore > dealerScore && playerScore <= 21 || dealerScore > 21)
        {
            winText.text = "Player Wins!";
        }
        else if (playerScore == dealerScore)
        {
            winText.text = "Tie game";
        }
        else
        {
            winText.text = "Dealer Wins!";
        }
    }

    void PlaceCard(GameObject cardPos, Card card, bool flipped = true)
    {
        Card displayCard = card;
        if (flipped)
        {
            //displayCard.spriteRenderer.sprite = displayCard.cardFront;
            displayCard.Flip();
        }
        else
        {
            displayCard.spriteRenderer.sprite = card.cardBack;
        }
        displayCard.spriteRenderer.transform.position = cardPos.transform.position;
        Instantiate(displayCard);
        //card.spriteRenderer.sprite = card.cardBack;
    }

    //void PlaceCard(List<SpriteRenderer> renderers, Card card, bool flipped = true)
    //{
    //    renderers()
    //}
}
