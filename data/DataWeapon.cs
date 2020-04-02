using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataWeaponEvent : UnityEvent<DataWeaponParam>
{
}

public class DataWeaponParam : CsvDataParam
{
	public int serial { get; set; }
	public int equip { get; set; }

	public int weapon_id { get; set; }
	public int level { get; set; }

    public DataWeaponParam() { }
    public DataWeaponParam(int _serial , int _weapon_id) {
        serial = _serial;
        weapon_id = _weapon_id;
    }

}

public class DataWeapon : CsvData<DataWeaponParam>
{
    public void Add(DataWeaponParam _data )
    {
        int add_serial = 1;
        foreach (DataWeaponParam data in list)
        {
            if (add_serial <= data.serial)
            {
                add_serial = data.serial + 1;
            }
        }
        _data.serial = add_serial;
        list.Add(_data);
    }

    public bool AddAlbum( int _iWeaponId)
    {
        DataWeaponParam check_exist = list.Find(p => p.weapon_id == _iWeaponId);
        if(check_exist != null)
        {
            return false;
        }

        MasterWeaponParam master_exist = DataManager.Instance.masterWeapon.list.Find(p => p.weapon_id == _iWeaponId);
        if(master_exist == null)
        {
            // そもそもない
            return false;
        }

        // シリアルは使わない
        DataWeaponParam add = new DataWeaponParam(0, _iWeaponId);
        list.Add(add);
        return true;
    }

}
