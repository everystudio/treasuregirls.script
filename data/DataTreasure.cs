using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataTreasureEvent : UnityEvent<DataTreasureParam>
{
}

public class DataTreasureParam : CsvDataParam
{
    public int serial { get; set; }
    public int treasure_id { get; set; }

    public int equip { get; set; }

    public int level { get; set; }

    public DataTreasureParam(){}
    public DataTreasureParam(int _iSerial , int _iTreasureId)
    {
        serial = _iSerial;
        treasure_id = _iTreasureId;
    }

}

public class DataTreasure : CsvData<DataTreasureParam>
{
    public int Add(int _iTreasureId)
    {
        int add_serial = 1;
        foreach( DataTreasureParam data in list)
        {
            if(add_serial <= data.serial)
            {
                add_serial = data.serial + 1;
            }
        }
        list.Add(new DataTreasureParam(add_serial,_iTreasureId));
        DataManager.Instance.dataTreasureAlbum.AddAlbum(_iTreasureId);
        return add_serial;
    }
    public bool AddAlbum(int _iTreasureId)
    {
        DataTreasureParam check_exist = list.Find(p => p.treasure_id == _iTreasureId);
        if (check_exist != null)
        {
            return false;
        }

        MasterTreasureParam master_exist = DataManager.Instance.masterTreasure.list.Find(p => p.treasure_id == _iTreasureId);
        if (master_exist == null)
        {
            // そもそもない
            return false;
        }

        // シリアルは使わない
        DataTreasureParam add = new DataTreasureParam(0, _iTreasureId);
        list.Add(add);
        return true;
    }

}
