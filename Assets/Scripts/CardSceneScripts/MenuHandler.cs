using Assets.Models;
using System;
using System.Collections.Generic;


namespace Assets.Scripts.CardSceneScripts
{
    internal class MenuHandler
    {
        public void InitializeTodayMenuItems()
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
    }
}
