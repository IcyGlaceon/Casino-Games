using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarManager : MonoBehaviour
{
    [SerializeField] DeckOfCards PlayableCards;
    List<Card> Player1Cards = new List<Card>();
    List<Card> Player2Cards = new List<Card>();

	public void Start()
	{
		PlayableCards.Shuffle();
		FillPlayerHands();
	}

	public void Update()
	{
		DisplayHands();	
	}

	private void FillPlayerHands()
	{
		int length = (PlayableCards.cards.Length / 2);
		for (int i = 0; i < length; i++) 
		{
			Player1Cards.Add(PlayableCards.Draw());
			Player2Cards.Add(PlayableCards.Draw());
		}
	}

	// show a stack of face down cards with the number of cards in the hand on top
	private void DisplayHands()
	{ 

	}

	// show the cards face up that each player played
	private void DisplayRound()
	{ 
	}

	// each round a player can either win or both tie
	private RoundWinner IsPlayer1GreaterThanPlayer2(Card Player1Card, Card Player2Card)
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

	// used by the UI
	public void PlayCards()
	{ 
		
	}
}

public enum RoundWinner 
{ 
	PLAYER1,
	PLAYER2,
	WAR
}