using UnityEngine;
using System.Collections;

public class DebugChangeCard : MonoBehaviour
{
    Dealer deal;
    int cardIndex = 1;

    public GameObject card;

	// Use this for initialization
	void Awake()
    {
        deal = card.GetComponent<Dealer>();
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 28), "Hit me!"))
        {
            deal.cardIndex = cardIndex;
            deal.DealCard();

            if (cardIndex == 52)
            {
                cardIndex = 0;
            }

            cardIndex++;
        }
    }

    public void OnClick()
    {
        deal.cardIndex = cardIndex;
        deal.DealCard();

        if (cardIndex == 52)
        {
            cardIndex = 0;
        }

        cardIndex++;
    }
}
