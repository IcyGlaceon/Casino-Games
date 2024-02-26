using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BlackJackManager : MonoBehaviour
{
    [SerializeField] DeckOfCards deck;
    [SerializeField] TextMeshPro winText;
    List<Card> playerCards = new List<Card>();
    List<Card> dealerCards = new List<Card>();

    int playerScore = 0;
    int dealerScore = 0;

    void Start()
    {
        deck.Shuffle();
    }


    void Update()
    {
        
    }

    //Resets scores, clears current hands, and reshuffles cards back into the deck
    void StartGame()
    {
        playerScore = 0;
        dealerScore = 0;
        foreach (Card card in playerCards) 
        {
            deck.Discard(card);
        }
        foreach (Card card in dealerCards)
        {
            deck.Discard(card);
        }
        deck.ReturnToDeck(true);
        StartDeal();
    }

    //Deals player a card then adds value to score and checks for blackjack or bust
    void DealPlayer()
    {
        Card card = deck.Draw();
        
        playerCards.Add(card);
        playerScore += card.value;

        if (playerScore >= 21) CheckWin();

    }

    //Deals dealer a card then adds value to score and checks for blackjack or bust
    void DealDealer()
    {
        Card card = deck.Draw();

        dealerCards.Add(card);
        dealerScore += card.value;

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

    //Checks who won the game
    void CheckWin()
    {
        if (playerScore > dealerScore && playerScore <= 21)
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


}
