using UnityEngine;
using System.Collections;

public class CardFlip : MonoBehaviour
{
    //not implemented
    //card flipping animation script
    float defaultCardScale;
    SpriteRenderer cardRender; //reference to attatched card sprite render
    CardActor card;

    public AnimationCurve curveScale;
    public float animationTime = 0.5f;

    void Awake()
    {
        //get the current components
        cardRender = GetComponent<SpriteRenderer>();
        card = GetComponent<CardActor>();

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
        //TO DO: change to match new design
        if (cardIndex == 0)
        {
            card.ShowCard(false); //show back
        }
        else
        {
            card.cardIndex = cardIndex; //show the card value
            card.ShowCard(true);
        }
    }
}
