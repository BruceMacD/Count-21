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

    public int bank;
    public int bet;

    #region Controls

    public void Hit()
    {
        player.Push(deck.Pop());
        if (player.HandValue() > 21)
        {
            //Player is bust
            StartCoroutine(DealerHit());
        }
    }

    public void Stick()
    {
        StartCoroutine(DealerHit());
    }

    #endregion

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        //TO DO: show card back

        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            dealer.Push(deck.Pop());
        }
    }

    IEnumerator DealerHit()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;
        //flip dealer card by hiding cardBack
        Vector3 localScale = transform.localScale;
        localScale.x = 0; //hide card
        cardBack.transform.localScale = localScale;

        if (player.HandValue() <= 21 && player.HandValue() > dealer.HandValue())
        {
            while (dealer.HandValue() < player.HandValue())
            {
                yield return new WaitForSeconds(1f);
                dealer.Push(deck.Pop());
            }
        }
        //let the player see the results
        yield return new WaitForSeconds(3f);
        CheckWinner();
    }

    public void CheckWinner()
    {
        if ((player.HandValue() > dealer.HandValue() && (player.HandValue() <= 21)) || dealer.HandValue() > 21)
        {
            Debug.Log("Player wins: player = " + player.HandValue() + " dealer: " + dealer.HandValue());
            //TO DO: Add to bank based on bet
        }
        else
        {
            //dealer wins
            Debug.Log("Dealer wins: player = " + player.HandValue() + " dealer: " + dealer.HandValue());
            //Remove bet from play/bank
        }
        ResetGame();
    }

    public void ResetGame()
    {
        player.GetComponent<CardStackView>().Clear();
        dealer.GetComponent<CardStackView>().Clear();
        dealer.Reset();

        hitButton.interactable = true;
        stickButton.interactable = true;

        StartGame();
    }
    
}
