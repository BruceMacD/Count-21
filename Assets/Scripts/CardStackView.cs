using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    public Vector3 start;
    public GameObject cardPrefab;
    public GameObject canvas;
    public float cardOffset;

    void Start()
    {
        deck = GetComponent<CardStack>();
        ShowCards();
    }

    void ShowCards()
    {
        int cardCount = 0;

        foreach(int i in deck.GetCards())
        {
            float offset = cardOffset * cardCount; 

            GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
            cardCopy.transform.SetParent(canvas.transform, false);
            //offset for viewing purposes
            //TD DO: fix 88f offset
            Vector3 temp = start + new Vector3(offset, 88f);
            cardCopy.transform.position = temp;

            CardActor card = cardCopy.GetComponent<CardActor>();
            card.cardIndex = i;
            card.ShowCard(true);

            //keep the order consistant
            SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = cardCount;

            cardCount++;
        }
    }
}
