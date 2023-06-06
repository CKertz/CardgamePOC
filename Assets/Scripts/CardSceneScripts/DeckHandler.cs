using Assets.Models;
using Assets.Scripts;
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
            string associatedRecipe = lookupCardAssociatedRecipeByName(keyValue.Key);

            int cardToCreateCount = 0;
            while (cardToCreateCount < keyValue.Value)
            {
                Card card = new Card();
                card.CardName = keyValue.Key;
                card.CardSpritePath = spritePath;
                card.AssociatedRecipePrefabPath = associatedRecipe;
                DataManager.Instance.deck.CardList.Add(card);
                cardToCreateCount++;
            }
        }
        Randomizer.ShuffleList(DataManager.Instance.deck.CardList);
    }

    public string lookupCardSpritePathByName(string cardName)
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

    public string lookupCardAssociatedRecipeByName(string cardName)
    {
        string filePath = Application.dataPath + "/Models/json/AvailableShopItems.json";
        string json = File.ReadAllText(filePath);
        List<ShopItem> shopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(json);

        foreach (ShopItem shopItem in shopItemList)
        {
            if (shopItem.CardName == cardName)
            {
                Debug.Log("associatedrecipe:" + shopItem.AssociatedRecipePrefabPath);
                return shopItem.AssociatedRecipePrefabPath;
            }
        }
        return null;
    }

}
