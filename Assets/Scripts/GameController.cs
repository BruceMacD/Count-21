using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public CardStack player;
    public CardStack deck;
    public CardStack dealer;

    #region Controls

    public void Hit()
    {
        player.Push(deck.Pop());
        Debug.Log("Hit");
    }

    public void Stick()
    {

    }

    #endregion

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
