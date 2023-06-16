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
    public void UnitAttack(GameObject attack, Transform unit, Transform target, float speed, int damage, int increase,float t)
    {
        Instantiate(attack).TryGetComponent<UnitAttack>(out UnitAttack atk);
        atk.transform.parent = unit;
        atk.transform.position = new Vector2((unit.position.x + target.position.x)/2,(unit.position.y + target.position.y)/2);
        Vector2 direction = (target.position - unit.position).normalized;
        atk.speed = speed;
        atk.damage = damage;
        atk.direction = direction;
        atk.increaseValue = increase;
        Destroy(atk.gameObject, t);
    }
}
