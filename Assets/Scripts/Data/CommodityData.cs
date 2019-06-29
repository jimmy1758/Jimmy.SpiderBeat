using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommodityData : Singleton<CommodityData> {

    public Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>()
    {
        {1001, new Dictionary<string, string>(){{"CommodityName", "Life Bonus"},{"CostCoin", "500" },{"Description", "Increase your max hp of your spider by 5 forever！" } } },
        {1002, new Dictionary<string, string>(){{"CommodityName", "Combo Bonus"},{"CostCoin", "800" },{"Description", "Increase your initail combos by 1!" } } },
        {1003, new Dictionary<string, string>(){{"CommodityName", "More Bugs"},{"CostCoin", "1000" },{"Description", "Unlcok one random bug style!" } } }
    };
}
