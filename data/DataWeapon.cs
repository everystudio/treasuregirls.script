using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataWeaponParam : CsvDataParam
{
	public int serial { get; set; }
	public int equip { get; set; }

	public int weapon_id { get; set; }
	public int level { get; set; }
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

}
