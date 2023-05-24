using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {

    }
}
