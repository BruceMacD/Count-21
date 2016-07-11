using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    List<int> fetchedCards;
    int lastCount;

    public Vector3 start;
    public GameObject cardPrefab;
    public GameObject canvas;
    public float cardOffset;

    void Start()
    {
        fetchedCards = new List<int>();
        deck = GetComponent<CardStack>();
        ShowCards();
        lastCount = deck.CardCount;
    }

    void Update()
    {
        if (lastCount != deck.CardCount)
        {
            lastCount = deck.CardCount;
            ShowCards();
        }
    }

    void ShowCards()
    {
        int cardCount = 0;

        foreach(int i in deck.GetCards())
        {
            float offset = cardOffset * cardCount;
            
            //offset for viewing purposes
            //TD DO: fix 88f offset
            Vector3 temp = start + new Vector3(offset, 0f);
            AddCard(temp, i, cardCount);

            cardCount++;
        }
    }

    void AddCard(Vector3 location, int cardIndex, int orderIndex)
    {
        if (fetchedCards.Contains(cardIndex))
        {
            //error check, already added
            return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.SetParent(canvas.transform, false);

        cardCopy.transform.position = location;

        CardActor card = cardCopy.GetComponent<CardActor>();
        card.cardIndex = cardIndex;
        card.ShowCard(true);

        //keep the order consistant
        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = orderIndex;

        fetchedCards.Add(cardIndex);
    }
}
