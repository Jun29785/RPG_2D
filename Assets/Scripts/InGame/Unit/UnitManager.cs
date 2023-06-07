using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 유닛의 기본 공격 오브젝트를 생성해주는 함수
    /// attack : 프리팹, unit : 현재 유닛, target : 적 타겟, speed : 오브젝트의 속도, damage : 공격력, t : 라이프타임
    /// </summary>
    public void UnitBasicAttack(GameObject attack, Transform unit, Transform target, float speed, int damage, float t)
    {
        Instantiate(attack).TryGetComponent<UnitBasicAttack>(out UnitBasicAttack basicAttack);
        basicAttack.transform.parent = unit;
        basicAttack.transform.position = new Vector2((unit.position.x + target.position.x)/2,(unit.position.y + target.position.y)/2);
        Vector2 direction = (target.position - unit.position).normalized;
        basicAttack.speed = speed;
        basicAttack.damage = damage;
        basicAttack.direction = direction;
        Destroy(basicAttack.gameObject, t);
    }
}
