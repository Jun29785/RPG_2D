using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Define;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    public int unitLayer;

    #region EnemySpawn
    [Header("Field")]
    [SerializeField] SpriteRenderer field;
    private float fieldWidth;
    private float fieldHeight;
    [SerializeField] private Vector2[] vertexs;
    [SerializeField] [Range(0f, 10f)] private float spawnRange = 1.2f;
    #endregion

    void Start()
    {
        unitLayer = LayerMask.NameToLayer("Unit");
        enemyPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Enemy"));
        fieldWidth = field.sprite.bounds.size.x * field.transform.lossyScale.x;
        fieldHeight = field.sprite.bounds.size.y * field.transform.lossyScale.y;
        float fieldX = field.transform.position.x; float fieldY = field.transform.position.y;
        vertexs = new Vector2[]{
            new Vector2(fieldX - (fieldWidth / 2), fieldY - (fieldHeight / 2)),
            new Vector2(fieldX - (fieldWidth / 2), fieldY + (fieldHeight / 2)),
            new Vector2(fieldX + (fieldWidth / 2), fieldY + (fieldHeight / 2)),
            new Vector2(fieldX + (fieldWidth / 2), fieldY - (fieldHeight / 2))
        };
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
        atk.damage = damage;
        atk.speed = speed;
        Destroy(atk.gameObject, .5f);
        return atk.gameObject;
    }

    /// <summary>
    /// 적을 생성하는 함수 type : 생성할 적
    /// </summary>
    private GameObject SpawnEnemy(EnemyType type)
    {
        GameObject enemy = Instantiate(enemyPrefabs[(int)type]);
        enemy.transform.position = GetSpawnPosition();

        return enemy;
    }

    /// <summary>
    /// 적이 생성되는 위치를 구하는 함수
    /// </summary>
    /// <returns></returns>
    private Vector2 GetSpawnPosition()
    {
        int randNumber = Random.Range(0, vertexs.Length);
        Vector2 firstSelectedVector = vertexs[randNumber];
        if (Random.Range(0, 2) == 0) randNumber--;
        else randNumber++;
        Vector2 secondSelectedVector = vertexs[(randNumber + 4) % 4];
        float x = firstSelectedVector.x > secondSelectedVector.x ? Random.Range(secondSelectedVector.x, firstSelectedVector.x) : Random.Range(firstSelectedVector.x, secondSelectedVector.x);
        float y = firstSelectedVector.y > secondSelectedVector.y ? Random.Range(secondSelectedVector.y, firstSelectedVector.y) : Random.Range(firstSelectedVector.y, secondSelectedVector.y);
        return new Vector2(x + Random.Range(-spawnRange,spawnRange),y + Random.Range(-spawnRange,spawnRange));
    }
}
