using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// ���� ������ �������ִ� �Լ�
    /// prefab : ������ ������Ʈ, enemy : ������ �ϴ� ��, target : ������ ���ϴ� ���, speed : ������Ʈ�� �ӵ�, damage : �ִ� ������, t : ������Ʈ�� ���ӵ� �ð�
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="enemy"></param>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    /// <param name="damage"></param>
    /// <param name="t"></param>
    /// <returns></returns>
    public GameObject EnemyAttack(GameObject prefab, Transform enemy, Transform target, float speed, int damage, float t)
    {
        Instantiate(prefab).TryGetComponent<EnemyAttack>(out EnemyAttack atk);
        atk.transform.parent = enemy;
        atk.transform.position = (enemy.position + target.position) / 2;


        return null;
    }

    public void SpawnEnemy(GameObject enemy)
    {
        Vector2 location = new Vector2();

        CreateEnemyObject(enemy);
    }

    /// <summary>
    /// ���� �����ϴ� �Լ� enemy : ������ �� ������Ʈ
    /// </summary>
    private GameObject CreateEnemyObject(GameObject enemy)
    {
        return Instantiate(enemy);
    }
}
