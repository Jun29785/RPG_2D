using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public abstract class UnitAttack : MonoBehaviour
{
    [Header("Attack Projectile Info")]
    public float speed;
    public int damage;
    public Vector2 direction;
    public List<GameEffect> effect;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 1 << LayerMask.NameToLayer("Enemy"))
        {
            collision.transform.GetComponent<EnemyBase>().getDamage.Invoke(damage, effect);
        }
    }
}
