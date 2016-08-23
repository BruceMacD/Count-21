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
    
    //touch controls
    float touchDuration;
    Touch touch;
    bool touchEnabled = true;

    //keep current hi/lo count
    int count = 0;
    int prevPlayerHandVal;
    int prevDealerHandVal;
    int hiddenVal;

    //button text
    public Text betText;
    public Text countText;
    public Text bankText;

    //bank and betting
    double bank = 100;
    double bet = 0;

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
        AddPlayerCount();

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
        //TODO: show bet UI
        //PlaceBet();
    }

    public void PlaceBetFive()
    {
        if (bank >= 5)
        {
            bet += 5;
            bank -= 5;
            SetBalance();
        }
    }

    public void PlaceBetTen()
    {
        if (bank >= 10)
        {
            bet += 10;
            bank -= 10;
            SetBalance();
        }
    }

    public void PlaceBetTwenty()
    {
        if (bank >= 20)
        {
            bet += 20;
            bank -= 20;
            SetBalance();
        }
    }

    public void StartGame()
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
                AddDealerCount();
            }

            player.Push(deck.Pop());
            AddPlayerCount();
        }

        //TODO: move this, also verify
        //confirm.GetComponent<Animation>().Play();
    }

    void AddDealerCount()
    {
        CountCalc(dealer.HandValue() - prevDealerHandVal);
        prevDealerHandVal = dealer.HandValue();
    }

    void AddPlayerCount()
    {
        CountCalc(player.HandValue() - prevPlayerHandVal);
        prevPlayerHandVal = player.HandValue();
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
        cardBack.SetActive(false);
        //add hidden dealer card to the count
        CountCalc(hiddenVal);

        if (player.HandValue() <= 21 && player.HandValue() > dealer.HandValue())
        {
            while (dealer.HandValue() < player.HandValue())
            {
                yield return new WaitForSeconds(1f);
                dealer.Push(deck.Pop());
                AddDealerCount();
            }
        }
        //let the player see the results
        yield return new WaitForSeconds(3f);
        CheckWinner();
    }

    void CheckWinner()
    {
        if ((player.HandValue() > dealer.HandValue() && (player.HandValue() <= 21)) || dealer.HandValue() > 21)
        {
            Debug.Log("Player wins: player = " + player.HandValue() + " dealer: " + dealer.HandValue());
            //casino pays 3:2
            double ret = bet * .5;
            bank = bank + bet + ret;
            bet = 0;
        }
        else
        {
            //dealer wins
            Debug.Log("Dealer wins: player = " + player.HandValue() + " dealer: " + dealer.HandValue());
            bet = 0;
        }
        ResetGame();
    }

    public void ResetGame()
    {
        //check if player is out of money
        if (bank < 5)
        {
            //TODO: end game
        }

        player.GetComponent<CardStackView>().Clear();
        dealer.GetComponent<CardStackView>().Clear();
        dealer.Reset();

        touchEnabled = true;

        SetBalance();

        //TODO: show the betting UI
    }
    
    void SetBalance()
    {
        bankText.text = bank.ToString();
        betText.text = bet.ToString();
    }
}
