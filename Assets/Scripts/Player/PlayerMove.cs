using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CircleCollider2D _groundDetector;
    [SerializeField] private Animator _animator;

    [SerializeField] private Vector2 _Move;

    [SerializeField] private int _groundCount;



    [Header("Value")]
    [SerializeField] private float _Speed;


    [Header("BOOL")]
    [SerializeField] private bool bIsGrounded = false;
    [SerializeField] private bool _onAction = false;
    [SerializeField] private bool _isDead = false;
    [SerializeField] private float bOnJump = 0;
    [SerializeField] private float _timerJump = 0f;

    [Header("Jump System")]
    #region Jump
    public bool bHasJumped;

    [SerializeField] private float _jumpForce; // Puissance du saut
    public float jumpTimeCounter; // Temps maximal auquel le joueur peut resté appuyé
    public float jumpTime = 1; // 

    //Multiplicateur de gravité, elle est modifié lors du saut
    public float lowJumpMultiplier; // en relâchant
    public float fallMultiplier; //pendant la chute
    public float normalGravity; //au sol
    public float groundedGravity; //au sol
    #endregion

    public float stickForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _groundDetector = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        bIsGrounded = false;
        /*        _Speed = 5;
                _jumpForce = 7;*/

        normalGravity = rb.gravityScale;
    }



    void Update()
    {
        //float Jumping = bOnJump == 1 ? _jumpForce : Physics.gravity.y;

        if (!_isDead || !_onAction)
        {
            //rb.velocity = new Vector2(_Move.x * _Speed, Jumping);
            rb.velocity = new Vector2(_Move.x * _Speed, /*bIsGrounded && _Move.x != 0 == true ? -0.5f : */rb.velocity.y);

            if (bIsGrounded)
            {
                if (_Move.x == 0) rb.gravityScale = groundedGravity;
                else rb.gravityScale = 0;

                if (rb.velocity.y != 0) rb.velocity = new Vector2(rb.velocity.x, -5);
            }
        }

        _animator.SetInteger("VelocityX", _Move.x.ConvertTo<int>());
        _animator.SetFloat("VelocityY", rb.velocity.y);
        _animator.SetBool("IsGrounded", bIsGrounded);
        //if (equipment[1] != null) 
        //  _animator.SetInteger("hand", equipment[0].gameObject.GetComponent<Collectible>().weaponState);

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Rotation
            if (rb.velocity.x > 0)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            if (rb.velocity.x < 0)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
            #endregion
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        bIsGrounded = _groundCount > 0 ? true : false;

        // Ici le personnage doit rester collé au sol, si celui-ci saute la gravité reviendra à la normal

        ///if (bIsGrounded) rb.gravityScale = groundedGravity;

        JumpBehaviour();

        if (bIsGrounded)

            /*            if (_Move != Vector2.zero) rb.AddForce(Vector2.down * stickForce, ForceMode2D.Force);

                        else */
            //??????????????????????????????????????????????????????????????????????,
            //if (_Move == Vector2.zero) rb.gravityScale = 0;
            //??????????????????????????????????????????????????????????????????????,

        if(Input.GetKey(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            _groundCount++;
            _animator.SetBool("Jump", false);
            //bHasJumped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundCount--;
        }
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    #region Jump

    public void JumpBehaviour()
    {
        if (bOnJump == 1 && !bHasJumped)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, _jumpForce);
                jumpTimeCounter -= Time.deltaTime;
                _animator.SetBool("Jump", true);
            }
            else
            {

            }
        }

        if (!bIsGrounded)
        {
            ///////////
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = fallMultiplier; // descente plus rapide
            }
            else if (rb.velocity.y > 0 && bOnJump <= 0)
            {
                rb.gravityScale = lowJumpMultiplier; // saut écourté
                bHasJumped = true;
            }
            else
            {
                rb.gravityScale = normalGravity; // montée normale
            }
        }
        else bHasJumped = false;
    }
    public void Jump(InputAction.CallbackContext context) // Lorsque le joueur appuie sur la touche "saut"
    {

        bOnJump = context.ReadValue<float>();

        if (bOnJump == 1 && bIsGrounded && !bHasJumped)
        {
            jumpTimeCounter = jumpTime; // durée max pendant laquelle on peut "continuer" le saut
        }

    }
    #endregion

    void Idle()
    {
        //_Move = Vector2.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (!_onAction)
        _Move = context.ReadValue<Vector2>();
    }
    bool _drawGizmo;
    Vector3 _lastGizmoPoint;
    public void Dodge(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 3f);
            Debug.DrawLine(transform.position, hit.point, color: Color.red, 2f);
            Vector3 endPoint = (hit.collider != null) ? (Vector3)hit.point : (transform.position + (Vector3)(Vector2.right * transform.localScale.x * 3f));

            // On ne peut plus appeler Gizmos.DrawSphere ici (runtime) — on mémorise le point et on le dessine dans OnDrawGizmos (éditeur)
            _lastGizmoPoint = endPoint;
            _drawGizmo = true;
            StopCoroutine(nameof(ClearGizmoAfterSeconds));
            StartCoroutine(ClearGizmoAfterSeconds(2f));

            if(hit.collider == null)
            {
                _animator.SetTrigger("Dodge"); 
                StartCoroutine(MoveToTarget(endPoint, 0.25f));
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 destination, float time)
    {
        Vector3 start = transform.position;
        float elapsed = 0.01f;

        while (elapsed < time)
        {
            transform.position = Vector3.Lerp(start, destination, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }






    private IEnumerator ClearGizmoAfterSeconds(float s)
    {
        yield return new WaitForSeconds(s);
        _drawGizmo = false;
    }

    // Méthode autorisée pour dessiner des gizmos dans l'éditeur
    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_lastGizmoPoint, 0.25f);
        }

    }
}