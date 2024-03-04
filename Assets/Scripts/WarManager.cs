using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WarManager : MonoBehaviour
{
    [SerializeField] DeckOfCards PlayableCards;
	[SerializeField] TMP_Text Player1Text;
	[SerializeField] TMP_Text Player2Text;
	[SerializeField] SpriteRenderer Player1;
	[SerializeField] SpriteRenderer Player2;
	[SerializeField] TMP_Text PlayedStackText1;
	[SerializeField] TMP_Text PlayedStackText2;
	[SerializeField] TMP_Text WarUI;
	[SerializeField] TMP_Text Player1Win;
	[SerializeField] TMP_Text Player2Win;
	[SerializeField] GameObject Tutorial;
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
		PlayedStackText1.enabled = false;
		PlayedStackText2.enabled = false;
		WarUI.enabled = false;
		Player1Win.enabled = false;
		Player2Win.enabled = false;
		Tutorial.SetActive(false);
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
		Round.Clear();
	}

	// used by the UI
	public void PlayCards()
	{
		if (CardsOut)
		{
			if (Winner != RoundWinner.WAR)
			{
				Player1.enabled = false;
				Player2.enabled = false;
				PlayedStackText1.enabled = false;
				PlayedStackText2.enabled = false;
				GiveCardsToWinner(Round);
				CardsOut = false;
			}
			else
			{
				if (WarUI.enabled == true)
				{
					WarUI.enabled = false;
					DisplayRound();
					RemoveCards();
					DisplayRound();
					Winner = DetermineWinner(Player1Cards.First(), Player2Cards.First());
					RemoveCards();
					PlayedStackText1.text = (Round.Count() / 2).ToString();
					PlayedStackText2.text = (Round.Count() / 2).ToString();
					PlayedStackText1.enabled = true;
					PlayedStackText2.enabled = true;
					CardsOut = true;
				}
				else 
				{
					WarUI.enabled = true;
				}
			}
		}
		else 
		{
			if (Player1Cards.Count() > 0 && Player2Cards.Count() > 0)
			{ 
				DisplayRound();
				Winner = DetermineWinner(Player1Cards.First(), Player2Cards.First());
				RemoveCards();
				CardsOut = true;
			}
			else 
			{
				if (Player1Cards.Count() <= 0)
				{
					Player2Win.enabled = true;
				}
				else 
				{
					Player1Win.enabled = true;
				}
			}
		}
	}

	public void ShowTutorial()
	{ 
		Tutorial.SetActive(true);
	}

	public void HideTutorial()
	{
		Tutorial.SetActive(false);
	}
}

public enum RoundWinner 
{ 
	PLAYER1,
	PLAYER2,
	WAR
}