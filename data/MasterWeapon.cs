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

    public MasterWeaponParam GetParam( int _iLevel)
    {
        MasterWeaponParam ret = new MasterWeaponParam();

        ret.weapon_id = weapon_id;
        ret.name = name;
        ret.rarity = rarity;
        ret.attack = attack + (int)((float)attack * ((float)_iLevel / 100.0f));
        ret.speed = speed + (int)((float)speed * ((float)_iLevel / 100.0f));
        ret.ability_id = ability_id;
        ret.ability_rate = ability_rate;
        ret.sprite_name = sprite_name;

        return ret;
    }

}

public class MasterWeapon : CsvData<MasterWeaponParam>
{
}
