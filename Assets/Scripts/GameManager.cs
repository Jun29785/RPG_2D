using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public UserDataManager userDataManager;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DataBaseManager.Instance.LoadTable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DataBaseManager.Instance.LoadTable();
            Debug.Log(DataBaseManager.Instance.tdUnitDict.Count);
        }
        
    }
}
