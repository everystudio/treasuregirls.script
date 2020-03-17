using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 使用できるポーションは１つ
public class DataPotionParam : CsvDataParam
{
    public int potion_id { get; set; }
    public bool is_use { get; set; }
    public int num { get; set; }
}

public class DataPotion : CsvData<DataPotionParam>
{

}
