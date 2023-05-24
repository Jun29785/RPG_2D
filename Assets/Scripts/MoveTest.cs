using UnityEngine;

public class MoveTest : MonoBehaviour
{
    public SpriteRenderer field;
    float field_width;
    float field_height;

    public Vector2 field_Left;
    public Vector2 field_Right;
    public Vector2 field_Up;
    public Vector2 field_Down;

    public float weight_Left;
    public float weight_Right;
    public float weight_Up;
    public float weight_Down;

    public DirectionW X_DirW = new DirectionW();
    public DirectionW Y_DirW = new DirectionW();

    public Vector2 DirectionVector = Vector2.zero;

    public Transform perpendicularTransform;
    [Header("Test")]
    public LineRenderer x_line;
    public LineRenderer y_line;
    public LineRenderer vector_line;
    public LineRenderer perpendicular_line;

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
        //if (MapRangeCheck()) return;
        curMoveWaitDelay += Time.deltaTime;
        if (curMoveWaitDelay < maxMoveWaitDuration && IsMove)
        {
            transform.Translate(DirectionVector * speed * Time.deltaTime);
        }
        else 
        {
            IsMove = false;
            curMoveWaitDelay = 0f;
            RandomMove();
            IsMove = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetBorderValue();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RandomMove();
        }
    }

    bool MapRangeCheck()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        if (x > field_Right.x || x < field_Left.x || y > field_Up.y || y < field_Down.y)
        {
            Debug.Log($"x: {x} y: {y}");
            return true;
        }
        return false;
    }

   void RandomMove()
    {
        SetBorderValue();
        AllocDistance2Weight();
        SetWeight(ref weight_Left, ref weight_Right);
        SetWeight(ref weight_Up, ref weight_Down);
        X_DirW = SelectDirW(new DirectionW(field_Left, weight_Left), new DirectionW(field_Right, weight_Right));
        Y_DirW = SelectDirW(new DirectionW(field_Up, weight_Up), new DirectionW(field_Down, weight_Down));
        //SelectDirW(ref X_DirW, new DirectionW(Vector2.left, weight_Left), new DirectionW(Vector2.right, weight_Right));
        //SelectDirW(ref Y_DirW, new DirectionW(Vector2.up, weight_Up), new DirectionW(Vector2.down, weight_Down));
        DirectionVector = GetDirection().normalized;
        x_line.SetPosition(0, field_Left);
        x_line.SetPosition(1, field_Right);
        y_line.SetPosition(0, field_Up);
        y_line.SetPosition(1, field_Down);
        vector_line.SetPosition(0, X_DirW.direction);
        vector_line.SetPosition(1, Y_DirW.direction);
        perpendicular_line.SetPosition(0, transform.position);
        perpendicular_line.SetPosition(1, perpendicularTransform.position);
    }

    void SetBorderValue()
    {
        field_Left = new Vector2(field.transform.position.x - (field_width / 2), transform.position.y);
        field_Right = new Vector2(field.transform.position.x + (field_width / 2), transform.position.y);
        field_Up = new Vector2(transform.position.x, field.transform.position.y + (field_height / 2));
        field_Down = new Vector2(transform.position.x, field.transform.position.y - (field_height / 2));
    }

    void AllocDistance2Weight()
    {
        weight_Left = Vector2.Distance(transform.position, field_Left);
        weight_Right = Vector2.Distance(transform.position, field_Right);
        weight_Up = Vector2.Distance(transform.position, field_Up);
        weight_Down = Vector2.Distance(transform.position, field_Down);
    }

    void SetWeight(ref float arg1, ref float arg2)
    {
        float sum = arg1 + arg2;
        arg1 = Mathf.Round(arg1 / sum * 100f) / 100f;
        arg2 = Mathf.Round(arg2 / sum * 100f) / 100f;
    }

    DirectionW SelectDirW(DirectionW arg1, DirectionW arg2)
    {
        if (Random.Range(0f, 1f) > arg1.weight)
        {
            return arg2;
        }
        else return arg1;
    }

    Vector2 GetDirection()
    {
        Vector2 c_Vector = X_DirW.direction - Y_DirW.direction;
        float x_Length = X_DirW.direction.x - transform.position.x;
        float y_Length = Y_DirW.direction.y - transform.position.y;
        float scala = Mathf.Pow(y_Length, 2) / Mathf.Pow(c_Vector.magnitude, 2);
        Vector3 perpendicularPoint = new Vector2(x_Length * scala, y_Length * (1 - scala));
        perpendicularTransform.localPosition = perpendicularPoint;
        return perpendicularTransform.position - transform.position;
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