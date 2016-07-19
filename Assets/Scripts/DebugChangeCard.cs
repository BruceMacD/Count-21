using UnityEngine;
using System.Collections;

public class DebugChangeCard : MonoBehaviour
{
    CardFlip flip;
    CardActor actor;
    int cardIndex = 0;

    public GameObject card;

	// Use this for initialization
	void Awake()
    {
        flip = card.GetComponent<CardFlip>();
        actor = card.GetComponent<CardActor>();
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            if (cardIndex >= actor.deck.Count)
            {
                cardIndex = 0;
                //flip.FlipCard(lastCard, firstCard, 0 index)
                flip.FlipCard(actor.deck[actor.deck.Count - 1], actor.deck[0], 0); //return to back of card
            }
            else
            {
                flip.FlipCard(actor.deck[cardIndex - 1], actor.deck[cardIndex], cardIndex);
                cardIndex++;
            }
        }
    }
}
