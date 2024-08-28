using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "Create Card Library")]
public class CardLibrary : ScriptableObject
{
    [FormerlySerializedAs("prefabs")] public List<CardData> cards;

    private Dictionary<string, CardData> dictionary;

    public void Initialize()
    {
        dictionary = new Dictionary<string, CardData>();
        foreach (var card in cards)
        {
            dictionary[card.cardName] = card;
        }
    }

    public CardData GetCard(string cardName)
    {
        return dictionary[cardName];
    }
}
