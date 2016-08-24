using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Bank : MonoBehaviour
{
    //start the player with $100
    decimal balance = 100.00m;
    decimal bet = 0;

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
            decimal ret = bet * .5m;
            balance = balance + bet + ret;
        }

        bet = 0;
        SetBalance();
    }

    public void SetBalance()
    {
        decimal balanceDisplay = Convert.ToDecimal(string.Format("{0:F2}", balance));
        bankText.text = balanceDisplay.ToString();

        decimal betDisplay = Convert.ToDecimal(string.Format("{0:F2}", bet));
        betText.text = betDisplay.ToString();
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
