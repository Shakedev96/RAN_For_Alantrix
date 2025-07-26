using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cards : MonoBehaviour, IPointerClickHandler
{
    public int cardID; 

    public GameManager gameManager;

    public bool isFlipped = false;
    public bool isMatched = false;
    public Image cardImage;

    void Start()
    {
        ResetCard();
    }

    public void OnPointerClick(PointerEventData eventData) // for android
    {
        FlipCard();
    }
    
    public void FlipCard()
    {
        if (isMatched) return;

        if (!isFlipped && (gameManager.firstCard == null || gameManager.secondCard == null))
        {
            isFlipped = true;
            cardImage.sprite = gameManager.cardFaces[cardID];
            gameManager.CardFlipped(this);
        }
    }

    public void ResetCard()
    {
        isFlipped = false;
        isMatched = false;
        if (cardImage != null && gameManager != null && gameManager.cardBack != null)
        {
            cardImage.sprite = gameManager.cardBack;
        }
        else
        {
            Debug.LogError("cardImage or GameManager.cardBack is null!");
        }
    }

    public void HideCard()
    {
        isFlipped = false;
        cardImage.sprite = gameManager.cardBack;
    }
    public void SetMatched(bool matched)
    {
        isMatched = matched;
        // add matched animation and sound here  
    }
}


