using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class StageManager : MonoBehaviour
{
    public int currentStage;
    public int allEnemyCount;
    public int currentEnemyCount;
    public EnemyType appearEnemy;
    public float enemySpawnDelay;
    public bool isStarted;

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

    /// <summary>
    /// Enemy Spawn Mechanism
    /// 1. Get Enemy Type
    /// 2. Get Enemy SpawnPoint
    /// 3. Spawn & Initialize Stat
    /// </summary>
    void EnemySpawn()
    {
        int spawnEnemy = Random.Range(0, (int)appearEnemy);
        //Instantiate(enemyList[spawnEnemy]).TryGetComponent<EnemyBase>(out EnemyBase enemy);
        //enemy.transform.position = CalculateRandomPoint(spawnPoints[Random.Range(0, spawnPoints.Length)]);
        InGameManager.Instance.enemyManager.SpawnEnemy((EnemyType)spawnEnemy);
    }
}