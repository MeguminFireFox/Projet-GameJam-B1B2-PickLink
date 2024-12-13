using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchCoin : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private Role _role;
    [SerializeField] private float _time;
    [SerializeField] public bool CanLaunch { get; set; } = true;

    public void OnLaunchCoin(InputAction.CallbackContext context)
    {
        if (_role.RoleName != "Picsou") return;
        
        if (!CanLaunch) return;

        if (context.performed)
        {
            GameObject objectCoin = CoinObjectPool.Instance.GetPooledObject();

            if (objectCoin != null)
            {
                objectCoin.transform.position = transform.position;
                objectCoin.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, objectCoin.transform.rotation.z, transform.rotation.w);
                objectCoin.SetActive(true);
                Rigidbody rb = objectCoin.GetComponent<Rigidbody>();
                rb.AddForce(gameObject.transform.forward * _force, ForceMode.Impulse);
                rb.AddForce(gameObject.transform.up * (_force / 2), ForceMode.Impulse);
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        CanLaunch = false;
        yield return new WaitForSeconds(_time);
        CanLaunch = true;
    }
}
