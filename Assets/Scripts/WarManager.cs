using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    [SerializeField] DeckOfCards PlayableCards;
	[SerializeField] TMP_Text Player1Text;
	[SerializeField] TMP_Text Player2Text;
	[SerializeField] SpriteRenderer Player1;
	[SerializeField] SpriteRenderer Player2;
    List<Card> Player1Cards = new List<Card>();
    List<Card> Player2Cards = new List<Card>();
	private bool CardsOut = false;
	private RoundWinner Winner;
	private List<Card> Round = new List<Card>();

	public void Start()
	{
		PlayableCards.Shuffle();
		FillPlayerHands();
		Player1.enabled = false;
		Player2.enabled = false;
	}

	public void Update()
	{
		DisplayHands();
	}

	private void FillPlayerHands()
	{
		int length = (PlayableCards.cards.Count / 2);
		for (int i = 0; i < length; i++) 
		{
			Player1Cards.Add(PlayableCards.Draw());
			Player2Cards.Add(PlayableCards.Draw());
		}
	}

	// show a stack of face down cards with the number of cards in the hand on top
	private void DisplayHands()
	{
		Player1Text.text = Player1Cards.Count.ToString();
		Player2Text.text = Player2Cards.Count.ToString();
	}

	// show the cards face up that each player played
	private void DisplayRound()
	{
		Player1.sprite = Player1Cards[0].cardFront;
		Player2.sprite = Player2Cards[0].cardFront;
		Player1.enabled = true;
		Player2.enabled = true;
	}

	// each round a player can either win or both tie
	private RoundWinner DetermineWinner(Card Player1Card, Card Player2Card)
	{
		if (Player1Card.value > Player2Card.value)
		{
			return RoundWinner.PLAYER1;
		}
		else if (Player1Card.value < Player2Card.value) 
		{ 
			return RoundWinner.PLAYER2;
		}
		return RoundWinner.WAR;
	}

	private void RemoveCards()
	{
		Round.Add(Player1Cards.First());
		Round.Add(Player2Cards.First());
		Player1Cards.RemoveAt(0);
		Player2Cards.RemoveAt(0);
	}

	private void GiveCardsToWinner(List<Card> cards)
	{
		if (Winner == RoundWinner.PLAYER1)
		{
			foreach(Card card in cards) 
			{ 
				Player1Cards.Add(card);
			}
		}
		else if (Winner == RoundWinner.PLAYER2)
		{
			foreach (Card card in cards)
			{
				Player2Cards.Add(card);
			}
		}
		// do the war thing lol
		Round.Clear();
	}

	// used by the UI
	public void PlayCards()
	{
		if (CardsOut)
		{
			Player1.enabled = false;
			Player2.enabled = false;
			GiveCardsToWinner(Round);
			CardsOut = false;
		}
		else 
		{
			DisplayRound();
			Winner = DetermineWinner(Player1Cards.First(), Player2Cards.First());
			RemoveCards();
			CardsOut = true;
		}
	}
}

public enum RoundWinner 
{ 
	PLAYER1,
	PLAYER2,
	WAR
}