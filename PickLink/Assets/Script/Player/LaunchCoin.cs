using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchCoin : MonoBehaviour
{
    [SerializeField] private float _force;

    public void OnLaunchCoin(InputAction.CallbackContext context)
    {
        GameObject objectCoin = CoinObjectPool.Instance.GetPooledObject();

        if (objectCoin != null)
        {
            objectCoin.transform.position = transform.position;
            objectCoin.transform.rotation = transform.rotation;
            objectCoin.SetActive(true);
            Rigidbody rb = objectCoin.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _force, ForceMode.Impulse);
        }
    }
}
