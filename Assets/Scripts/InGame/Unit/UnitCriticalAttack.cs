using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCriticalAttack : UnitAttack
{
    public int increaseValue;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Enemy"))
        {
            collision.transform.GetComponent<EnemyBase>().getDamage.Invoke((int)(damage * (increaseValue/100f)), effect);
        }
    }
}
