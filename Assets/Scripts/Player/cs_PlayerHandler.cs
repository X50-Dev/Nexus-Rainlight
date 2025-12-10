using UnityEngine;
using UnityEngine.InputSystem;

public class cs_PlayerHandler : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction; 
    [SerializeField] private Vector2 _moveInput;
    [SerializeField] private bool _Jump;

    private void OnEnable()
    {
        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.performed += OnMove;
            moveAction.action.canceled += OnMove;
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.performed -= OnMove;
            moveAction.action.canceled -= OnMove;
            moveAction.action.Disable();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public Vector2 GetMoveInput()
    {
        return _moveInput;
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        _Jump = (context.ReadValue<float>() == 1);
    }

    public int GetJumpInput()
    {
        return _Jump ? 1 : 0;
    }

}