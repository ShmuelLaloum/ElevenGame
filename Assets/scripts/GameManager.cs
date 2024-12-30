using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab של קלף
    public Transform[] playerHands; // ידיים של השחקנים
    public Transform centerPile; // הקופה המרכזית
    public List<Card> deck = new List<Card>(); // קופה של קלפים
    public Transform[] playerPrivatePiles; // הקופות הפרטיות של כל שחקן
    private bool isFirstRound = true; // האם מדובר בסיבוב הראשון
    private int currentPlayer = 0; // השחקן בתור
    private bool gameIsActive = true; // האם המשחק פעיל?
    private List<Card> centerCards = new List<Card>(); // קלפים על השולחן (הקופה המרכזית)
    private int[] teamScores = new int[2]; // ניקוד של כל קבוצה (קבוצה 1: 0, קבוצה 2: 1)
    private bool[] teamHasBonus = new bool[2]; // האם כל קבוצה השיגה בונוס

    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DealCards();
    }

    // יצירת קופה של 52 קלפים
    void InitializeDeck()
    {
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };  // סוגי הקלפים
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" }; // ערכי הקלפים

        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                Card newCard = new Card();
                newCard.cardName = value + " of " + suit;
                newCard.value = GetCardValue(value);  // קובעים את הערך של הקלף
                newCard.suit = suit;
                deck.Add(newCard);
            }
        }
    }

    // שיטה שמחשבת את הערך של כל קלף
    int GetCardValue(string value)
    {
        switch (value)
        {
            case "Jack": return 11;
            case "Queen": return 12;
            case "King": return 13;
            case "Ace": return 1;  // ה-Ace יהיה 1
            default: return int.Parse(value);  // כל מספר אחר
        }
    }

    // ערבוב הקופה
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // חלוקה של קלפים לשחקנים
    void DealCards()
    {
        int currentPlayer = 0; // מתחילים עם השחקן הראשון (שחקן 0)

        // חלוקה של 4 קלפים לכל שחקן
        for (int i = 0; i < 4; i++) // יש 4 שחקנים
        {
            for (int j = 0; j < 4; j++) // כל שחקן מקבל 4 קלפים
            {
                GameObject cardObject = Instantiate(cardPrefab, playerHands[currentPlayer].position, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>(); // קבלת המידע על הקלף

                // טוענים את התמונה של הקלף מתוך תיקיית Resources
                string cardName = deck[i * 4 + j].cardName;
                SpriteRenderer cardSpriteRenderer = cardObject.GetComponent<SpriteRenderer>();
                cardSpriteRenderer.sprite = Resources.Load<Sprite>("Cards/" + cardName);  // טוענים את התמונה

                // מניחים את הקלף ביד של השחקן
                cardObject.transform.SetParent(playerHands[currentPlayer], false);
            }

            // לאחר שסיימנו לחלק 4 קלפים לשחקן, נעבור לשחקן הבא
            currentPlayer = (currentPlayer + 1) % 4;  // זהו נוסחה שמחזירה את השחקן הבא, 0-3
        }

        // אם מדובר בסיבוב הראשון, מחלקים 4 קלפים גם למרכז
        if (isFirstRound)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject cardObject = Instantiate(cardPrefab, centerPile.position, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>(); // קבלת המידע על הקלף

                // טוענים את התמונה של הקלף מתוך תיקיית Resources
                string cardName = deck[16 + i].cardName;
                SpriteRenderer cardSpriteRenderer = cardObject.GetComponent<SpriteRenderer>();
                cardSpriteRenderer.sprite = Resources.Load<Sprite>("Cards/" + cardName);  // טוענים את התמונה

                // מניחים את הקלף במרכז
                cardObject.transform.SetParent(centerPile, false);
            }

            // לאחר החלוקה הראשונית, הסיבוב הראשון הסתיים
            isFirstRound = false;
        }
    }

    // השחקן לוקח את הקלף ומניח אותו בקופה הפרטית שלו
    void TakeCard(Card card)
    {
        card.transform.SetParent(playerPrivatePiles[currentPlayer], false);  // מניח את הקלף בקופה הפרטית
        centerCards.Remove(card);  // מסיר את הקלף מהשולחן
        Debug.Log("Card " + card.cardName + " added to private pile.");
    }

    // לוקח את כל הקלפים שנמצאים על השולחן ומניח אותם בקופה הפרטית
    void TakeCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.transform.SetParent(playerPrivatePiles[currentPlayer], false);  // מניח את הקלף בקופה הפרטית
            centerCards.Remove(card);  // מסיר את הקלף מהשולחן
        }
    }

    // מעביר לשחקן הבא בתור
    void NextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % 4; // מחליף לשחקן הבא בתור
        Debug.Log("Next player is player " + currentPlayer);
    }
}










