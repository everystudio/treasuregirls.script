using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DataArmorParam : CsvDataParam
{
    public int armor_id { get; set; }
    public string position { get; set; }
    public int level { get; set; }
}

public class DataArmorEvent : UnityEvent<DataArmorParam>
{
}

public class DataArmor : CsvData<DataArmorParam>
{
}
