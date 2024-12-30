using UnityEngine;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour
{
    public List<Card> hand = new List<Card>();  // ����� �� ����� �� �����

    // ���� ������ ��� ���
    public void AddCardToHand(Card card)
    {
        hand.Add(card);
        // ��� ���� ������ �� ���� ��� ����� �� ���� �� ���� �� ����
        Debug.Log("Card added to hand: " + card.cardName);
    }
}

