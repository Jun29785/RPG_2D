using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int currentStage;
    public int allEnemies;
    public int currentEnemies;
    public List<EnemyBase> enemies;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public GameObject EnemySpawn(GameObject enemy, Transform location)
    {
        Instantiate(enemy).TryGetComponent<EnemyBase>(out EnemyBase en);
        en.transform.position = location.position;
        return null;
    }
}
