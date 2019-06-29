using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommodityModel : Singleton<CommodityModel> {

    Dictionary<int, Dictionary<string, string>> data;

    public CommodityModel()
    {
        data = CommodityData.Instance.data;
    }

    public List<int> GetAllIDs()
    {
        List<int> res = new List<int>();
        foreach (var item in data)
        {
            res.Add(item.Key);
        }
        return res;
    }

    public string GetCommodityName(int id)
    {
        if (data.ContainsKey(id))
        {
            return data[id]["CommodityName"];
        }
        return "";
    }

    public int GetCommodityCost(int id)
    {
        if (data.ContainsKey(id))
        {
            return int.Parse(data[id]["CostCoin"]);
        }
        return 0;
    }

    public string GetDescription(int id)
    {
        if (data.ContainsKey(id))
        {
            return data[id]["Description"];
        }
        return "";
    }
}
