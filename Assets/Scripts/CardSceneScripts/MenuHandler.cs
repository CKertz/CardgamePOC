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
            cheeseburger.MenuItemName = "burger";
            cheeseburger.MenuItemSpritePath = "MenuItemSprites/menuburger";
            cheeseburger.IngredientList = cheeseburgerIngredients;
            //
            List<Ingredient> friesIngredients = new List<Ingredient>();
            Ingredient potato = new Ingredient();
            potato.IngredientName = "potato";
            friesIngredients.Add(potato);

            MenuItem fries = new MenuItem();
            fries.MenuItemName = "fries";
            fries.MenuItemSpritePath = "MenuItemSprites/menufries";
            fries.IngredientList = friesIngredients;
            //
            List<Ingredient> sodaIngredients = new List<Ingredient>();
            Ingredient sodaIngredient = new Ingredient();
            sodaIngredient.IngredientName = "soda";
            sodaIngredients.Add(sodaIngredient);

            MenuItem drink = new MenuItem();
            drink.MenuItemName = "soda";
            drink.MenuItemSpritePath = "MenuItemSprites/menudrink";
            drink.IngredientList = friesIngredients;
            //
            DataManager.Instance.todayMenuItems.Add(cheeseburger);
            DataManager.Instance.todayMenuItems.Add(fries);
            DataManager.Instance.todayMenuItems.Add(drink);
        }
    }
}
