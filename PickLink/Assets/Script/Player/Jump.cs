using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] public bool CanJump {  get; set; } = true;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jumpForce;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanJump) return;

        if (context.performed)
        {
            ActiveJump();
        }
    }

    public void ActiveJump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
}
