using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int dealerFirstCard = -1;

    public CardStack player;
    public CardStack deck;
    public CardStack dealer;
    public GameObject cardBack;

    public Button hitButton;
    public Button stickButton;

    #region Controls

    public void Hit()
    {
        player.Push(deck.Pop());
        if (player.HandValue() > 21)
        {
            //TO DO: Player is bust
            hitButton.interactable = false;
            stickButton.interactable = false;
        }
    }

    public void Stick()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;

        //flip dealer card by hiding cardBack
        Vector3 localScale = transform.localScale;
        localScale.x = 0; //hide card
        cardBack.transform.localScale = localScale;

        StartCoroutine(DealerHit());
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

    IEnumerator DealerHit()
    {
        
        while (dealer.HandValue() < 17 || (dealer.HandValue() < player.HandValue()))
        {
            //dealer.FlipFirstCard();
            dealer.Push(deck.Pop());
            yield return new WaitForSeconds(1f);
        }
    }
    
}
