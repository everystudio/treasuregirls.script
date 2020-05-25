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
    public string weapon_type { get; set; }

    public MasterWeaponParam GetParam( int _iLevel)
    {
        MasterWeaponParam ret = new MasterWeaponParam();

        ret.weapon_id = weapon_id;
        ret.name = name;
        ret.rarity = rarity;
        ret.attack = attack + (int)((float)attack * ((float)_iLevel / 5.0f));
        ret.speed = speed + (int)((float)speed * ((float)_iLevel / 50.0f));
        ret.ability_id = ability_id;
        ret.ability_rate = ability_rate;
        ret.sprite_name = sprite_name;

        return ret;
    }
    public int GetGachaProbFree()
    {
        switch (rarity)
        {
            case 1:
                return 10000;
            case 2:
                return 1000;
            case 3:
                return 100;
        }
        return 0;
    }
    public int GetGachaProb()
    {
        switch (rarity)
        {
            case 1:
                // 巻物用で一応数字がいる
                return 10;
            case 2:
                return 100000;
            case 3:
                return 25000;
            case 4:
                return 5000;
            case 5:
                return 500;
        }
        return 0;
    }

}

public class MasterWeapon : CsvData<MasterWeaponParam>
{
    public static int GetGradeupPrice(DataWeaponParam _data, MasterWeaponParam _master)
    {
        return (_master.rarity * 2) * 100 * (_data.level + 1);
    }

    public static int GetSellPrice(DataWeaponParam _data, MasterWeaponParam _master)
    {
        return 100 * _master.rarity;
    }
}
