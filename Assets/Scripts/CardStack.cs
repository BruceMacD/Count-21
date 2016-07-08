using UnityEngine;
using System.Collections.Generic;

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

    //card decks are a 'stack'
    //Pop/Push to Get/Set
    public int Pop()
    {
        //first random number
        int ret = cards[0];
        cards.RemoveAt(0);
        return ret;
    }

    public void Push(int card)
    {
        cards.Add(card);
    }

    public void MakeDeck()
    {
        //fill with cards
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }

        //Fisher-Yates shuffle
        int n = cards.Count;
        while (n > 2)
        {
            n--;
            //dont shuffle card back stored at index 0
            int changeIndex = Random.Range(1, n + 1);
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
