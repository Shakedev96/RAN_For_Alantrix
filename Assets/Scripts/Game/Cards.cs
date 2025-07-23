using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public int cardID; 

    public GameManager gameManager;

    private bool isFlipped;
    public Image cardImage;

    void Start()
    {
        isFlipped = false;
        //cardImage.sprite = GameManager.Instance.cardBack; //to show back ofcard
        if (cardImage != null && GameManager.Instance != null && GameManager.Instance.cardBack != null)
        {
            cardImage.sprite = GameManager.Instance.cardBack; // to show back of card
        }
        else
        {
            Debug.LogError("cardImage or GameManager.Instance.cardBack is null!");
        }
    }

    public void FlipCard()
    {
        if(!isFlipped && gameManager.firstCard == null || gameManager.secondCard == null)
        {
            isFlipped = true;
            cardImage.sprite = gameManager.cardFaces[cardID];
            gameManager.CardFlipped(this);
        }
    }

    public void HideCard()
    {
        isFlipped = false;
        cardImage.sprite = gameManager.cardBack;
    }
}


