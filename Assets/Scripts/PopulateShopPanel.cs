using Models;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PopulateShopPanel : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject shopItemPrefab; // Reference to the child Panel prefab


    // Start is called before the first frame update
    void Start()
    {
        shopPanel = GameObject.Find("Shop Item Container");
        GetAvailableShopItemsFromJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void InitializeShopItemJson()
    {
        //these should be dynamically added once unlocked but just hardcoding for initial testing

        ShopItem bunShopItem = new ShopItem
        {
            CardName = "Bun",
            CardPrice = 2,
            CardImagePath = "ShopSprites/bun"
        };
        ShopItem pattyShopItem = new ShopItem
        {
            CardName = "Patty",
            CardPrice = 4,
            CardImagePath = "ShopSprites/patty"
        };
        ShopItem cheeseShopItem = new ShopItem
        {
            CardName = "Cheese",
            CardPrice = 3,
            CardImagePath = "ShopSprites/cheese"
        };
        List<ShopItem> shopItemList = new List<ShopItem> { bunShopItem, pattyShopItem, cheeseShopItem };
        string json = JsonConvert.SerializeObject(shopItemList, Formatting.Indented);

        File.WriteAllText(Application.dataPath+"/Models/json/AvailableShopItems.json", json);
    }
    void GetAvailableShopItemsFromJson()
    {
        //TODO: throw the json path into a res file or similar
        string filePath = Application.dataPath + "/Models/json/AvailableShopItems.json";
        string json = File.ReadAllText(filePath);
        List<ShopItem> shopItemList = JsonConvert.DeserializeObject<List<ShopItem>>(json);

        foreach (ShopItem shopItem in shopItemList)
        {
            AddCardPrefabToShopList(shopItem.CardName, shopItem.CardPrice, shopItem.CardImagePath);
        }
    }

    void AddCardPrefabToShopList(string cardName, float cardPrice, string spritePath)
    {
        // Instantiate the child Panel prefab
        GameObject childPanel = Instantiate(shopItemPrefab);
        SetupShopItemData shopItemDataScript = childPanel.GetComponent<SetupShopItemData>();
        if(shopItemDataScript)
        {
            shopItemDataScript.SetShopItemMetadata(cardName,cardPrice,spritePath);
        }
        // Set the parent of the child Panel to be the parent Panel
        childPanel.transform.SetParent(shopPanel.transform);
    }
}
