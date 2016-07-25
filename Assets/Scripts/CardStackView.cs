using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    Dictionary<int, GameObject> fetchedCards;
    int lastCount;

    public Vector3 start;
    public GameObject cardPrefab;
    public GameObject canvas;
    //dealers first card must be drawn face down
    public bool dealerFirstCard = false;
    public bool showFace = false;
    //selector to draw from top or bottom
    public bool drawBottom = false;
    public float cardOffset;

    void Start()
    {
        fetchedCards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardStack>();
        ShowCards();
        lastCount = deck.CardCount;

        deck.CardRemoved += Deck_CardRemoved;
    }

    private void Deck_CardRemoved(object sender, CardEventRemovedArgs e)
    {
        //check contains card to be removed
        if (fetchedCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchedCards[e.CardIndex]);
            fetchedCards.Remove(e.CardIndex);
        }
    }

    void Update()
    {
        ShowCards();
    }

    public void Clear()
    {
        deck.Reset();

        foreach(GameObject view in fetchedCards.Values)
        {
            Destroy(view.gameObject);
        }
        fetchedCards.Clear();
    }

    void ShowCards()
    {
        int cardCount = 0;

        foreach(int i in deck.GetCards())
        {
            float offset = cardOffset * cardCount;
            
            //offset for viewing purposes
            Vector3 temp = start + new Vector3(offset, 0f);
            AddCard(temp, i, cardCount);

            cardCount++;
        }
    }

    void AddCard(Vector3 location, int cardIndex, int orderIndex)
    {
        if (fetchedCards.ContainsKey(cardIndex))
        {
            //error check, already added
            return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.SetParent(canvas.transform, false);

        cardCopy.transform.localPosition = location;

        CardActor card = cardCopy.GetComponent<CardActor>();
        card.cardIndex = cardIndex;
        if (dealerFirstCard)
        {
            dealerFirstCard = false;
            card.ShowCard(false);
        }
        else
        {
            //firstCard.ShowCard(true);
            card.ShowCard(showFace);
        }

        //keep the order consistant
        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (drawBottom)
        {
            spriteRenderer.sortingOrder = orderIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = 52 - orderIndex;
        }

        fetchedCards.Add(cardIndex, cardCopy);

        Debug.Log("Hand value = " + deck.HandValue());
    }
}
