using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class StageManager : MonoBehaviour
{
    public int currentStage;
    public int allEnemyCount;
    public int currentEnemyCount;
    public List<EnemyType> appearEnemy;
    public float enemySpawnDelay;
    public bool isStarted;

    [SerializeField] private List<EnemyBase> enemyBaseList;

    [SerializeField] private Rect fieldRect;
    [SerializeField] private float spawnRange;

    void Start()
    {
        
    }

    void Update()
    {
        if (allEnemyCount <= currentEnemyCount && isStarted)
        {
            isStarted = false;
            // nextStage;
            StageStart();
        }
    }

    private void StageStart()
    {
        InGameManager.Instance.enemyManager.ClearAllEnemy();
        currentEnemyCount = 0;

        isStarted = true;
        EnemySpawnTimer();
    }

    private void EnemySpawnTimer()
    {
        if (isStarted)
        {
            EnemySpawn();
            Invoke("EnemySpawnTimer", enemySpawnDelay);
            return;
        }
        return;
    }

    void EnemySpawn()
    {
        int enemy = Random.Range(0, appearEnemy.Count);
        
    }

    float CalculateRandomPoint(float args1, float args2)
    {
        float result = float.PositiveInfinity;
        int compare = args1.CompareTo(args2);
        switch (compare)
        {
            case -1:
            case 0:
                result = Random.Range(args1 - spawnRange, args2 + spawnRange);
                break;
            case 1:
                result = Random.Range(args2 - spawnRange, args1 + spawnRange);
                break;
        }
        return result;
    }
}