using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureAssist
{
    public int coin { get; set; }
    public int hp { get; set; }
    public int attack { get; set; }
    public int def { get; set; }
    public int mind { get; set; }
    public int move { get; set; }
    public int heal { get; set; }
    public int luck { get; set; }


    public void add_treasure_assist( MasterTreasureParam _treasure , int _iLevel)
    {
        int iParam = _treasure.GetParam(_iLevel);
        switch (_treasure.type)
        {
            case "coin":
                coin += iParam;
                break;
            case "hp":
            case "hp_max":
                hp += iParam;
                break;
            case "def":
                def += iParam;
                break;
            case "mind":
                mind += iParam;
                break;
            case "move":
                move += iParam;
                break;
            case "attack":
                attack += iParam;
                break;
            case "heal":
                heal += iParam;
                break;
            case "luck":
                luck += iParam;
                break;
            default:
                break;
        }
    }

    public static TreasureAssist GetTreasureAssist(List<DataTreasureParam> _data_treasure_list)
    {
        TreasureAssist ret = new TreasureAssist();
        ret.coin = 100;
        ret.hp = 100;
        ret.def = 100;
        ret.mind = 100;
        ret.move = 100;
        ret.attack = 100;
        ret.heal = 100;
        ret.luck = 100;

        foreach (DataTreasureParam data in _data_treasure_list)
        {
            MasterTreasureParam master = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == data.treasure_id);
            ret.add_treasure_assist( master , data.level);
        }
        return ret;
    }
}

public class MasterTreasureParam : CsvDataParam
{
    public int treasure_id { get; set; }
    public string name { get; set; }
    public int rarity { get; set; }

    public string type { get; set; }
    public int param { get; set; }
    public string sprite_name { get; set; }


    public int GetParam(int _iLevel)
    {
        float rate = _iLevel * 0.1f;
        return param + (int)(param * rate);
    }

    public string GetOutline( int _iLevel )
    {
        string ret = "";

        int iShowParam = GetParam(_iLevel);
        switch (type)
        {
            case "coin":
                ret = string.Format("獲得できるコインが{0}%アップ", iShowParam);
                break;
            case "hp":
            case "hp_max":
                ret = string.Format("最大HPが{0}%アップ", iShowParam);
                break;
            case "def":
                ret = string.Format("防御力が{0}%アップ", iShowParam);
                break;
            case "mind":
                ret = string.Format("精神力が{0}%アップ", iShowParam);
                break;
            case "move":
                ret = string.Format("移動スピードが{0}%アップ", iShowParam);
                break;
            case "attack":
                ret = string.Format("攻撃力が{0}%アップ", iShowParam);
                break;
            case "heal":
                ret = string.Format("回復量が{0}%アップ", iShowParam);
                break;
            case "luck":
                ret = string.Format("レア素材ドロップ率がアップ");
                break;
            default:
                ret = "未設定";
                break;
        }
        return ret;
    }

    public int GetGachaProbFree()
    {
        switch (rarity)
        {
            case 1:
                return 100000;
            case 2:
                return 5000;
            case 3:
                return 10;
        }
        return 0;
    }
    public int GetGachaProb()
    {
        switch (rarity)
        {
            // なんか怖くなったので一応入れる。
            case 1:
                return 10;
            case 2:
                return 100000;
            case 3:
                return 5000;
            case 4:
                return 10;
            case 5:
                return 1;
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
