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
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();

    int playerScore = 0;
    int dealerScore = 0;

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
            deck.Discard(card);
            deck.discard.Append(card);
        }
        foreach (Card card in dealerCards)
        {
            deck.Discard(card);
        }
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

        if (playerScore >= 21) CheckWin();

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

        if (dealerScore >= 21) CheckWin();
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

    public void DiscardTest()
    {
        Card card = deck.Draw();
        //deck.Discard(card);
        deck.discard.Append(card);
    }
}
