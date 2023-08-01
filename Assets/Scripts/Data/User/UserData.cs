using System.Collections.Generic;
using System;

[Serializable]
public class UserData
{
    public string UserName;

    public int TeamLimit;

    public SerializableDictionary<int, UnitData> unitDatas = new SerializableDictionary<int, UnitData>();
    public Dictionary<int, bool> unlockedUnits = new Dictionary<int, bool>();
    public Dictionary<int, bool> selectedUnits = new Dictionary<int, bool>();
}

[Serializable]
public class UnitData
{
    public bool isUnlocked;
    public bool isSelected;

    #region Unit Stat Level
    public int attack;
    public int defense;
    public int attackSpeed;
    public int criticalProbability;
    public int criticalAttack;
    public int coolTime;
    #endregion
}