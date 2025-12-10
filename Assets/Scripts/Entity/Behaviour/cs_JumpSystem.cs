using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cs_JumpSystem : MonoBehaviour
{
    [Header("")]
    public bool bOnJump;
    [SerializeField] private bool bIsGrounded;
    [SerializeField] private bool bHasJumped;

    [SerializeField] private float _timerJump = 0f;

    [Header("Gravity Settings")]
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float normalGravity;


    [Header("")]
    [SerializeField] public float jumpTimeCounter;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _groundCount;

    [Header("")]
    [SerializeField] private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (_groundCount > 0)
        {
            bIsGrounded = true;
            bHasJumped = false;
        }
        else
            bIsGrounded = false;

        //Jumping();

    }

    public void Jumping(int bOnJump)
    {
        print(bOnJump);
        if (bOnJump == 1 && !bHasJumped && bIsGrounded)
        {
            if (/*jumpTimeCounter > 0*/true)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                //_animator.SetBool("Jump", true);
            }
            else
            {

            }
        }

        /*        if (!bIsGrounded)
                {
        */           ///////////
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier; // descente plus rapide
        }
        else if (rb.velocity.y > 0 && bOnJump == 0)
        {
            rb.gravityScale = lowJumpMultiplier; // saut écourté
            bHasJumped = true;
        }
        else
        {
            rb.gravityScale = normalGravity; // montée normale
        }
        /*        }
                else bHasJumped = false;
        */
    }



    private void TouchGround()
    {
        rb.gravityScale = normalGravity;
        bHasJumped = false;
        jumpTimeCounter = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            _groundCount++;
            //_animator.SetBool("Jump", false);
            bHasJumped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundCount--;
        }
    }
}
