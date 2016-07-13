using UnityEngine;
using System.Collections.Generic;

public class CardActor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    //contatiner for cards still in deck
    public List<Sprite> deck = new List<Sprite>();
    public Sprite cardBack = new Sprite();
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
            spriteRenderer.sprite = cardBack;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
