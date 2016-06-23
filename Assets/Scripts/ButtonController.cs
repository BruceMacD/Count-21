using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Button hitButton;
    Dealer deal;
    int cardIndex = 1;

    void Awake()
    {
        hitButton = GetComponent<Button>();

        hitButton.onClick.AddListener(() => { ; });
    }
}