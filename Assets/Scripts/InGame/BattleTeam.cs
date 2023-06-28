using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine.Events;

public class BattleTeam : MonoBehaviour
{
    private Rigidbody2D rb;
    #region Field
    [Header("Field")]
    [SerializeField] private SpriteRenderer field;
    float field_width;
    float field_height;

    [SerializeField] Vector2 field_Left;
    [SerializeField] Vector2 field_Right;
    [SerializeField] Vector2 field_Up;
    [SerializeField] Vector2 field_Down;
    #endregion
    #region Weight
    float weight_Left;
    float weight_Right;
    float weight_Up;
    float weight_Down;
    #endregion
    #region Direction
    DirectionW X_DirW = new DirectionW();
    DirectionW Y_DirW = new DirectionW();

    Vector2 DirectionVector = Vector2.zero;
    public Transform perpendicularTransform;
    #endregion
    #region Team Unit
    [Header("Team Unit")]
    private List<Transform> basicLocations = new List<Transform>();
    private List<UnitBase> Units = new List<UnitBase>();
    [SerializeField] private Transform unitParent;
    [SerializeField] private Transform basicLocationParent;
    [SerializeField] private TeamState state;
    public UnityEvent changeState;
    #endregion


    #region Test
    [Header("Visual Test")]
    public bool VisualTesting = false;
    [SerializeField] private LineRenderer x_line;
    [SerializeField] private LineRenderer vector_line;
    [SerializeField] private LineRenderer y_line;
    [SerializeField] private LineRenderer perpendicular_line;
    #endregion

    [SerializeField] private WaitForSeconds waitSetDirection = new WaitForSeconds(0.07f);

    [SerializeField] private float speed = 2.5f;
    [SerializeField] private bool IsMove;
    [SerializeField] private float maxMoveWaitDuration;
    [SerializeField] private float curMoveWaitDelay;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        InitializeUnits();
        curMoveWaitDelay = 0f;
        field_width = field.sprite.bounds.size.x * field.transform.lossyScale.x;
        field_height = field.sprite.bounds.size.y * field.transform.lossyScale.y;
        SetBorderValue();
        changeState.AddListener(CheckUnitState);
        changeState.Invoke();
    }

    void Update()
    {
        TeamMove();
    }

    void InitializeUnits()
    {
        foreach (Transform unit in unitParent)
            Units.Add(unit.GetComponent<UnitBase>());
        foreach (Transform location in basicLocationParent)
            basicLocations.Add(location);
        switch (Units.Count)
        {
            case 1:
                basicLocations[0].localPosition = Vector3.zero;
                Units[0].basicLocation = basicLocations[0];
                break;
            case 2:
                basicLocations[0].localPosition = new Vector2(-(Mathf.Sqrt(3) / 3), 0);
                basicLocations[1].localPosition = new Vector2(Mathf.Sqrt(3) / 3, 0);
                Units[0].basicLocation = basicLocations[0];
                Units[1].basicLocation = basicLocations[1];
                break;
            case 3:
                basicLocations[0].localPosition = Vector2.up;
                basicLocations[1].localPosition = new Vector2(-(Mathf.Sqrt(3) / 3), -(1 / 2));
                basicLocations[2].localPosition = new Vector2(Mathf.Sqrt(3) / 3, -(1 / 2));
                Units[0].basicLocation = basicLocations[0];
                Units[1].basicLocation = basicLocations[1];
                Units[2].basicLocation = basicLocations[2];
                break;
            default:
                break;
        }
        foreach (UnitBase unit in Units)
            unit.transform.localPosition = unit.basicLocation.localPosition;
    }

    #region Move Method
    void TeamMove()
    {
        changeState.Invoke();
        if (state == TeamState.Wait) return;
        curMoveWaitDelay += Time.deltaTime;
        if (curMoveWaitDelay < maxMoveWaitDuration && IsMove)
        {
            transform.Translate(DirectionVector * speed * Time.deltaTime);
            //rb.MovePosition(transform.position + (Vector3)DirectionVector * speed * Time.deltaTime);
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
        DirectionVector = GetDirection();
        maxMoveWaitDuration = DirectionVector.magnitude / speed;
        DirectionVector = DirectionVector.normalized;
        if (VisualTesting) TestCode();
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
    #endregion

    void CheckUnitState()
    {
        state = TeamState.Move;
        foreach (UnitBase unit in Units)
        {
            if (unit.state == UnitState.Individual)
                state = TeamState.Wait;
        }
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

    public DirectionW(Vector2 direction, float weight)
    {
        this.direction = direction;
        this.weight = weight;
    }
}