using UnityEngine;
using System.Collections;

public class DebugChangeCard : MonoBehaviour
{
    CardFlip flip;
    Dealer deal;
    //starts showing back, so start at count 1
    int cardIndex = 1;

    public GameObject card;

	// Use this for initialization
	void Awake()
    {
        flip = card.GetComponent<CardFlip>();
        deal = card.GetComponent<Dealer>();
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            if (cardIndex >= deal.deck.Count)
            {
                cardIndex = 1;
                //flip.FlipCard(lastCard, firstCard, 0 index)
                flip.FlipCard(deal.deck[deal.deck.Count - 1], deal.deck[0], 0); //return to back of card
            }
            else
            {
                flip.FlipCard(deal.deck[cardIndex - 1], deal.deck[cardIndex], cardIndex);
                cardIndex++;
            }
        }
    }
}
