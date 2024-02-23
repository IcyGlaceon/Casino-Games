using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckOfCards : MonoBehaviour
{
    [SerializeField] Card[] cards;
    [SerializeField] Card[] discard;

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

    public void ReturnToDeck(bool shuffled = true)
    {
		foreach (Card card in discard)
		{ 
			cards.Append(card);
		}
		discard = new Card[0];
		Shuffle();
    }

	public void Discard(Card card)
	{ 
		discard.Append(card);
		cards = cards.Where(item => !item.Equals(card)).ToArray();
	}
}
