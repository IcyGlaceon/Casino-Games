using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GoFish : MonoBehaviour
{
    [SerializeField]DeckOfCards deck;

    [SerializeField]Canvas player1Canvas;

    [SerializeField] Canvas AICanvas;

    List<Card> player1 = new List<Card>();

    [SerializeField]List<Card> AI = new List<Card>();

    GameObject[] player1Cards = new GameObject[7];

    GameObject selectedCard;

    // Start is called before the first frame update
    void Start()
    {
        deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            player1.Add(deck.Draw());
            AI.Add(deck.Draw());

            player1Cards[i] = player1Canvas.transform.GetChild(i).gameObject;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        for(int i = 0; i < player1Cards.Length; i++)
        {
            player1Cards[i].GetComponent<Image>().sprite = player1[i].GetComponent<SpriteRenderer>().sprite;
        }
    }

    public void CardButtonClick()
    {
        selectedCard = EventSystem.current.currentSelectedGameObject;

       for(int i = 0; i < player1Cards.Length; i++)
        {
            if(selectedCard.GetComponent<Image>().sprite == player1[i].GetComponent<SpriteRenderer>().sprite)
            {
                for(int j = 0; j < AI.Count; j++)
                {
                    if (player1[i].value == AI[j].value)
                    {
                        Debug.Log("Same Rank");
                    }
                    else
                    {
                        Debug.Log("GO FISH");
                    }

                }                
            }
        }

    }
}
