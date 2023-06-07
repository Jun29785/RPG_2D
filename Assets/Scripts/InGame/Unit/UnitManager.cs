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
    /// ������ �⺻ ���� ������Ʈ�� �������ִ� �Լ�
    /// attack : ������, unit : ���� ����, target : �� Ÿ��, speed : ������Ʈ�� �ӵ�, damage : ���ݷ�, t : ������Ÿ��
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
