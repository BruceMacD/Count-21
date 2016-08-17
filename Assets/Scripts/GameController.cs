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

    //betting
    public Bank gameBank;
    //betting UI
    public Button confirm;
    public Button betFive;
    public Button betTen;
    public Button betTwenty;

    //keep current hi/lo count
    int count = 0;
    int prevPlayerHandVal;
    int prevDealerHandVal;
    int hiddenVal;

    //button text
    public Text betText;
    public Text countText;
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
        CountCalc(player.HandValue() - prevPlayerHandVal);
        prevPlayerHandVal = player.HandValue();

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
        //cover the dealers first card
        cardBack.SetActive(true);

        //reset hand value for count
        prevPlayerHandVal = 0;
        prevDealerHandVal = 0;

        for (int i = 0; i < 2; i++)
        {
            //don't count the unseen dealer's card
            if (i == 0)
            {
                dealer.Push(deck.Pop());
                hiddenVal = dealer.HandValue();
                prevDealerHandVal = dealer.HandValue();
            }
            else
            {
                dealer.Push(deck.Pop());
                CountCalc(dealer.HandValue() - prevDealerHandVal);
                prevDealerHandVal = dealer.HandValue();
            }

            player.Push(deck.Pop());
            CountCalc(player.HandValue() - prevPlayerHandVal);
            prevPlayerHandVal = player.HandValue();
        }

        //TODO: move this, also verify
        //confirm.GetComponent<Animation>().Play();
    }

    void CountCalc(int cardVal)
    {
        //2, 3, 4, 5, 6, 7 = +1
        //8, 9 = 0
        //10, J, Q, K, A = -1
        if (cardVal == 1 || cardVal > 9)
        {
            count--;
        }
        else if (7 < cardVal && cardVal < 10)
        {
            //add nothing
            return;
        }
        else
        {
            count++;
        }

        countText.text = count.ToString();
    }

    IEnumerator DealerHit()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;
        cardBack.SetActive(false);
        CountCalc(hiddenVal);

        if (player.HandValue() <= 21 && player.HandValue() > dealer.HandValue())
        {
            while (dealer.HandValue() < player.HandValue())
            {
                yield return new WaitForSeconds(1f);
                dealer.Push(deck.Pop());
                CountCalc(dealer.HandValue() - prevDealerHandVal);
                prevDealerHandVal = dealer.HandValue();
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
