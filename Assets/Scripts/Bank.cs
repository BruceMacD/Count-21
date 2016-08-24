using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Bank : MonoBehaviour
{
    //start the player with $100
    double balance = 100;
    double bet = 0;

    //UI text
    public Text betText;
    public Text bankText;

    public void PlaceBet(int chip)
    {
        if (balance >= chip)
        {
            bet += chip;
            balance -= chip;
            SetBalance();
        }
    }

    public void PayOut(bool winner)
    {
        if (winner)
        {
            //casino pays 3:2
            double ret = bet * .5;
            balance = balance + bet + ret;
        }

        bet = 0;
        SetBalance();
    }

    public void SetBalance()
    {
        bankText.text = balance.ToString();
        betText.text = bet.ToString();
    }

    public bool Empty()
    {
        if (balance < 5)
        {
            //less than smallest possible bet
            return true;
        }

        return false;
    }
}
