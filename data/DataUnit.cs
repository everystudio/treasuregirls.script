using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUnitParam :CsvDataParam
{
    // プロジェクトごとに使い分ける必要あり
    public int unit_id;

}

public class DataUnit : CsvData<DataUnitParam>
{
}
