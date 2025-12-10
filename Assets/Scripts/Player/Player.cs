using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Progress;

public class Player : MonoBehaviour
{
    #region Values
    [SerializeField] private UI UI;
    [SerializeField] private LvlShip LvlShip;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _groundDetector;

    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isGrounded = false, _onAction = false, _isDead = false;
    [SerializeField] public int Hp = 10;
    [SerializeField] private float _Speed, _jumpForce;
    [SerializeField] private string _actionType = null;
    [SerializeField] private Vector2 _Move;
    [SerializeField] private GameObject collectableObject, Use, hand;

    public List<GameObject> inventory = new List<GameObject>();
    [SerializeField] public GameObject[] equipment;
    #endregion

    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        UI = GameObject.Find("Canvas").GetComponent<UI>();
        LvlShip = GameObject.Find("Ship").GetComponent<LvlShip>();
        //hand = GameObject.Find("hand").gameObject;
        _isGrounded = false;
        _Speed = 5;
        _jumpForce = 7;
        Use = transform.GetChild(0).gameObject;
        equipment = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!_isDead || !_onAction)
        {
            _rb.velocity = new Vector2(_Move.x * _Speed, _rb.velocity.y);
        }

        _animator.SetInteger("VelocityX", _Move.x.ConvertTo<int>());
        _animator.SetFloat("VelocityY", _rb.velocity.y);
        _animator.SetBool("IsGrounded", _isGrounded);
        */


        //if (equipment[1] != null) 
        //  _animator.SetInteger("hand", equipment[0].gameObject.GetComponent<Collectible>().weaponState);


        /*
        #region Rotation
        if (_rb.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (_rb.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        #endregion

        print(_rb.velocity);*/
    }


    #region TriggerCollider
/*    private void OnTriggerEnter2D(Collider2D collision)
    {

*//*        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
            _animator.SetBool("Jump", false);
        }*/
/*        if (collision.gameObject.GetComponent<Collectible>())
        {
            collectableObject = collision.gameObject;
        }

        if (collision.gameObject.name == "Ship")
        {
            LvlShip.GetInventory(inventory);
        }*//*

        if (collision.gameObject.tag == "EnnemyAttack")
        {
            _animator.SetTrigger("TakeDamage");
            TakeDamage(2, true);
        }
    }*/

/*    private void OnTriggerExit2D(Collider2D collision)
    {
*//*        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }*/

/*        if (collision.gameObject == collectableObject)
        {
            collectableObject = null;
        }*//*


    }*/
    #endregion

/*    #region Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (_isGrounded)
        {
            _animator.SetBool("Jump", true);
        }
    }
    public void OnJump()
    {
        _rb.velocity = Vector3.up * _jumpForce;
    }
    #endregion
*/
/*    void Idle()
    {
        //_Move = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (!_onAction)
            _Move = context.ReadValue<Vector2>();
    }*/

/*    public void Collect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (collectableObject)
            {
                _animator.SetTrigger("Collect");
                collectableObject.transform.position = new Vector2(99, 99);
                inventory.Add(collectableObject);
                if (collectableObject.transform.tag == "Weapon")
                    equipment[0] = collectableObject;
                UI.UpdateUI(collectableObject.GetComponent<SpriteRenderer>().sprite, collectableObject.transform.tag*//*, inventory*//*);
                collectableObject = null;
                Equip();
            }
        }
    }
*/
/*    public void Equip()
    {
        if (equipment[0] != null)
        {
            equipment[0].transform.parent = hand.transform;
            equipment[0].transform.position = Vector2.zero;
        }
    }*/

/*    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger("Attack");
        }

    }
*/
/*    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _animator.SetTrigger("Dodge");
            _Move = new Vector2(0.5f * transform.localScale.x, 0);
        }
    }*/

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