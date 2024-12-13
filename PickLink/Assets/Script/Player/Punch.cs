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

    public void OnPunch(InputAction.CallbackContext context)
    {
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

            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(_force, _transform.position, _radius);

                if (_time >= 1.5f)
                {
                    rb.AddForce(transform.up * _forceUp, ForceMode.Impulse);
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
