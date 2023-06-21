using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    public class MenuItem
    {
        public string MenuItemSpritePath { get; set; }
        public List<Ingredient> IngredientList { get; set; }
        public string MenuItemName { get; set; }
        public float MenuItemPrice { get; set; }
    }
}
