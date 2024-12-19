using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Punch : MonoBehaviour
{
    [field: SerializeField] public bool CanPunch { get; set; } = true;
    [SerializeField] private Transform _transform;
    [SerializeField] private LayerMask _affectedLayers;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _forceUp;
    private bool _charged = false;
    private float _time;
    [SerializeField] private float _dammage;
    [SerializeField] private Role _role;
    [SerializeField] private Score _score;
    [SerializeField] private Animator _animator;
    [SerializeField] public int KillCount { get; set; }
    private bool _quota = false;
    private bool _canQuota = false;

    private void Start()
    {
        StartCoroutine(ActiveQuota());
    }

    IEnumerator ActiveQuota()
    {
        yield return new WaitForSeconds(2.5f);
        _canQuota = true;
    }

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (!CanPunch) return;

        if (context.performed)
        {
            _charged = true;
        }

        if (context.canceled)
        {
            StartCoroutine(WaitAttack());
        }
    }

    IEnumerator WaitAttack()
    {
        _animator.SetBool("IsHit", true);
        yield return new WaitForSeconds(0.2f);
        _animator.SetBool("IsHit", false);
        ActivePunch();
        StartCoroutine(Wait());
        _charged = false;
        _time = 0;
    }

    void Update()
    {
        if (_canQuota)
        {
            if (!_quota)
            {
                if (KillCount >= _score.Quota && _role.RoleName == "Killer")
                {
                    _score.Point += 100;
                    _quota = true;
                }
            }
        }

        if (!_charged) return;

        _time += Time.deltaTime;
    }

    void ActivePunch()
    {
        Collider[] colliders = Physics.OverlapSphere(_transform.position, _radius, _affectedLayers);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == this.gameObject) continue;

            EnemyHP enemy = collider.GetComponent<EnemyHP>();

            if (enemy != null)
            {
                if (_role.RoleName == "Killer")
                {
                    if (enemy.HP <= 3)
                    {
                        KillCount += 1;
                        _score.CurrentQuota = KillCount;
                        _score.Point += 2;
                    }
                    enemy.HP -= (_dammage + 1);
                }

                if (enemy.HP <= 1)
                {
                    _score.Point += 1;
                }
                else
                {
                    enemy.Animator.SetBool("IsHit", true);
                    StartCoroutine(WaitAnimationEnemy(enemy));
                }
                enemy.HP -= _dammage;
            }
            else
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                PlayerStun stun = rb.GetComponent<PlayerStun>();

                if (rb != null)
                {
                    rb.AddExplosionForce(_force, _transform.position, _radius);
                    stun.Torpeur += 10;

                    if (stun.Torpeur < stun._torpeurObjectif)
                    {
                        stun.Animator.SetBool("IsAie", true);
                    }

                    StartCoroutine(WaitAnimation(stun));

                    if (_time >= 1.5f)
                    {
                        rb.AddForce(transform.up * _forceUp, ForceMode.Impulse);
                        stun.Torpeur += 10;
                    }
                }
            }
        }
    }

    IEnumerator Wait()
    {
        CanPunch = false;
        //yield return new WaitForSeconds(0.1f);
        //_animator.SetBool("IsHit", false);
        yield return new WaitForSeconds(0.9f);
        CanPunch = true;
    }

    IEnumerator WaitAnimation(PlayerStun stun)
    {
        yield return new WaitForSeconds(0.1f);
        stun.Animator.SetBool("IsAie", false);
    }

    IEnumerator WaitAnimationEnemy(EnemyHP enemy)
    {
        yield return new WaitForSeconds(0.1f);
        enemy.Animator.SetBool("IsHit", false);
    }

    void OnDrawGizmos()
    {
        if (_transform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_transform.position, _radius);
        }
    }
}
