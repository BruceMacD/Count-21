using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CardStack player;
    public CardStack deck;
    public CardStack dealer;
    public GameObject cardBack;

    //controls
    public Button hitButton;
    public Button stickButton;
    //touch controls
    float touchDuration;
    Touch touch;
    bool touchEnabled = true;

    //start the player with $100
    public int bank;
    public int bet;
    public Text bankText;

    #region Controls

    void Update()
    {
        //detect double tap or hold
        if(Input.touchCount > 0 && touchEnabled)
        {
            touchDuration += Time.deltaTime;
            touch = Input.GetTouch(0);
 
            //double tap
            if(touch.phase == TouchPhase.Ended && touchDuration < 0.2f)
            {
                //not tap and hold, check hit
                StartCoroutine("CheckDoubleTap");
            }
            //press and hold
            else if (touch.phase == TouchPhase.Moved && touchDuration > 0.4f)
            {
                //player sticks
                Stick();
            }
        }
        else
        {
            touchDuration = 0.0f;
        }
    }
 
    IEnumerator CheckDoubleTap(){
        yield return new WaitForSeconds(0.3f);
        if(touch.tapCount == 2){
            //tapped twice, stop checking now
            StopCoroutine("CheckDoubleTap");
            Hit();
        }
    }

    public void Hit()
    {
        player.Push(deck.Pop());
        if (player.HandValue() >= 21)
        {
            touchEnabled = false;
            //Player is bust or at 21
            StartCoroutine(DealerHit());
        }
    }

    public void Stick()
    {
        //stop checking touch
        touchEnabled = false;
        StartCoroutine(DealerHit());
    }

    #endregion

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        //TO DO: deal with large numbers
        bankText.text = bank.ToString();
        cardBack.SetActive(true);

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
        cardBack.SetActive(false);

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

        touchEnabled = true;

        StartGame();
    }
    
}
