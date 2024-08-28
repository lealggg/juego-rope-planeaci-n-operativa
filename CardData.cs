using System;
using UnityEngine;

[Serializable]
public class CardData
{
    public string cardName;
    public string description;
    public int points;
    public Sprite image;

    public void PrintCardData()
    {
        Debug.Log(cardName + ": " + description);
    }
}
