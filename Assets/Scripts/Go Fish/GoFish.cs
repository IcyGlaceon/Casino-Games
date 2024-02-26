using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GoFish : MonoBehaviour
{
    [SerializeField]DeckOfCards deck;

    [SerializeField]List<Card> player1 = new List<Card>();

    // Start is called before the first frame update
    void Start()
    {
        deck.Shuffle();

        Debug.Log(deck.cards.Length);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        deck.Draw();

        //Debug.Log(card);

        /*for (int i = 0; i < 7; i++)
        {
            player1.Add(deck.Draw());
        }*/
        //Debug.Log(player1.Count());
    }
}
