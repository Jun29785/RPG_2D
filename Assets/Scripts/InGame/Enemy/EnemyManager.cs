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
    private Vector2[] vertexs;
    [SerializeField] [Range(0f, 10f)] private float spawnRange = 1.2f;
    [SerializeField] private Transform enemyParent;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Keypad0))
        {
            SpawnEnemy(EnemyType.Normal_1);
        }
    }

    private void Initialize()
    {
        unitLayer = LayerMask.NameToLayer("Unit"); /*Unit Layer*/
        enemyPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>("Prefabs/Enemy")); /*Load Enemy Prefabs in Resources*/
        FieldInitialize();
    }

    private void FieldInitialize()
    {
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
        atk.damage = damage;
        atk.speed = speed;
        Destroy(atk.gameObject, .5f);
        return atk.gameObject;
    }

    /// <summary>
    /// ���� �����ϴ� �Լ� type : ������ �� ����
    /// </summary>
    public GameObject SpawnEnemy(EnemyType type)
    {
        GameObject enemy = Instantiate(enemyPrefabs[(int)type],enemyParent);
        enemy.transform.position = GetSpawnPosition();

        return enemy;
    }

    /// <summary>
    /// ���� �����Ǵ� ��ġ�� ���ϴ� �Լ�
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
        return new Vector2(x + Random.Range(-spawnRange, spawnRange), y + Random.Range(-spawnRange, spawnRange));
    }

    public void ClearAllEnemy()
    {
        foreach(Transform enemy in enemyParent)
        {
            Destroy(enemy.gameObject);
        }
    }
}
