using System;
using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public UserData data;

    void Start()
    {
        InitUserData();
        data.unlockedUnits[(int)UnitType.Bandit] = true;
        data.selectedUnits[(int)UnitType.Bandit] = true;
        data.unlockedUnits[(int)UnitType.Knight] = true;
        data.selectedUnits[(int)UnitType.Knight] = true;
    }

    void Update()
    {
    }

    public void InitUserData()
    {
        data = new UserData();
        data.UserName = "ABC";
        data.TeamLimit = 3;
        foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
        {
            data.unlockedUnits[(int)type] = false;
            data.selectedUnits[(int)type] = false;
        }
    }
}
