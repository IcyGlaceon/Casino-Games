using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Card : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] Suit suit;
    [SerializeField] Sprite image;
    SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = image;
	}
}

public enum Suit 
{ 
    SPADE,
    HEART,
    CLUB,
    DIAMOND
}