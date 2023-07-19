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
        
    }

    void Update()
    {
        InitUserData();
    }

    public void InitUserData()
    {
        data = new UserData();
        data.UserName = "ABC";
        data.TeamLimit = 1;
        foreach (UnitType type in Enum.GetValues(typeof(UnitType)))
        {
            data.unlockedUnits[type] = false;
            data.selectedUnits[type] = false;
        }
    }
}
