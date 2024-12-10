using UnityEngine;
using UnityEngine.InputSystem;

public class Punch : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private LayerMask _affectedLayers;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private float _forceUp;
    private bool _charged = false;
    private float _time;

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _charged = true;
        }

        if (context.canceled)
        {
            ActivePunch();
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
}
