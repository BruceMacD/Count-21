using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public CardStack player;
    public CardStack deck;
    public CardStack dealer;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            dealer.Push(deck.Pop());
        }
    }
}
