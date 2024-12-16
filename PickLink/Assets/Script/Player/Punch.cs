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
    [SerializeField] public int KillCount { get; set; }
    private bool _quota = false;

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (!_quota)
        {
            if (KillCount >= _score.Quota)
            {
                _score.Point += 100;
                _quota = true;
            }
        }

        if (!CanPunch) return;

        if (context.performed)
        {
            _charged = true;
        }

        if (context.canceled)
        {
            ActivePunch();
            StartCoroutine(Wait());
            _charged = false;
            _time = 0;
        }
    }

    void Update()
    {
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
                        _score.Point += 2;
                    }
                    enemy.HP -= (_dammage * 2);
                }

                if (enemy.HP <= 1)
                {
                    _score.Point += 1;
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
        yield return new WaitForSeconds(1);
        CanPunch = true;
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
