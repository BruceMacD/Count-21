using UnityEngine;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    //card flipping animation script
    float defaultCardScale;
    SpriteRenderer cardRender; //reference to attatched card sprite render
    Dealer deal;

    public AnimationCurve curveScale;
    public float animationTime = 0.5f;

    void Awake()
    {
        //get the current components
        cardRender = GetComponent<SpriteRenderer>();
        deal = GetComponent<Dealer>();

        defaultCardScale = transform.localScale.x;
    }

    public void FlipCard(Sprite start, Sprite end, int cardIndex)
    {
        StopCoroutine(Flip(start, end, cardIndex));
        StartCoroutine(Flip(start, end, cardIndex));
    }

    IEnumerator Flip(Sprite start, Sprite end, int cardIndex)
    {
        cardRender.sprite = start;

        float time = 0f;
        while (time <= 1f)
        {
            float scale = curveScale.Evaluate(time);
            time = time + Time.deltaTime / animationTime;

            Vector3 localScale = transform.localScale;
            localScale.x = scale * defaultCardScale; //flip left -> right
            transform.localScale = localScale;
            
            if (time >= animationTime)
            {
                cardRender.sprite = end; //show value
            }

            yield return new WaitForFixedUpdate();
        } 

        if (cardIndex == 0)
        {
            deal.ShowCard(false); //show back
        }
        else
        {
            deal.cardIndex = cardIndex; //show the card value
            deal.ShowCard(true);
        }
    }
}
