using Assets.Models;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeckHandler 
{
    public void constructDeck()
    {
        foreach(KeyValuePair<string,int> keyValue in DataManager.Instance.quantityCounter)
        {
            string spritePath = lookupCardSpritePathByName(keyValue.Key);

            int cardToCreateCount = 0;
            while (cardToCreateCount < keyValue.Value)
            {
                Card card = new Card();
                card.CardName = keyValue.Key;
                card.CardSpritePath = spritePath;
                DataManager.Instance.deck.CardList.Add(card);
                cardToCreateCount++;
            }
        }
        shuffleDeck();
    }

    private string lookupCardSpritePathByName(string cardName)
    {
        string filePath = Application.dataPath + "/Models/json/AvailableShopItems.json";
        string json = File.ReadAllText(filePath);
        List<ShopItem> shopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(json);

        foreach (ShopItem shopItem in shopItemList)
        {
            if (shopItem.CardName == cardName)
            {
                return shopItem.CardImagePath;
            }
        }
        return null;
    }

    private void shuffleDeck()
    {
        System.Random rand = new System.Random();

        // Shuffle the list using a Fisher-Yates shuffle algorithm
        int n = DataManager.Instance.deck.CardList.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            Card value = DataManager.Instance.deck.CardList[k];
            DataManager.Instance.deck.CardList[k] = DataManager.Instance.deck.CardList[n];
            DataManager.Instance.deck.CardList[n] = value;
        }
    }

}
