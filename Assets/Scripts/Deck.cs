using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    List<int> cards;
	
    public void Shuffle()
    {
        if (cards == null)
        {
            cards = new List<int>();
        }
        else
        {
            cards.Clear();
        }

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

	void Start ()
    {
        Shuffle();
	}
}
