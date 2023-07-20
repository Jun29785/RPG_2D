using System.Collections.Generic;
using Define;

[System.Serializable]
public class UserData
{
    public string UserName;

    public int TeamLimit;

    public Dictionary<int, bool> unlockedUnits = new Dictionary<int, bool>();
    public Dictionary<int, bool> selectedUnits = new Dictionary<int, bool>();
}
