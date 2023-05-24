using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour
{
    public SpriteRenderer field;
    float field_width;
    float field_height;

    Vector2 field_Left;
    Vector2 field_Right;
    Vector2 field_Up;
    Vector2 field_Down;

    float weight_Left;
    float weight_Right;
    float weight_Up;
    float weight_Down;

    DirectionW X_DirW = new DirectionW();
    DirectionW Y_DirW = new DirectionW();

    Vector2 DirectionVector = Vector2.zero;

    public Transform perpendicularTransform;
    [Header("Test")]
    [SerializeField] private LineRenderer x_line;
    [SerializeField] private LineRenderer vector_line;
    [SerializeField] private LineRenderer y_line;
    [SerializeField] private LineRenderer perpendicular_line;

    [SerializeField] private WaitForSeconds waitSetDirection = new WaitForSeconds(1.7f);

    public float speed = 2.5f;
    public bool IsMove;
    public float maxMoveWaitDuration;
    public float curMoveWaitDelay;

    public Vector2 TestVector;
    public float temp;
    void Start()
    {
        curMoveWaitDelay = 0f;
        field_width = field.sprite.bounds.size.x * field.transform.lossyScale.x;
        field_height = field.sprite.bounds.size.y * field.transform.lossyScale.y;
        SetBorderValue();
    }

    void Update()
    {
        curMoveWaitDelay += Time.deltaTime;
        if (curMoveWaitDelay < maxMoveWaitDuration && IsMove)
        {
            transform.Translate(DirectionVector * speed * Time.deltaTime);
        }
        else 
        {
            IsMove = false;
            curMoveWaitDelay = 0f;
            StartCoroutine(RandomMove());
            IsMove = true;
        }
    }
    
    IEnumerator RandomMove()
    {
        DirectionVector = Vector2.zero;
        yield return waitSetDirection;
        SetBorderValue();
        AllocDistance2Weight();
        SetWeight(ref weight_Left, ref weight_Right);
        SetWeight(ref weight_Up, ref weight_Down);
        X_DirW = SelectDirW(new DirectionW(field_Left, weight_Left), new DirectionW(field_Right, weight_Right));
        Y_DirW = SelectDirW(new DirectionW(field_Up, weight_Up), new DirectionW(field_Down, weight_Down));
        DirectionVector = GetDirection().normalized;
    }

    void SetBorderValue()
    {
        float field_x = field.transform.position.x; float field_y = field.transform.position.y;
        float x = transform.position.x; float y = transform.position.y;
        field_Left = new Vector2(field_x - (field_width / 2), y);
        field_Right = new Vector2(field_x + (field_width / 2), y);
        field_Up = new Vector2(x, field_y + (field_height / 2));
        field_Down = new Vector2(x, field_y - (field_height / 2));
    }

    void AllocDistance2Weight()
    {
        var pos = transform.position;
        weight_Left = Vector2.Distance(pos, field_Left);
        weight_Right = Vector2.Distance(pos, field_Right);
        weight_Up = Vector2.Distance(pos, field_Up);
        weight_Down = Vector2.Distance(pos, field_Down);
    }

    void SetWeight(ref float arg1, ref float arg2)
    {
        float sum = arg1 + arg2;
        arg1 = Mathf.Round(arg1 / sum * 100f) / 100f;
        arg2 = Mathf.Round(arg2 / sum * 100f) / 100f;
    }

    DirectionW SelectDirW(DirectionW arg1, DirectionW arg2)
    {
        if (arg1.weight >= 0.9)
            return arg1;
        if (arg1.weight <= 0.1)
            return arg2;
        if (Random.Range(0f, 1f) > arg1.weight)
            return arg2;
        else return arg1;
    }

    Vector2 GetDirection()
    {
        var pos = transform.position;
        Vector2 c_Vector = X_DirW.direction - Y_DirW.direction;
        float x_Length = X_DirW.direction.x - pos.x;
        float y_Length = Y_DirW.direction.y - pos.y;
        float scala = Mathf.Pow(y_Length, 2) / Mathf.Pow(c_Vector.magnitude, 2);
        Vector3 perpendicularPoint = new Vector2(x_Length * scala, y_Length * (1 - scala));
        perpendicularTransform.localPosition = perpendicularPoint;
        return perpendicularTransform.position - pos;
    }

    void TestCode()
    {
        x_line.SetPosition(0, field_Left);
        x_line.SetPosition(1, field_Right);
        y_line.SetPosition(0, field_Up);
        y_line.SetPosition(1, field_Down);
        vector_line.SetPosition(0, X_DirW.direction);
        vector_line.SetPosition(1, Y_DirW.direction);
        perpendicular_line.SetPosition(0, transform.position);
        perpendicular_line.SetPosition(1, perpendicularTransform.position);
    }

    //Vector2 CrossCheck(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    //{
    //    float x1, x2, x3, x4, y1, y2, y3, y4, X, Y;

    //    x1 = a.x; y1 = a.y;
    //    x2 = b.x; y2 = b.y;
    //    x3 = c.x; y3 = c.y;
    //    x4 = d.x; y4 = d.y;

    //    float cross = ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
    //    if (cross == 0) return new Vector2(10000, 10000);

    //    X = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / cross;
    //    Y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / cross;

    //    return new Vector2(X, Y);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("wall"))
        {
            Debug.Log("Collision with wall Start RandomMove()");
            StartCoroutine(RandomMove());
        }
    }
}

[System.Serializable]
public class DirectionW
{
    public Vector2 direction;
    public float weight;
    
    public DirectionW()
    {

    }

    public DirectionW(Vector2 direction,float weight)
    {
        this.direction = direction;
        this.weight = weight;
    }
}