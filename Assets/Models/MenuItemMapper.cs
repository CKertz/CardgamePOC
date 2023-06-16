using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class MenuItemMapper
    {
        public readonly Dictionary<string, string> menuItems = new Dictionary<string, string>()
        {
            {"burger","PlatedFoodPrefabs/prefab_BurgerPlated" },
            {"fries","PlatedFoodPrefabs/prefab_FriesPlated"},
            {"soda","PlatedFoodPrefabs/prefab_SodaPlated" }
        };
    }
}
