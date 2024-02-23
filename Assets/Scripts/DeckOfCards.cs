using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckOfCards : MonoBehaviour
{
    [SerializeField] Card[] cards;
    [SerializeField] Card[] discard;

	/// <summary>
	/// Randomize the order of the cards in the main deck.
	/// </summary>
    public void Shuffle()
    {
		System.Random rand = new System.Random();
		for (int i = cards.Length - 1; i > 0; i--)
		{
			int j = rand.Next(0, i + 1);
			Card temp = cards[i];
			cards[i] = cards[j];
			cards[j] = temp;
		}

		string deck = "";
		foreach (Card card in cards)
		{
			deck += card.value + " " + card.suit + "\n";
		}
		Debug.Log(deck);
	}

	/// <summary>
	/// Place all cards currently in the discard pile back into the main deck.
	/// </summary>
	/// <param name="shuffled">Do you want the cards to be shuffled or not. Defualt is shuffling the cards.</param>
    public void ReturnToDeck(bool shuffled = true)
    {
		foreach (Card card in discard)
		{ 
			cards.Append(card);
		}
		discard = new Card[0];
		if (shuffled)
		{ 
			Shuffle();
		}
    }

	/// <summary>
	/// Place any card into the discard pile.
	/// </summary>
	/// <param name="card">The card that you want to discard.</param>
	public void Discard(Card card)
	{ 
		discard.Append(card);
	}

	/// <summary>
	/// Place any number of cards into the discard pile.
	/// </summary>
	/// <param name="discards">The cards that you want to discard.</param>
	public void Discard(Card[] discards)
	{
		foreach (Card card in discards)
		{ 
			discard.Append(card);
		}
	}

	/// <summary>
	/// Draw any number of cards. Once drawn, you need to keep track of them to then be moved into discard later.
	/// </summary>
	/// <param name="NumberOfCards">How many cards you want to draw from the top of the deck.</param>
	/// <returns>The cards that you have drawn</returns>
	public Card[] Draw(int NumberOfCards)
	{
		Card[] drawn = new Card[0];
		for (int i = 0; i < NumberOfCards; i++)
		{
			drawn.Append(cards[i]);
		}

		foreach (Card card in drawn)
		{
			cards = cards.Where(item => !item.Equals(card)).ToArray();
		}

		return drawn;
	}

	/// <summary>
	/// Draw one card from the deck. Once drawn, you need to keep track of it so that it can be placed in the discard pile later.
	/// </summary>
	/// <returns>The drawn card.</returns>
	public Card Draw()
	{
		Card drawn = cards[0];
		cards = cards.Where(item => !item.Equals(cards[0])).ToArray();
		return drawn;
	}
}
