using System.Collections.Generic;
using Define;

[System.Serializable]
public class UserData
{
    public string UserName;

    public int TeamLimit;

    public Dictionary<UnitType, bool> unlockedUnits = new Dictionary<UnitType, bool>();
    public Dictionary<UnitType, bool> selectedUnits = new Dictionary<UnitType, bool>();
}
