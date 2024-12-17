using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private Role _role;
    [SerializeField] private Score _score;
    [SerializeField] public bool CanJump {  get; set; } = true;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jumpForce;
    private bool _jump = false;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _collisionLayer;
    private bool _planing;
    [SerializeField] public int JumpCount {  get; set; }
    private bool _quota = false;

    private void Update()
    {
        if (!_quota)
        {
            if (JumpCount >= _score.Quota)
            {
                _score.Point += 100;
                _quota = true;
            }
        }

        if (CanJump)
        {
            _jump = Physics.OverlapSphere(_groundCheck.position, _radius, _collisionLayer).Length > 0;
        }

        if (_rb.velocity.y < 0 && !_jump)
        {
            if (_planing)
            {
                _rb.velocity = new Vector3(0, -0.1f, 0);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _planing = false;
        }

        if (!CanJump) return;

        if (context.performed && _jump)
        {
            ActiveJump();
            
            if (_role.RoleName == "Voltigeur")
            {
                _score.Point += 1;
                JumpCount += 1;
                _planing = true;
            }
        }
    }

    public void ActiveJump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        CanJump = false;
        _jump = false;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
        CanJump = true;
    }

    void OnDrawGizmos()
    {
        if (_groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        }
    }
}
