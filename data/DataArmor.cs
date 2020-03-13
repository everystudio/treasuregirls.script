using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataArmorParam : CsvDataParam
{
    public string position { get; set; }
    public int level { get; set; }
}

public class DataArmor : CsvData<DataArmorParam>
{
}
