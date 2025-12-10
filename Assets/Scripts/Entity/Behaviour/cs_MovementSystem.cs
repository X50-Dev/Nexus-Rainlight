using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_MovementSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    private float _moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void Move(Vector2 moveInput)
    {
        rb.velocity = new Vector2(moveInput.x * _moveSpeed, /*bIsGrounded && moveInput.x != 0 == true ? -0.5f : */rb.velocity.y);
        Rotation(moveInput.x);
    }

    public void Rotation(float moveInput)
    {
        #region Rotation
        if (moveInput > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (moveInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        #endregion
    }
}
