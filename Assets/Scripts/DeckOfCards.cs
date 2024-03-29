using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckOfCards : MonoBehaviour
{
    [SerializeField] public List<Card> cards = new List<Card>();
    [SerializeField] public List<Card> discard = new List<Card>();

	/// <summary>
	/// Randomize the order of the cards in the main deck.
	/// </summary>
    public void Shuffle()
    {
		System.Random rand = new System.Random();
		for (int i = cards.Count - 1; i > 0; i--)
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
	}

	/// <summary>
	/// Place all cards currently in the discard pile back into the main deck.
	/// </summary>
	/// <param name="shuffled">Do you want the cards to be shuffled or not. Defualt is shuffling the cards.</param>
    public void ReturnToDeck(bool shuffled = true)
    {
		foreach (Card card in discard)
		{ 
			cards.Add(card);
		}
		discard.Clear();
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
		discard.Add(card);
	}

	/// <summary>
	/// Place any number of cards into the discard pile.
	/// </summary>
	/// <param name="discards">The cards that you want to discard.</param>
	public void Discard(List<Card> discards)
	{
		foreach (Card card in discards)
		{ 
			discard.Add(card);
		}
	}

	/// <summary>
	/// Draw any number of cards. Once drawn, you need to keep track of them to then be moved into discard later.
	/// </summary>
	/// <param name="NumberOfCards">How many cards you want to draw from the top of the deck.</param>
	/// <returns>The cards that you have drawn</returns>
	public List<Card> Draw(int NumberOfCards)
	{
		List<Card> drawn = new List<Card>();
		for (int i = 0; i < NumberOfCards; i++)
		{
			drawn.Add(cards[i]);
			cards.RemoveAt(i);
		}
		return drawn;
	}

	/// <summary>
	/// Draw one card from the deck. Once drawn, you need to keep track of it so that it can be placed in the discard pile later.
	/// </summary>
	/// <returns>The drawn card.</returns>
	public Card Draw()
	{
		Card drawn = cards.First();
		cards.Remove(drawn);
		return drawn;
	}
}
