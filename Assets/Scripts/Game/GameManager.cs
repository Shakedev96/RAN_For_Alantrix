using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameMode pendingMode = GameMode.Easy;
    public static bool usePendingMode = false;

    public static GameManager Instance;

    public GridLayoutGroup gridLayout;

    public enum GameMode { Easy, Medium, Hard }
    public GameMode currentMode;

    public Cards cardPrefab;
    public Sprite cardBack; // shows the back of the card
    public Sprite[] cardFaces; // since the faces of card are fixed usig array 

    private List<Cards> cards; // using this to hold all the cards

    private List<int> cardIDs; // card ids

    public Cards firstCard, secondCard; // to check for flipped cards

    public Transform cardHolder; // the panel that has grid layout

    public GameObject finalUI;
    public TextMeshProUGUI finalText;// gameOVer text
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI movesText;




    private int pairsMatched;
    private int totalPairs;
    public float timer;
    private bool isGameOver;
    public int score;
    public int movesPlayed;

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

        finalUI.gameObject.SetActive(false);

        if (usePendingMode)
        {
            SetGameMode(pendingMode);
            usePendingMode = false;
        }

        Debug.Log("GameManager Start(): timer=" + timer);
    }
    void Update()
    {
        if (!isGameOver && !isLevelFinished)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateUI();

            }
            else
            {
                GameOver();
            }

        }
    }

    public void SetGameMode(GameMode mode)
    {
        currentMode = mode;
        Debug.Log("SetGameMode called with mode: " + mode);

        int columns = 4; // default 

        switch (mode)
        {
            case GameMode.Easy:
                totalPairs = 4;
                columns = 4;
                break;
            case GameMode.Medium:
                totalPairs = 8;
                columns = 4;
                break;
            case GameMode.Hard:
                totalPairs = 14;
                columns = 7;
                break;
        }

        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = columns;
        }

        Debug.Log("SetGameMode(): timer=" + timer);
        pairsMatched = 0;
        score = 0;
        movesPlayed = 0;
        timer = maxTime;
        isGameOver = false;
        isLevelFinished = false;

        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        cardIDs.Clear();

        GenerateBoard();

        finalUI.SetActive(false);
        UpdateUI();
    }

    public void GenerateBoard()
    {
        cardIDs = new List<int>();
        for (int i = 0; i < totalPairs; i++)
        {
            cardIDs.Add(i);
            cardIDs.Add(i);
        }

        ShuffleCards();

        cards = new List<Cards>();

        foreach (int id in cardIDs)
        {
            Cards newCard = Instantiate(cardPrefab, cardHolder);
            newCard.gameManager = this;
            newCard.cardID = id;// Assign ID here AFTER shuffle
            newCard.ResetCard();
            cards.Add(newCard);

        }
    }

    void ShuffleCards()
    {
        for (int i = cardIDs.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = cardIDs[i];
            cardIDs[i] = cardIDs[randomIndex];
            cardIDs[randomIndex] = temp;
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
        movesPlayed++; // increases

        if (firstCard.cardID == secondCard.cardID)
        {
            pairsMatched++; // increment to pairsmatched as the cards match
            score += 2;

            firstCard.SetMatched(true);
            secondCard.SetMatched(true);

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
        yield return new WaitForSeconds(1f);

        if (firstCard != null)
        {
            firstCard.HideCard();
        }
        if (secondCard != null)
        {
            secondCard.HideCard();
        }
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

        if (isLevelFinished)
        {
            finalText.text =
                "Level Finished, Time Taken: " + Mathf.Round(timer) + "s\n" +
                "Score is " + Mathf.Round(score) + " pts\n" +
                "Moves Played: " + movesPlayed;
        }
        else if (isGameOver)
        {
            finalText.text = "Game Over\nMoves Played: " + movesPlayed;
        }
    }
    public void RestartGame()
    {
        firstCard = null;
        secondCard = null;
        SetGameMode(currentMode);
    }

    void UpdateUI()
    {
        timerText.text = "time left " + Mathf.Round(timer) + "s";
        scoreText.text = "Score: " + Mathf.Abs(score) + " pts";
        if (movesText != null)
        {
            movesText.text = "Moves: " + movesPlayed;
        }
    }


}





