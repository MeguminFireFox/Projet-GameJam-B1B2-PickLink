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
    [SerializeField] private bool _jump = false;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private LayerMask _verifSol;

    [SerializeField] private bool _planing;
    [SerializeField] public int JumpCount {  get; set; }
    [SerializeField] private Animator _animator;
    private bool _quota = false;
    private bool _canQuota = false;
    private Collider[] _colliders;
    private bool _canCheck;
    [SerializeField] public Vector3 _posRespawn;

    private void Start()
    {
        StartCoroutine(ActiveQuota());
    }

    IEnumerator ActiveQuota()
    {
        yield return new WaitForSeconds(2.5f);
        _canQuota = true;
    }

    private void Update()
    {
        _canCheck = Physics.OverlapSphere(_groundCheck.position, _radius, _verifSol).Length > 0;

        if (_canCheck)
        {
            _posRespawn = transform.position;
        }

        if (_canQuota)
        {
            if (!_quota)
            {
                if (JumpCount >= _score.Quota && _role.RoleName == "Voltigeur")
                {
                    _score.Point += 100;
                    _quota = true;
                }
            }
        }

        if (CanJump)
        {
            _jump = Physics.OverlapSphere(_groundCheck.position, _radius, _collisionLayer).Length > 0;
            _colliders = Physics.OverlapSphere(_groundCheck.position, _radius, _collisionLayer);
        }

        if (_rb.velocity.y < 0)
        {
            if (_planing)
            {
                _rb.velocity = new Vector3(0, -0.9f, 0);
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

        foreach (Collider collider in _colliders)
        {
            if (context.performed && _jump && collider.gameObject != this.gameObject)
            {
                _animator.SetBool("IsJump", true);
                ActiveJump();

                if (_role.RoleName == "Voltigeur")
                {
                    _score.Point += 1;
                    JumpCount += 1;
                    _score.CurrentQuota = JumpCount;
                    _planing = true;
                }
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
        _animator.SetBool("IsJump", false);
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
