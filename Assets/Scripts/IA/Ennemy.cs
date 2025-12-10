using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ennemy : Character
{
    //[SerializeField] private CircleCollider2D _groundDetector;
    [SerializeField] public Animator _animator;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private GameObject Target;
    [SerializeField] private Collider2D FoV, FoA;

    public bool attacked = false;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_groundDetector = GetComponent<CircleCollider2D>();
        //_animator = GetComponent<Animator>();
        _Speed = 0;
        _jumpForce = 7;

    }

    // Update is called once per frame
    void Update()
    {        

        _animator.SetInteger("VelocityX", _rb.velocity.x.ConvertTo<int>());


        if (Target == null)
        {
            return;
        }
        else
        {
            OnChase();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Target == null)
            {
                Target = collision.gameObject;
                FoV.enabled = false;
            }

            else
            {
                _animator.SetTrigger("Attack");
            }
        }
    }

    private void OnChase()
    {
        Vector2 direction = (Target.transform.position - transform.position).normalized;
        _rb.velocity = direction * _Speed;
    }

    public void TakeDamage(int Damage, bool recoil)
    {
        attacked = false;
        _animator.SetTrigger("TakeDamage");
        if(Hp <= 0)
        _animator.SetTrigger("Dead");
    }

}
