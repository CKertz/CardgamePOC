using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Handlers
{
    class BillCostHandler
    {
        static int billCost;

        //$3, $10, -$3, -$10, -$125
        public string updateBillCostText(string itemCost)
        {
            int updatedItemCost;
            string strippedString = itemCost.Replace("$", "");
            updatedItemCost = int.Parse(strippedString);

            billCost += updatedItemCost;

            return billCost.ToString();
        }
    }
}
