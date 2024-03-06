using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoFish : MonoBehaviour
{
    [SerializeField] DeckOfCards deck;

    [SerializeField] Canvas player1Canvas;

    [SerializeField] Canvas AICanvas;

    [SerializeField] List<Card> player1 = new List<Card>();

    [SerializeField] List<Card> AI = new List<Card>();

    [SerializeField]List<GameObject> player1Cards = new List<GameObject>();

    HashSet<int> hashSet;

    List<int> dupCards;

    GameObject selectedCard;

    Sprite defaultCard;

    [SerializeField] GameObject cardPrefab;


    bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject card;
        deck.Shuffle();

        for (int i = 0; i < 7; i++)
        {
            player1.Add(deck.Draw());
            AI.Add(deck.Draw());
            card = Instantiate(cardPrefab, new Vector3((i * 50) + 430, 100, 0), Quaternion.Euler(0, 0, 0));
            card.GetComponent<Button>().onClick.AddListener(CardButtonClick);
            card.transform.SetParent(player1Canvas.transform);
            player1Cards.Add(player1Canvas.transform.GetChild(i).gameObject);
        }
        defaultCard = player1Cards[0].GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("GoFish");
        }

        if (player1.Count != player1Cards.Count)
        {
            player1Cards.RemoveAt(player1Cards.Count - 1);
            CheckDuplicateCards();
            UpdateDisplay();
        }

        if (player1Canvas.transform.childCount != player1.Count || player1Canvas.transform.childCount != player1Cards.Count)
        {
            Destroy(player1Canvas.transform.GetChild(player1Canvas.transform.childCount).gameObject);
            CheckDuplicateCards();
            UpdateDisplay();
        }
    }

    private void OnMouseDown()
    {
        if (!gameStart)
        {
            CheckDuplicateCards();

            UpdateDisplay();
            gameStart = true;


        }
    }

    public void CardButtonClick()
    {
        GameObject card;

        selectedCard = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < player1.Count; i++)
        {
            if (selectedCard.GetComponent<Image>().sprite == player1[i].GetComponent<SpriteRenderer>().sprite)
            {
                for (int j = 0; j < AI.Count; j++)
                {
                    if (player1[i].value == AI[j].value)
                    {
                        player1.Remove(player1[i]);
                        Destroy(selectedCard);

                        CheckDuplicateCards();
                        UpdateDisplay();
                    }
                    else
                    {

                        player1.Add(deck.Draw());
                        card = Instantiate(cardPrefab, new Vector3((player1.Count * 50) + 430, 100, 0), Quaternion.Euler(0, 0, 0));
                        card.GetComponent<Button>().onClick.AddListener(CardButtonClick);
                        card.GetComponent<Image>().sprite = player1[player1.Count - 1].GetComponent<SpriteRenderer>().sprite;
                        card.transform.SetParent(player1Canvas.transform);
                        player1Cards.Add(player1Canvas.transform.GetChild(player1Cards.Count).gameObject);
                        CheckDuplicateCards();
                        UpdateDisplay();
                        break;
                    }

                }
            }
        }

    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < player1.Count; i++)
        {
            if (player1Cards[i].GetComponent<Image>().sprite == defaultCard)
            {
                player1Cards[i].GetComponent<Image>().sprite = player1[i].GetComponent<SpriteRenderer>().sprite;
            }
        }

        for (int i = 0; i < player1Canvas.transform.childCount; i++)
        {

            if (player1Canvas.transform.GetChild(i).GetComponent<Image>().sprite == defaultCard)
            {
                Debug.Log("TEST");

                Destroy(player1Canvas.transform.GetChild(i).gameObject);

            }
        }


        for (int i = 0; i < player1Cards.Count + 1; i++)
        {
            if (player1Cards[i].GetComponent<Image>().sprite == defaultCard)
            {
                player1Cards.Remove(player1Cards[i]);
                Debug.Log(":)");
            }
            if (player1Cards[i] == null)
            {
                Debug.Log(":(");
                Destroy(player1Cards[i]);
            }
        }
    }

    public void CheckDuplicateCards()
    {
        hashSet = new HashSet<int>();
        dupCards = new List<int>();

        foreach (var card in player1)
        {
            if (!hashSet.Add(card.value))
            {
                dupCards.Add(card.value);
            }
        }

        foreach (var dup in dupCards)
        {
            for (int i = 0; i < player1.Count; i++)
            {
                if (dup == player1[i].value)
                {
                    player1.Remove(player1[i]);
                }
            }
        }
    }
}
