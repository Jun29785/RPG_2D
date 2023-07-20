using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : Singleton<DataBaseManager>
{
    public Dictionary<int,TDUnit> tdUnitDict = new Dictionary<int,TDUnit>();

    public void LoadTable()
    {
        LoadUnitTable();
    }

    void LoadUnitTable()
    {
        TextAsset csvText = Resources.Load<TextAsset>("TableBase/UnitBase");

        tdUnitDict.Clear();

        string[] parseObj = csvText.text.Trim().Split("\n");

        foreach(string str in parseObj)
        {
            TDUnit tdUnit = new TDUnit();
            tdUnit.SetCSVData(str);

            tdUnitDict.Add(tdUnit.Key, tdUnit);
        }
    }
}
