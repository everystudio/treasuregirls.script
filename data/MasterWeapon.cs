using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWeaponParam : CsvDataParam
{
    public int weapon_id { get; set; }
    public string name { get; set; }
    public int rarity { get; set; }
    public int attack { get; set; }
    public int speed { get; set; }
    public int ability_id { get; set; }
    public int ability_rate { get; set; }
    public string sprite_name { get; set; }

}

public class MasterWeapon : CsvData<MasterWeaponParam>
{
}
