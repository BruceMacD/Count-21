using UnityEngine;
using System.Collections;

public class DebugDealer : MonoBehaviour
{
    public CardStack dealer;
    public CardStack player;

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 256, 28), "Hit me!"))
        {
            player.Push(dealer.Pop());
        }
    }
}
