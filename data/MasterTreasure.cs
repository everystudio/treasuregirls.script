using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterTreasureParam : CsvDataParam
{
    public int treasure_id { get; set; }
    public string name { get; set; }
    public int rarity { get; set; }

    public string type { get; set; }
    public int param { get; set; }
    public string sprite_name { get; set; }


    public int GetGachaProb()
    {
        switch (rarity)
        {
            case 1:
                return 1000;
            case 2:
                return 500;
            case 3:
                return 100;
            case 4:
                return 25;
            case 5:
                return 5;
        }
        return 0;
    }

}

public class MasterTreasure : CsvData<MasterTreasureParam>
{
    public static int GetGradeupPrice(DataTreasureParam _data , MasterTreasureParam _master)
    {
        return (_master.rarity * 2) * 100 * (_data.level + 1);
    }

    public static int GetSellPrice(DataTreasureParam _data, MasterTreasureParam _master)
    {
        return 100 * _master.rarity;
    }

}
