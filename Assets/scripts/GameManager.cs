using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab �� ���
    public Transform[] playerHands; // ����� �� �������
    public Transform centerPile; // ����� �������
    public List<Card> deck = new List<Card>(); // ���� �� �����
    public Transform[] playerPrivatePiles; // ������ ������� �� �� ����
    private bool isFirstRound = true; // ��� ����� ������ ������
    private int currentPlayer = 0; // ����� ����
    private bool gameIsActive = true; // ��� ����� ����?
    private List<Card> centerCards = new List<Card>(); // ����� �� ������ (����� �������)
    private int[] teamScores = new int[2]; // ����� �� �� ����� (����� 1: 0, ����� 2: 1)
    private bool[] teamHasBonus = new bool[2]; // ��� �� ����� ����� �����

    void Start()
    {
        InitializeDeck();
        ShuffleDeck();
        DealCards();
    }

    // ����� ���� �� 52 �����
    void InitializeDeck()
    {
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };  // ���� ������
        string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" }; // ���� ������

        foreach (string suit in suits)
        {
            foreach (string value in values)
            {
                Card newCard = new Card();
                newCard.cardName = value + " of " + suit;
                newCard.value = GetCardValue(value);  // ������ �� ���� �� ����
                newCard.suit = suit;
                deck.Add(newCard);
            }
        }
    }

    // ���� ������ �� ���� �� �� ���
    int GetCardValue(string value)
    {
        switch (value)
        {
            case "Jack": return 11;
            case "Queen": return 12;
            case "King": return 13;
            case "Ace": return 1;  // �-Ace ���� 1
            default: return int.Parse(value);  // �� ���� ���
        }
    }

    // ����� �����
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

    // ����� �� ����� �������
    void DealCards()
    {
        int currentPlayer = 0; // ������� �� ����� ������ (���� 0)

        // ����� �� 4 ����� ��� ����
        for (int i = 0; i < 4; i++) // �� 4 ������
        {
            for (int j = 0; j < 4; j++) // �� ���� ���� 4 �����
            {
                GameObject cardObject = Instantiate(cardPrefab, playerHands[currentPlayer].position, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>(); // ���� ����� �� ����

                // ������ �� ������ �� ���� ���� ������ Resources
                string cardName = deck[i * 4 + j].cardName;
                SpriteRenderer cardSpriteRenderer = cardObject.GetComponent<SpriteRenderer>();
                cardSpriteRenderer.sprite = Resources.Load<Sprite>("Cards/" + cardName);  // ������ �� ������

                // ������ �� ���� ��� �� �����
                cardObject.transform.SetParent(playerHands[currentPlayer], false);
            }

            // ���� ������� ���� 4 ����� �����, ����� ����� ���
            currentPlayer = (currentPlayer + 1) % 4;  // ��� ����� ������� �� ����� ���, 0-3
        }

        // �� ����� ������ ������, ������ 4 ����� �� �����
        if (isFirstRound)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject cardObject = Instantiate(cardPrefab, centerPile.position, Quaternion.identity);
                Card card = cardObject.GetComponent<Card>(); // ���� ����� �� ����

                // ������ �� ������ �� ���� ���� ������ Resources
                string cardName = deck[16 + i].cardName;
                SpriteRenderer cardSpriteRenderer = cardObject.GetComponent<SpriteRenderer>();
                cardSpriteRenderer.sprite = Resources.Load<Sprite>("Cards/" + cardName);  // ������ �� ������

                // ������ �� ���� �����
                cardObject.transform.SetParent(centerPile, false);
            }

            // ���� ������ ��������, ������ ������ ������
            isFirstRound = false;
        }
    }

    // ����� ���� �� ���� ����� ���� ����� ������ ���
    void TakeCard(Card card)
    {
        card.transform.SetParent(playerPrivatePiles[currentPlayer], false);  // ���� �� ���� ����� ������
        centerCards.Remove(card);  // ���� �� ���� �������
        Debug.Log("Card " + card.cardName + " added to private pile.");
    }

    // ���� �� �� ������ ������� �� ������ ����� ���� ����� ������
    void TakeCards(List<Card> cards)
    {
        foreach (Card card in cards)
        {
            card.transform.SetParent(playerPrivatePiles[currentPlayer], false);  // ���� �� ���� ����� ������
            centerCards.Remove(card);  // ���� �� ���� �������
        }
    }

    // ����� ����� ��� ����
    void NextPlayer()
    {
        currentPlayer = (currentPlayer + 1) % 4; // ����� ����� ��� ����
        Debug.Log("Next player is player " + currentPlayer);
    }
}










