using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;

public class EnemyAttack : MonoBehaviour
{
    public float speed;
    public int damage;
    public List<GameEffect> effects;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == InGameManager.Instance.enemyManager.unitLayer)
        {
            collision.transform.GetComponent<UnitBase>()
                .getDamageEvent.Invoke((int)(damage), effects);
        }
    }
}
