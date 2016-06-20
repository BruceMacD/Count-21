using UnityEngine;
using System;
using System.Collections.Generic;

public class Dealer : MonoBehaviour {

    //contatiner for cards still in deck
    public List<GameObject> deck = new List<GameObject>();
    //list of cards in play
    public List<GameObject> cards = new List<GameObject>();
    //player's current hand
    public List<GameObject> player = new List<GameObject>();

    public void DealCard()
    {
        Debug.Log("hit");
    }

    //add cards to deck
    public void Shuffle()
    {
      //cards.Add(c2);
    }
}
