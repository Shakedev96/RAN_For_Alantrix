using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Cards cardPrefab;
    public Sprite cardBack; // shows the back of the card
    public Sprite[] cardFaces; // since the faces of card are fixed usig array 

    private List<Cards> cards; // using this to hold all the cards

    private List<int> cardIDs; // card ids

    public Cards firstCard,secondCard; // to check for flipped cards

    public Transform cardHolder; // the panel that has grid layout

    public GameObject finalUI;
    public TextMeshProUGUI finalText;// gameOVer text
    public TextMeshProUGUI timerText;


    private int pairsMatched;
    private int totalPairs;
    private float timer;
    private bool isGameOver;

    private bool isLevelFinished;

    public float maxTime = 60f;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cards = new List<Cards>();
        cardIDs = new List<int>();
        pairsMatched = 0; // zero pairs matched at the start of the game
        totalPairs = cardFaces.Length / 2; // card faces are 6 but pairs are 12 hence half

        timer = maxTime;
        isGameOver = false; 
        isLevelFinished = false;

        CreateCards();
        ShuffleCards();
        finalUI.gameObject.SetActive(false);

        
    }
    void Update()
    {
        if (!isGameOver && !isLevelFinished)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTImerText();

            }
            else
            {
                GameOver();
            }

        }
    }

    void CreateCards()
    {
        for (int i = 0; i < cardFaces.Length / 2; i++)
        {
            cardIDs.Add(i); // adding cardID to the first card in the pair
            cardIDs.Add(i); // same but to the second card in the pair
        }
        foreach(int id in cardIDs) // spwaning cards shuffled as per their iD's
        {
            Cards newcards = Instantiate(cardPrefab, cardHolder); // instantiating the cardprefab to the grid layout
            newcards.gameManager = this; // linking the gamemanager to the Cards 
            newcards.cardID = id; // Card ID assigned 
            cards.Add(newcards); 
        }
    }

    void ShuffleCards()
    {
        for (int i = 0; i < cardIDs.Count; i++)
        {
            int randomIndex = Random.Range(i, cardIDs.Count);
            int temp = cardIDs[i];
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;    
        }

        for(int i = 0;i < cardIDs.Count;i++)
        {
            cards[i].cardID = cardIDs[i]; // assinging the card ids of shuffled cards to the cards
        }
    }

    public void CardFlipped(Cards flippedCard) // function called when card is flipped
    {
        if (firstCard == null)
        {
            firstCard = flippedCard; // settin the first flipped card

        }
        else if (secondCard == null)
        {
            secondCard = flippedCard; // second flipped card

            CheckMatch();
        }
    }

    void CheckMatch()
    {
        if(firstCard.cardID == secondCard.cardID)
        {
            pairsMatched++; // increment to pairsmatched as the cards match

            if (pairsMatched == totalPairs) // to check if the level is finished or not
            {
                LevelFinished();
            }

            firstCard = null;// resetting the cards back
            secondCard = null;
        }
        else
        {
            StartCoroutine(FlipBackCards()); // mismatch cards need to flip back after delay
        }
    }

    IEnumerator FlipBackCards()
    {
        yield return new WaitForSeconds(1f); // delay of 1 second
        firstCard.HideCard();
        secondCard.HideCard();
        firstCard = null;
        secondCard = null;  
    }

    void GameOver()
    {
        isGameOver = true;
        FinalPanel();
    }

    void LevelFinished()
    {
        isLevelFinished = true;
        FinalPanel();
    }

    public void FinalPanel()
    {
        finalUI.gameObject.SetActive(true);
        if(isLevelFinished)
        {
            finalText.text = " Level Finished time Taken:  " + Mathf.Round(timer) + "s"; // add moves and final score

        }
        else if (isGameOver)
        {
            finalText.text = " Game OVer ";
        }
    }


    public void RestartGame()
    {
        pairsMatched = 0;
        timer = maxTime;
        isGameOver = false;
        isLevelFinished = false;
        finalUI.gameObject.SetActive(false);    

        //resetting the cards, use object pooling here if possible
        foreach(var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        cardIDs.Clear();

        CreateCards();
        ShuffleCards();

    }

    void UpdateTImerText()
    {
        timerText.text = "time left " + Mathf.Round(timer) + "s";
    }
}



