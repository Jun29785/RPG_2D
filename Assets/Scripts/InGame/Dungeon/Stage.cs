using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class Stage : MonoBehaviour
{
    public int currentStage;
    public int allEnemyCount;
    public int currentEnemyCount;
    public List<EnemyType> appearEnemy;
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
            Invoke("EnemySpawnTimer", enemySpawnDelay);
            return;
        }
        return;
    }
}