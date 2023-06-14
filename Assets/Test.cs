using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform other;

    void Start()
    {
        var distance = Mathf.Pow(Vector2.Distance(transform.position, other.position), 2) + 1;
        Debug.Log(distance);
        var other_x = Mathf.Pow(other.position.x, 2);
        var other_y = Mathf.Pow(other.position.y, 2);
        var x = Mathf.Pow(transform.position.x, 2);
        var y = Mathf.Pow(transform.position.y, 2);
        x = distance * (other_x / ((other_x - x) + (other_y - y)));
        y = distance * (other_y / ((other_x - x) + (other_y - y)));
        Debug.Log($"x : {Mathf.Sqrt(x)}");
        Debug.Log($"y : {Mathf.Sqrt(y)}");
        transform.position = new Vector2(Mathf.Sqrt(x), Mathf.Sqrt(y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
