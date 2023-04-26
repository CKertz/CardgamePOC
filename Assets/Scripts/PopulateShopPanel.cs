using Assets.Models;
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
        InitializeTodayMenuItems();
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
    void InitializeTodayMenuItems()
    {
        //TODO: temporary hardcoding the today ingredients for testing
        //
        List<Ingredient> cheeseburgerIngredients = new List<Ingredient>();
        Ingredient cheese = new Ingredient();
        cheese.IngredientName = "cheese";
        cheeseburgerIngredients.Add(cheese);

        Ingredient patty = new Ingredient();
        cheese.IngredientName = "patty";
        cheeseburgerIngredients.Add(patty);

        Ingredient bun = new Ingredient();
        cheese.IngredientName = "bun";
        cheeseburgerIngredients.Add(bun);

        MenuItem cheeseburger = new MenuItem();
        cheeseburger.MenuItemName = "Cheeseburger";
        cheeseburger.MenuItemSpritePath = "MenuItemSprites/cheeseburger";
        cheeseburger.IngredientList = cheeseburgerIngredients;
        //
        List<Ingredient> friesIngredients = new List<Ingredient>();
        Ingredient potato = new Ingredient();
        potato.IngredientName = "potato";
        friesIngredients.Add(potato);

        MenuItem fries = new MenuItem();
        fries.MenuItemName = "Fries";
        fries.MenuItemSpritePath = "MenuItemSprites/fries";
        fries.IngredientList = friesIngredients;
        //
        List<Ingredient> drinkIngredients = new List<Ingredient>();
        Ingredient drinkIngredient = new Ingredient();
        drinkIngredient.IngredientName = "drink";
        drinkIngredients.Add(drinkIngredient);

        MenuItem drink = new MenuItem();
        drink.MenuItemName = "Drink";
        drink.MenuItemSpritePath = "MenuItemSprites/drink";
        drink.IngredientList = friesIngredients;
        //
        DataManager.Instance.todayMenuItems.Add(cheeseburger);
        DataManager.Instance.todayMenuItems.Add(fries);
        DataManager.Instance.todayMenuItems.Add(drink);


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
