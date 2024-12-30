using UnityEngine;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour
{
    public List<Card> hand = new List<Card>();  // רשימה של קלפים של השחקן

    // שיטה להוסיף קלף ליד
    public void AddCardToHand(Card card)
    {
        hand.Add(card);
        // כאן תוכל להוסיף את הקוד כדי להציג את הקלף על המסך אם צריך
        Debug.Log("Card added to hand: " + card.cardName);
    }
}

