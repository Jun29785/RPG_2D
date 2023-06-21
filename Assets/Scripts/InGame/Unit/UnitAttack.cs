using System.Collections.Generic;
using UnityEngine;
using Define;

public class UnitAttack : MonoBehaviour
{
    [Header("Attack Projectile Info")]
    public float speed;
    public int damage;
    public Vector2 direction;
    public List<GameEffect> effect;
    public int increaseValue;
    [SerializeField] protected AttackKind kind = AttackKind.None;

    protected virtual void Update()
    {
        if (kind == AttackKind.None)
        {
            kind = increaseValue == 100 ? AttackKind.Normal : AttackKind.Critical;
            if (kind == AttackKind.Normal)
                GetComponent<SpriteRenderer>().color = Color.black;
            else
                GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 1 << InGameManager.Instance.unitManager.enemyLayer)
        {
            collision.transform.GetComponent<EnemyBase>().getDamage.Invoke((int)(damage * (increaseValue/100f)), effect, kind);
        }
    }
}
