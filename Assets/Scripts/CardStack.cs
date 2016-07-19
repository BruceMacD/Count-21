using UnityEngine;
using System.Collections.Generic;

//Creates a list of numbers in a pseudorandom order to simulate shuffle

public class CardStack : MonoBehaviour
{
    List<int> cards;

    //only create if deck
    public bool deckCheck;

    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }

    public bool HasCards
    {
        get { return cards != null && cards.Count > 0; }
    }

    public event CardRemovedEventHandler CardRemoved;

    public int CardCount
    {
        get
        {
            if (cards == null)
            {
                return 0;
            }
            else
            {
                return cards.Count;
            }
        }
    }

    //card decks are a 'stack'
    //Pop/Push to Get/Set
    public int Pop()
    {
        //first random number
        int ret = cards[0];
        cards.RemoveAt(0);

        if (CardRemoved != null)
        {
            CardRemoved(this, new CardEventRemovedArgs(ret));
        }

        return ret;
    }

    public void Push(int card)
    {
        cards.Add(card);
    }

    public int HandValue()
    {
        //gets card values
        int total = 0;
        int aces = 0;

        foreach (int card in GetCards())
        {
            //cards are 13 decks per suit in order
            //order -> A, 2, 3, ..., K, Q
            int cardVal = card % 13;

            if (cardVal == 0)
            {
                aces++;
            }
            else if (cardVal < 9)
            {
                //numbered cards, add one offset to index
                cardVal += 1;
            }
            else
            {
                //face cards or 10;
                cardVal = 10;
            }

            total += cardVal;
        }
        //TO DO: 10 + A + A = 22, should be 12
        for (int i = 0; i < aces; i++)
        {
            //check for bust with ace
            if (total + 11 <= 21)
            {
                total += 11;
            }
            else
            {
                total += 1;
            }
        }
        
        return total;
    }

    public void MakeDeck()
    {
        //fill with cards
        for (int i = 0; i < 51; i++)
        {
            cards.Add(i);
        }

        //Fisher-Yates shuffle
        int n = cards.Count;
        while (n > 2)
        {
            n--;
            int changeIndex = Random.Range(0, n + 1);
            int swap = cards[changeIndex];
            cards[changeIndex] = cards[n];
            cards[n] = swap;
        }

    }

	void Awake()
    {
        cards = new List<int>();
        if (deckCheck)
        {
            MakeDeck();
        }
    }
}
