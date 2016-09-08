using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CardStack player;
    public CardStack deck;
    public CardStack dealer;
    public GameObject cardBack;
    
    //touch controls
    float touchDuration;
    Touch touch;
    bool touchEnabled = false;

    //keep current hi/lo count
    int count = 0;
    int prevPlayerHandVal;
    int prevDealerHandVal;
    int hiddenVal;

    //UI text
    public Text countText;
    public Text winLoseText;
    public Text quitText;
    public Text informationText;
    bool quitGame = false;

    //UI buttons
    public GameObject confirmButton;
    public GameObject undoButton;
    public GameObject betFiveButton;
    public GameObject betTenButton;
    public GameObject betTwentyButton;

    public Bank bank;

    #region Controls

    void Update()
    {
        //detect double tap or hold
        if (Input.touchCount > 0 && touchEnabled)
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
 
    IEnumerator CheckDoubleTap()
    {
        yield return new WaitForSeconds(0.3f);

        if(touch.tapCount == 2)
        {
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
        //prevent user from getting cards before betting
        touchEnabled = false;
    }

    public void StartGame()
    {
        //only allow play if a bet is active 
        if (bank.GetBet() > 0.00m)
        {
            //clear information UI
            informationText.text = "";

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

            //hide the betting UI
            confirmButton.SetActive(false);
            undoButton.SetActive(false);
            betFiveButton.SetActive(false);
            betTenButton.SetActive(false);
            betTwentyButton.SetActive(false);

            touchEnabled = true;
        }
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
        //show the dealer's hidden card
        cardBack.SetActive(false);

        //add hidden dealer card to the count
        CountCalc(hiddenVal);

        //dealer plays until it's hand is >= player to win
        if (player.HandValue() <= 21 && player.HandValue() > dealer.HandValue())
        {
            while (dealer.HandValue() < player.HandValue())
            {
                yield return new WaitForSeconds(1f);
                dealer.Push(deck.Pop());
                AddDealerCount();
            }
        }

        CheckWinner();
    }

    void CheckWinner()
    {
        if ((player.HandValue() > dealer.HandValue() && (player.HandValue() <= 21)) || dealer.HandValue() > 21)
        {
            //update the bank balance
            bank.PayOut(true);
            winLoseText.text = "WIN";
        }
        else
        {
            //dealer wins
            bank.PayOut(false);
            winLoseText.text = "LOSE";
        }

        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        //let the player see the results
        yield return new WaitForSeconds(2f);
        winLoseText.text = "";
        
        //check if player is out of money
        if (bank.Empty())
        {
            StartCoroutine(GameOver());
        }
        else
        {
            //remove cards in play
            player.GetComponent<CardStackView>().Clear();
            dealer.GetComponent<CardStackView>().Clear();

            //dealer.Reset();

            bank.SetBalance();

            //show the betting UI
            confirmButton.SetActive(true);
            undoButton.SetActive(true);
            betFiveButton.SetActive(true);
            betTenButton.SetActive(true);
            betTwentyButton.SetActive(true);
        }
    }

    IEnumerator GameOver()
    {
        //update player the game has ended and return to main menu
        winLoseText.text = "GAME OVER";
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitMenu()
    {
        if (!quitGame)
        {
            quitText.text = "touch again to quit";
            quitGame = true;
            StartCoroutine(TouchToQuit());
        }
        else
        {
            //double tap
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator TouchToQuit()
    {
        //let the player see the results
        yield return new WaitForSeconds(2f);

        //disable quit
        quitText.text = "";
        quitGame = false;
    }
}
