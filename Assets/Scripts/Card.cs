using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    [SerializeField] public int value;
    [SerializeField] public Suit suit;
    [SerializeField] public bool flipped;
    [SerializeField] public Sprite cardFront;
    [SerializeField] public Sprite cardBack;
    public SpriteRenderer spriteRenderer;

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
    DIAMOND,
    JOKER
}