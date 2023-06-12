using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class PlatedDish
    {
        public string DishPrefabName { get; set; }
        public bool IsCompleted { get; set; }
        public GameObject PlatedDishObject { get; set; }

        public PlatedDish(string dishPrefabName, bool isCompleted, GameObject platedDishObject) 
        {
            this.DishPrefabName = dishPrefabName;
            this.IsCompleted = isCompleted;
            this.PlatedDishObject = platedDishObject;
        }
    }
}
