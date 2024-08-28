using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public void ShuffleDeck(List<CardData> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            CardData temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public List<CardData>[] DealCards(int numberOfPlayers, List<CardData> cards)
    {
        List<CardData>[] playerHands = new List<CardData>[numberOfPlayers];
        for (int i = 0; i < numberOfPlayers; i++)
        {
            playerHands[i] = new List<CardData>();
        }

        int cardsPerPlayer = cards.Count / numberOfPlayers;
        for (int i = 0; i < cardsPerPlayer; i++)
        {
            playerHands[i % numberOfPlayers].Add(cards[i]);
        }

        return playerHands;
    }
}
