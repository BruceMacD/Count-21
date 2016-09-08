using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CardEventRemovedArgs : EventArgs
{
    public int CardIndex { get; private set; }

    public CardEventRemovedArgs(int cardIndex)
    {
        //track the index of card removed pass value
        CardIndex = cardIndex;
    }
}