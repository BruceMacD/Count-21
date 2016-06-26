﻿using UnityEngine;
using System.Collections.Generic;

public class Dealer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //contatiner for cards still in deck
    public List<Sprite> deck = new List<Sprite>();
    //list of cards in play
    public List<Sprite> cards = new List<Sprite>();
    //player's current hand
    public List<Sprite> hand = new List<Sprite>();
    //cardIndex 0 = back
    public int cardIndex = 1;

    public void ShowCard(bool flip)
    {
        if (flip)
        {
            //set card value
            spriteRenderer.sprite = deck[cardIndex];
        }
        else
        {
            //show back
            spriteRenderer.sprite = deck[0];
        }

    }

    //add cards to deck
    public void Shuffle()
    {
      //cards.Add(c2);
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
