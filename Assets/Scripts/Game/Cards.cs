using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cards : MonoBehaviour, IPointerClickHandler
{
    public bool isInteractable = true; // new => controls interaction 

    public int cardID;

    public GameManager gameManager;

    private Animator anim;

    public bool isFlipped = false;
    public bool isMatched = false;
    public Image cardImage;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        //ResetCard();
    }

    public void OnPointerClick(PointerEventData eventData) // for android
    {
        if (!isInteractable) // new => to block interaction during preview
        {
            return;
        }

        FlipCard();
        
    }

    public void FlipCard()
    {
        if (isMatched) return;

        if (!isFlipped && (gameManager.firstCard == null || gameManager.secondCard == null))
        {
            isFlipped = true;
            anim.SetTrigger("FlipFront");
            SoundSys.Instance.PlayCardFlip();
            StartCoroutine(ShowFrontImageDelayed());

        }
    }
    IEnumerator ShowFrontImageDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        cardImage.sprite = gameManager.cardFaces[cardID];
        gameManager.CardFlipped(this);
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
        anim.SetTrigger("FlipBack");
        SoundSys.Instance.PlayCardFlip();
        StartCoroutine(ShowBackImageDelayed());

    }

    IEnumerator ShowBackImageDelayed()
    {
        yield return new WaitForSeconds(0.5f);
        cardImage.sprite = gameManager.cardBack;
    }
    public void SetMatched(bool matched)
    {
        isMatched = matched;
        // add matched animation and sound here  
    }


    // new => to show face, helps in preview

    public void ShowFace()
    {
        Debug.LogError("Memorise them cards");
        isFlipped = true;
        SoundSys.Instance.PlayCardFlip();
        if(cardImage != null && gameManager != null)
        {
            cardImage.sprite = gameManager.cardFaces[cardID];
        }
    }

    public void ShowBack()
    {
        Debug.LogError("Cards are Hidden now");
        isFlipped = false;
        SoundSys.Instance.PlayCardFlip();
        if (cardImage != null && gameManager != null && gameManager.cardBack != null)
        {
            cardImage.sprite = gameManager.cardBack;
        }
    }
}
