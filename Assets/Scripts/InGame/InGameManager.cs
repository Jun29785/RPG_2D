using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;

    public UnitManager unitManager;
    public EnemyManager enemyManager;
    public StageManager stageManager;

    public InGameMainCanvas canvas;

    public int teamStrength;
    public int userCoin;


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

    public Transform GetClosetTarget(Collider2D[] colliders,Transform obj)
    {
        float closetDis = float.PositiveInfinity;
        Transform ClosetTarget = null;
        foreach (Collider2D collider in colliders)
        {
            if (collider == null) continue;
            Vector3 offset = obj.transform.position - collider.transform.position;

            if (offset.sqrMagnitude < closetDis)
            {
                ClosetTarget = collider.transform;
                closetDis = offset.sqrMagnitude;
            }
        }
        return ClosetTarget;
    }
}
