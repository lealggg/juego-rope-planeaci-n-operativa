using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandUI : MonoBehaviour
{
    public CardGO cardPrefab;
    //public GameObject cardPrefab;
    public Transform handPanel;

    private List<CardGO> cardInstances;

    private void Awake()
    {
        cardInstances = new List<CardGO>();
    }

    public void InstantiateCards(CardData[] cards)
    {
        foreach (CardData c in cards)
        {

            CardGO cardObject = Instantiate(cardPrefab, transform);
            cardObject.UpdateView(c);
            cardInstances.Add(cardObject);
        }
    }
}

