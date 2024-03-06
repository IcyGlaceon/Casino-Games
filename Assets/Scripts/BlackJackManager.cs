using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BlackJackManager : MonoBehaviour
{
    [SerializeField] DeckOfCards deck;
    [SerializeField] TMP_Text winText;
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject dealerPos;
    [SerializeField] List<SpriteRenderer> playerSprites;
    [SerializeField] List<SpriteRenderer> dealerSprites;
    [SerializeField] Button hitButton;
    [SerializeField] Button stayButton;
    [SerializeField] GameObject tutorial;
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();

    Card dealerFaceDown = null;

    int playerScore = 0;
    int dealerScore = 0;

    int dealerCurrent = 0;
    int playerCurrent = 0;

    void Start()
    {
        deck.Shuffle();
        StartGame();
    }

    //Resets scores, clears current hands, and reshuffles cards back into the deck
    public void StartGame()
    {
        winText.text = "";
        playerScore = 0;
        dealerScore = 0;
        foreach (Card card in playerCards)
        {
            deck.discard.Add(card);
        }
        foreach (SpriteRenderer spriteRenderer in playerSprites)
        {
            spriteRenderer.sprite = null;
        }
        playerCurrent = 0;
        playerCards.Clear();
        foreach (Card card in dealerCards)
        {
            deck.discard.Add(card);
        }
        foreach (SpriteRenderer spriteRenderer in dealerSprites)
        {
            spriteRenderer.sprite = null;
        }
        dealerCurrent = 0;
        dealerCards.Clear();
        deck.ReturnToDeck(true);
        hitButton.enabled = true;
        stayButton.enabled = true;
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
        PlaceCardPlayer(card);
        playerScore = CheckScore(playerCards);
    }

    //Deals dealer a card then adds value to score and checks for blackjack or bust
    void DealDealer(bool flipped = true)
    {
        Card card = deck.Draw();
        dealerCards.Add(card);
        PlaceCardDealer(card, flipped);
        dealerScore = CheckScore(dealerCards);
    }

    //First hands dealt when game starts
    void StartDeal()
    {
        DealDealer(false);
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

    int CheckScore(List<Card> cards)
    {
        //cards.Sort();
        List<Card> sortedCards = cards.OrderBy(card => card.value).ToList();
        sortedCards.Reverse();
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

        return score;
    }

    //Checks who won the game
    void CheckWin()
    {
        dealerSprites[0].sprite = dealerFaceDown.cardFront;
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
        hitButton.enabled = false;
        stayButton.enabled = false;
    }

    void PlaceCardDealer(Card card, bool flipped = true)
    {
        if (flipped) dealerSprites[dealerCurrent].sprite = card.cardFront;
        else
        {
            dealerSprites[dealerCurrent].sprite = card.cardBack;
            dealerFaceDown = card;
        }

        dealerCurrent++;
    }

    void PlaceCardPlayer(Card card, bool flipped = true)
    {
        if (flipped) playerSprites[playerCurrent].sprite = card.cardFront;

        playerCurrent++;
    }

    public void HowToPlay()
    {
        tutorial.SetActive(true);
    }
    public void BackToGame()
    {
        tutorial.SetActive(false);
    }
}
