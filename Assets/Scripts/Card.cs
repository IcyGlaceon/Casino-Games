using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Suit suit;
    [SerializeField] Sprite cardFront;
    [SerializeField] Sprite cardBack;
    [SerializeField] bool flipped;
    SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
        if (flipped)
        {
            spriteRenderer.sprite = cardFront;
        }
        else 
        {
            spriteRenderer.sprite = cardBack;
        }
	}

    public void Flip() 
    {
        flipped = !flipped;
		if (flipped)
		{
			spriteRenderer.sprite = cardFront;
		}
		else
		{
			spriteRenderer.sprite = cardBack;
		}
	}
}

public enum Suit 
{ 
    SPADE,
    HEART,
    CLUB,
    DIAMOND
}