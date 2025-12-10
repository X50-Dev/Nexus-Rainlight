using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugArrow : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D rb;
    public Vector3 Dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Direction = Player.transform.position - Dir;
        gameObject.transform.rotation = Quaternion.Euler(Direction);
        Dir = Player.transform.position;
    }
}
