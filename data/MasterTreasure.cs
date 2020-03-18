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
}

public class MasterTreasure : CsvData<MasterTreasureParam>
{
}
