using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CircleCollider2D _groundDetector;
    private Animator _animator;

    [SerializeField] private bool _onAction = false, _isDead = false;
    [SerializeField] public int Hp = 10;

    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] public GameObject[] equipment;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger("Attack");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "EnnemyAttack" && gameObject.layer != 6)
        {
            _animator.SetTrigger("TakeDamage");
            TakeDamage(2, true);
        }
    }

    public void TakeDamage(int Damage, bool recoil)
    {
        Hp -= Damage;
        if (recoil)
        {
            _rb.velocity = new Vector2(-_rb.velocity.x, _rb.velocity.y);
        }
        if (Hp <= 0)
        {
            _isDead = true;
            _animator.SetTrigger("Dead");
        }


    }
}
