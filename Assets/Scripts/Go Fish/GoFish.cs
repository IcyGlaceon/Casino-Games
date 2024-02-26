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
        //deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            player1.Add(deck.Draw());
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        //Debug.Log(player1.Count());
    }
}
