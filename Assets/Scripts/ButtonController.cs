using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    Button hitButton;
    int cardIndex = 1;
    //not implemented
    public void Awake()
    {
        hitButton = GetComponent<Button>();

        hitButton.onClick.AddListener(() => { ; });
    }
}