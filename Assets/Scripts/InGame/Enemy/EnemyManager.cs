using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// 적의 공격을 생성해주는 함수
    /// prefab : 생성할 오브젝트, enemy : 공격을 하는 적, target : 공격을 당하는 대상, speed : 오브젝트의 속도, damage : 주는 데미지, t : 오브젝트가 지속될 시간
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
}
